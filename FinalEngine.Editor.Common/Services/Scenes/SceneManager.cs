// <copyright file="SceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using FinalEngine.Editor.Common.Models.Scenes;

public sealed class SceneManager : ISceneManager
{
    public SceneManager(IScene scene)
    {
        this.ActiveScene = scene ?? throw new ArgumentNullException(nameof(scene));
    }

    public IScene ActiveScene { get; }
}
