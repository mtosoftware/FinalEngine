// <copyright file="IPerspectiveCamera.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Core.Cameras;

public interface IPerspectiveCamera : ICamera
{
    float AspectRatio { get; set; }

    float FarPlaneDistance { get; set; }

    float FieldOfView { get; set; }

    float NearPlaneDistance { get; set; }
}
