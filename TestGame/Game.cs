// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;
    using TestGame.Temp;

    public class Game : GameContainer
    {
        private readonly ISpriteDrawer drawer;

        private readonly ITexture2D textureA;

        private readonly EntityWorld world;

        public Game()
        {
            var binder = new TextureBinder(this.RenderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);
            this.drawer = new SpriteDrawer(this.RenderDevice, batcher, binder, this.Window!.ClientSize.Width, this.Window!.ClientSize.Height);

            this.textureA = this.TextureLoader.LoadTexture("cheese.jpg");

            this.world = new EntityWorld(
                new SpriteRenderSystem(this.drawer));

            var entity = new Entity();

            entity.AddComponent(new TransformComponent()
            {
                Position = Vector2.Zero,
                Rotation = 0,
                Scale = new Vector2(256, 256),
            });

            entity.AddComponent(new Sprite()
            {
                Color = Color.Red,
                Origin = new Vector2(128, 128),
                Texture = this.textureA,
            });

            this.world.AddEntity(entity);
        }

        protected override void Render()
        {
            this.RenderDevice.Clear(Color.CornflowerBlue);

            this.world.ProcessAll(GameLoopType.Render);

            base.Render();
        }

        protected override void Update()
        {
            if (this.Keyboard.IsKeyReleased(Key.Escape))
            {
                this.Exit();
            }

            this.world.ProcessAll(GameLoopType.Update);

            base.Update();
        }
    }
}