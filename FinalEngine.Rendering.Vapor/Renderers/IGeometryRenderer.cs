// <copyright file="IGeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Renderers;

using System.Collections.Generic;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;

public interface IGeometryRenderer
{
    void Render(IDictionary<Model, IEnumerable<Transform>> modelToTransformMap);
}
