// <copyright file="PerspectiveCameraComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Cameras
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Maths;

    public class PerspectiveCameraComponent : IComponent
    {
        public PerspectiveCameraComponent()
        {
            this.ProjectionWidth = 1280.0f;
            this.ProjectionHeight = 720.0f;
            this.NearPlaneDistance = 0.1f;
            this.FarPlaneDistance = 1000.0f;
            this.FieldOfView = 70.0f;
            this.IsEnabled = true;
        }

        public float FarPlaneDistance { get; set; }

        public float FieldOfView { get; set; }

        public bool IsEnabled { get; set; }

        public float NearPlaneDistance { get; set; }

        public float ProjectionHeight { get; set; }

        public float ProjectionWidth { get; set; }

        public Rectangle Viewport { get; set; }

        public Matrix4x4 CreateProjectionMatrix()
        {
            return Matrix4x4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(this.FieldOfView),
                this.ProjectionWidth / this.ProjectionHeight,
                this.NearPlaneDistance,
                this.FarPlaneDistance);
        }
    }
}
