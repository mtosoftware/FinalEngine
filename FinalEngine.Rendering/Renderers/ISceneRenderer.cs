// <copyright file="ISceneRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using FinalEngine.Rendering.Core;

public interface ISceneRenderer
{
    void Render(ICamera camera, bool useBuiltInShader);
}
