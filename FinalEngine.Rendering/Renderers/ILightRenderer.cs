// <copyright file="ILightRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using System;
using System.Numerics;

public interface ILightRenderer
{
    void Render(Action renderScene);

    void SetAmbientLight(Vector3 color, float intensity);
}
