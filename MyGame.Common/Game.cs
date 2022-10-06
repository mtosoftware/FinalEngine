namespace MyGame.Common
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.ECS.Components.Cameras;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.ECS.Components.Rendering;
    using FinalEngine.ECS.Systems.Cameras;
    using FinalEngine.ECS.Systems.Input;
    using FinalEngine.ECS.Systems.Rendering;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;
    using FinalEngine.Runtime;
    using FinalEngine.Runtime.Settings;

    public class Game : GameContainerBase
    {
        private IMaterial material;

        private IMesh mesh;

        private IRenderEngine renderEngine;

        private IEntityWorld world;

        public Game(GameSettings settings, IRuntimeFactory factory)
            : base(settings, factory)
        {
        }

        protected override void Initialize()
        {
            this.world = new EntityWorld();

            this.world.AddSystem(new PerspectiveCameraUpdateEntitySystem(this.RenderDevice.Pipeline, this.RenderDevice.Rasterizer));
            this.world.AddSystem(new FreeMovementEntitySystem(this.Keyboard));
            this.world.AddSystem(new FreeRotationEntitySystem(this.Keyboard, this.Mouse, this.RenderDevice.Rasterizer));
            this.world.AddSystem(new MeshRenderEntitySystem(this.RenderDevice));

            IShader vertexShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\shader.vert");
            IShader fragmentShader = ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\shader.frag");
            IShaderProgram shaderProgram = this.RenderDevice.Factory.CreateShaderProgram(new[] { vertexShader, fragmentShader });

            this.renderEngine = new RenderEngine(this.RenderDevice, shaderProgram);

            MeshVertex[] vertices =
            {
                new MeshVertex()
                {
                    Position = new Vector3(-1.0f, -1.0f, 0.0f),
                    Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                    TextureCoordinate = new Vector2(0.0f, 0.0f),
                },

                new MeshVertex()
                {
                    Position = new Vector3(1.0f, -1.0f, 0.0f),
                    Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                    TextureCoordinate = new Vector2(1.0f, 0.0f),
                },

                new MeshVertex()
                {
                    Position = new Vector3(0.0f, 1.0f, 0.0f),
                    Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                    TextureCoordinate = new Vector2(0.5f, 1.0f),
                },
            };

            int[] indices =
            {
                0, 1, 2,
            };

            this.mesh = new Mesh(this.RenderDevice.Factory, vertices, indices);
            this.material = new Material();

            var entity = new Entity();
            entity.AddComponent<TransformComponent>();
            entity.AddComponent(new ModelComponent()
            {
                Mesh = mesh,
                Material = material,
            });

            this.world.AddEntity(entity);

            var cameraEntity = new Entity();

            cameraEntity.AddComponent<TransformComponent>();
            cameraEntity.AddComponent(new VelocityComponent()
            {
                Speed = 0.1f,
            });

            cameraEntity.AddComponent(new PerspectiveCameraComponent()
            {
                ProjectionWidth = this.Settings.WindowSettings.Size.Width,
                ProjectionHeight = this.Settings.WindowSettings.Size.Height,
                Viewport = new Rectangle(0, 0, this.Settings.WindowSettings.Size.Width, this.Settings.WindowSettings.Size.Height),
            });

            this.world.AddEntity(cameraEntity);

            base.Initialize();
        }

        protected override void Render()
        {
            this.renderEngine.Render(this.world);
            base.Render();
        }

        protected override void Update()
        {
            this.world.ProcessAll(GameLoopType.Update);
            base.Update();
        }
    }
}