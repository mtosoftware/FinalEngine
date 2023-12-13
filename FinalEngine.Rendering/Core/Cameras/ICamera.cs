// <copyright file="ICamera.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Core.Cameras;

using System.Drawing;
using System.Numerics;

public interface ICamera
{
    bool IsEnabled { get; set; }

    Rectangle Viewport { get; set; }

    Matrix4x4 CreateProjectionMatrix();
}
