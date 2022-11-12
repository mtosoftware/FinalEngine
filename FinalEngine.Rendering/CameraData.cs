// <copyright file="CameraData.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Numerics;

    public struct CameraData
    {
        public Matrix4x4 Projection { get; set; }

        public Matrix4x4 View { get; set; }

        public Vector3 ViewPostiion { get; set; }
    }
}
