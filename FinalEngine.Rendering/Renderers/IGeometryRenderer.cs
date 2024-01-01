// <copyright file="IGeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using System.Collections.Generic;
using FinalEngine.Rendering.Core;
using FinalEngine.Rendering.Geometry;

public interface IGeometryRenderer
{
    void Render(IDictionary<Model, IEnumerable<Transform>> modelToTransformMap);
}
