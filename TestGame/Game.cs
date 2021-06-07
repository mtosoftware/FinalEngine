// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System.Drawing;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;

    public class Game : GameContainer
    {
        private readonly ISpriteDrawer drawer;

        public Game()
        {
            var binder = new TextureBinder(this.RenderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);
            this.drawer = new SpriteDrawer(this.RenderDevice, batcher, binder, this.Window!.ClientSize.Width, this.Window!.ClientSize.Height);
        }

        protected override void Render()
        {
            this.RenderDevice.Clear(Color.CornflowerBlue);

            base.Render();
        }

        protected override void Update()
        {
            if (this.Keyboard.IsKeyReleased(Key.Escape))
            {
                this.Exit();
            }

            base.Update();
        }
    }
}