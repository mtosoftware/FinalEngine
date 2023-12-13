// <copyright file="PerspectiveCamera.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Core.Cameras;

using System.Drawing;
using System.Numerics;
using FinalEngine.Maths;

public class PerspectiveCamera : IPerspectiveCamera
{
    public PerspectiveCamera()
    {
        this.IsEnabled = true;
    }

    public float AspectRatio { get; set; }

    public float FarPlaneDistance { get; set; }

    public float FieldOfView { get; set; }

    public bool IsEnabled { get; set; }

    public float NearPlaneDistance { get; set; }

    public Rectangle Viewport { get; set; }

    public Matrix4x4 CreateProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.FieldOfView), this.AspectRatio, this.NearPlaneDistance, this.FarPlaneDistance);
    }
}
