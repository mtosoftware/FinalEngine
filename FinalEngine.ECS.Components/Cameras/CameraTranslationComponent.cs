// <copyright file="CameraTranslationComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Cameras
{
    public class CameraTranslationComponent : IComponent
    {
        public CameraTranslationComponent(float speed)
        {
            this.Speed = speed;
        }

        public float Speed { get; set; }
    }
}