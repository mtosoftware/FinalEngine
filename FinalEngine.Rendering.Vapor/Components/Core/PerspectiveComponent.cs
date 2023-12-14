// <copyright file="PerspectiveComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Components.Core;

using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.Maths;

public sealed class PerspectiveComponent : IEntityComponent
{
    public float AspectRatio { get; set; }

    public float FarPlaneDistance { get; set; }

    public float FieldOfView { get; set; }

    public float NearPlaneDistance { get; set; }

    public Matrix4x4 CreateProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.FieldOfView), this.AspectRatio, this.NearPlaneDistance, this.FarPlaneDistance);
    }
}
