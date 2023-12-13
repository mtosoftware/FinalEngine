// <copyright file="PerspectiveCameraComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Rendering.Cameras;

using System.ComponentModel;
using FinalEngine.Rendering.Core.Cameras;

[Category("Cameras")]
public sealed class PerspectiveCameraComponent : PerspectiveCamera, IEntityComponent
{
    public bool IsLocked { get; set; }
}
