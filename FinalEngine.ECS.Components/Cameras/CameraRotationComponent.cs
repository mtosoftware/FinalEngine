// <copyright file="CameraRotationComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Cameras
{
    public class CameraRotationComponent : IComponent
    {
        public CameraRotationComponent(float speed)
        {
            this.Speed = speed;
        }

        public float Speed { get; set; }
    }
}