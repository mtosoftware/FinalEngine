// <copyright file="IPostRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using System;
using FinalEngine.Rendering.Core;

public interface IPostRenderer
{
    void Render(ICamera camera, Action renderScene);
}
