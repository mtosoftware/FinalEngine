// <copyright file="ISceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using FinalEngine.Editor.Common.Models.Scenes;

public interface ISceneManager
{
    IScene ActiveScene { get; }

    void Initialize();
}
