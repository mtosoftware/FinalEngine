// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;

    public class Game : GameContainer
    {
        private readonly ISpriteDrawer drawer;

        private readonly ITexture2D textureA;

        private readonly ITexture2D textureB;

        public Game()
        {
            var binder = new TextureBinder(this.RenderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);
            this.drawer = new SpriteDrawer(this.RenderDevice, batcher, binder, this.Window!.ClientSize.Width, this.Window!.ClientSize.Height);

            this.textureA = this.TextureLoader.LoadTexture("jedi.jpg");
            this.textureB = this.TextureLoader.LoadTexture("cheese.jpg");
        }

        protected override void Render(GameTimeInfo gameTime)
        {
            this.RenderDevice.Clear(Color.CornflowerBlue);

            this.drawer.Begin();

            this.drawer.Draw(this.textureA, Color.Red, Vector2.Zero, Vector2.Zero, 45, new Vector2(256, 256));
            this.drawer.Draw(this.textureB, Color.Green, Vector2.Zero, new Vector2(256, 0), 142, new Vector2(256, 256));
            this.drawer.Draw(this.textureA, Color.Purple, Vector2.Zero, new Vector2(0, 256), 5821, new Vector2(256, 256));

            this.drawer.End();

            base.Render(gameTime);
        }

        protected override void Update(GameTimeInfo gameTime)
        {
            if (this.Keyboard.IsKeyReleased(Key.Escape))
            {
                this.Exit();
            }

            base.Update(gameTime);
        }
    }
}