// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System.Drawing;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Launching;

    public class Game : GameContainer
    {
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