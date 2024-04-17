// <copyright file="ISkyboxRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Textures;

public interface ISkyboxRenderer
{
    void Render(ICamera camera);

    void SetSkybox(ITextureCube? texture);
}
