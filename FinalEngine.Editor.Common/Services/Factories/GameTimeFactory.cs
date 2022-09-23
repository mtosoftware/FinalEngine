// <copyright file="GameTimeFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Factories
{
    using FinalEngine.Platform;

    public class GameTimeFactory : IGameTimeFactory
    {
        public IGameTime CreateGameTime(double frameRate = 120)
        {
            return new GameTime(frameRate);
        }
    }
}