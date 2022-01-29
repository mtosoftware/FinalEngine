// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using FinalEngine.Launching;

    public sealed class Game : GameContainer
    {
        protected override void Update()
        {
            if (this.Keyboard.IsKeyReleased(FinalEngine.Input.Key.Escape))
            {
                this.Exit();
            }

            base.Update();
        }
    }
}