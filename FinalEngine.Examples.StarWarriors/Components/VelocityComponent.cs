// <copyright file="VelocityComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Components
{
    using System;
    using FinalEngine.ECS;

    public class VelocityComponent : IComponent
    {
        private const float ToRadians = (float)(Math.PI / 180.0d);

        public float Angle { get; set; }

        public float AngleAsRadians
        {
            get { return this.Angle * ToRadians; }
        }

        public float Speed { get; set; }

        public void AddAngle(float angle)
        {
            this.Angle = (this.Angle + angle) % 360;
        }
    }
}