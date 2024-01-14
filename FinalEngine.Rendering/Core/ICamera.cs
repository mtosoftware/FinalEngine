// <copyright file="ICamera.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Core;

using System.Drawing;
using System.Numerics;

public interface ICamera
{
    Rectangle Bounds { get; }

    Matrix4x4 Projection { get; }

    Transform Transform { get; }

    Matrix4x4 View { get; }
}
