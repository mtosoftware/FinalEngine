// <copyright file="IGeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers
{
    using FinalEngine.Rendering.Data;

    public interface IGeometryRenderer
    {
        void AddGeometry(GeometryData data);

        void ClearGeometry();

        void Render();
    }
}
