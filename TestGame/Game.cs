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
    using TestGame.Components;
    using TestGame.Systems;

    public class Game : GameContainer
    {
        private readonly ITexture2D texture;

        private readonly IEntityWorld world;

        private SpriteDrawer? drawer;

        public Game()
        {
            var binder = new TextureBinder(this.RenderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);

            this.drawer = new SpriteDrawer(
                this.RenderDevice,
                batcher,
                binder,
                this.Window!.ClientSize.Width,
                this.Window!.ClientSize.Height);

            this.world = new EntityWorld();
            this.world.AddSystem(new SpriteRenderSystem(this.drawer));
            this.texture = this.ResourceManager!.LoadResource<ITexture2D>("jedi.jpg");

            var entity = new Entity();

            entity.AddComponent(new TransformComponent()
            {
                Rotation = 276,
                Scale = new Vector2(512, 512),
            });

            entity.AddComponent(new SpriteComponent()
            {
                Color = Color.White,
                Origin = new Vector2(256, 256),
                Texture = this.texture,
            });

            this.world.AddEntity(entity);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.drawer != null)
                {
                    this.drawer.Dispose();
                    this.drawer = null;
                }
            }

            base.Dispose(disposing);
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
            this.Window!.Title = GameTime.FrameRate.ToString();
            this.world.ProcessAll(GameLoopType.Update);

            base.Update();
        }
    }
}