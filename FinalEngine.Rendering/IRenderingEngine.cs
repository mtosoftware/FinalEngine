// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Numerics;
using FinalEngine.Rendering.Core;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;

public interface IRenderingEngine
{
    void Enqueue(Light light);

    void Enqueue(Model model, Transform transform);

    void Render(ICamera camera);

    void SetAmbientLight(Vector3 color, float intensity);
}
