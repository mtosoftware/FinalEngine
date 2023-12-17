// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;
using FinalEngine.Rendering.Vapor.Lighting;

public interface IRenderingEngine
{
    void Enqueue(Light light);

    void Enqueue(Model model, Transform transform);

    void Render(ICamera camera);
}
