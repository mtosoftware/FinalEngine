// <copyright file="CameraComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Rendering
{
    using System.Drawing;

    public class CameraComponent : IComponent
    {
        public bool IsEnabled { get; set; }

        public bool IsLocked { get; set; }

        public Rectangle Viewport { get; set; }
    }
}
