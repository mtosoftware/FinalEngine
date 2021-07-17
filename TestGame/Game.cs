// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;

    public class Game : GameContainer
    {
        private readonly ITexture2D texture;

        private SpriteDrawer drawer;

        public Game()
        {
            var binder = new TextureBinder(this.RenderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);

            this.drawer = new SpriteDrawer(this.RenderDevice, batcher, binder, this.Window.ClientSize.Width, this.Window.ClientSize.Height);

            this.texture = this.ResourceManager.LoadResource<ITexture2D>("player.png");
        }

        protected override void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (this.drawer != null)
            {
                this.drawer.Dispose();
                this.drawer = null;
            }

            base.Dispose(disposing);
        }

        protected override void Render()
        {
            this.RenderDevice.Clear(Color.CornflowerBlue);

            this.drawer.Begin();

            this.drawer.Draw(
                this.texture,
                Color.White,
                Vector2.Zero,
                new Vector2(0, 0),
                0,
                new Vector2(256, 256));

            this.drawer.End();

            this.Window.Title = $"FPS: {GameTime.FrameRate} : Delta: {GameTime.Delta}";

            base.Render();
        }
    }
}