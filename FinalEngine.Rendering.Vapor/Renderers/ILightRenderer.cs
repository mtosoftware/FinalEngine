// <copyright file="ILightRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Renderers;

using FinalEngine.Rendering.Vapor.Lighting;

public interface ILightRenderer
{
    void Render(Light light);
}
