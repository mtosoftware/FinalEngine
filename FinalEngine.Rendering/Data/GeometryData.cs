// <copyright file="GeometryData.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Data
{
    using System;
    using System.Numerics;

    public class GeometryData
    {
        public GeometryData(IMaterial material, IMesh mesh, Matrix4x4 transformation)
        {
            this.Material = material ?? throw new ArgumentNullException(nameof(material));
            this.Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));
            this.Transformation = transformation;
        }

        public IMaterial Material { get; }

        public IMesh Mesh { get; }

        public Matrix4x4 Transformation { get; }
    }
}
