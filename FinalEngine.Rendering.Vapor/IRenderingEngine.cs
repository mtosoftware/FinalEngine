// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System.Numerics;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Lighting;

public interface IRenderingEngine
{
    void Enqueue(Light light);

    void Enqueue(Model model, Transform transform);

    void Render(ICamera camera);

    void SetAmbientLight(Vector3 color, float intensity);
}
