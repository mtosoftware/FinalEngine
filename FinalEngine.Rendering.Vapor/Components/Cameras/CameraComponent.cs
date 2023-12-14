// <copyright file="CameraComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Components.Cameras;

using System.Drawing;
using FinalEngine.ECS;

public sealed class CameraComponent : IEntityComponent
{
    public bool IsEnabled { get; set; }

    public bool IsLocked { get; set; }

    public Rectangle Viewport { get; set; }
}
