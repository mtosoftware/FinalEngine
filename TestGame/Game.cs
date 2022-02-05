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
            this.Window.Title = GameTime.FrameRate.ToString();
            base.Update();
        }
    }
}