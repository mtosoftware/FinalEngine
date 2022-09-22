// <copyright file="IGameTimeFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Factories
{
    using FinalEngine.Launching;

    public interface IGameTimeFactory
    {
        IGameTime CreateGameTime(double frameRate = 60.0d);
    }
}