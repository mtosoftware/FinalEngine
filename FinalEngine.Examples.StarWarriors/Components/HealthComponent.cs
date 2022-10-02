// <copyright file="HealthComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Components
{
    using System;
    using FinalEngine.ECS;

    public class HealthComponent : IComponent
    {
        public double HealthPercentage
        {
            get { return Math.Round(this.Points / this.MaximumHealth * 100f); }
        }

        public bool IsAlive
        {
            get { return this.Points > 0; }
        }

        public float MaximumHealth { get; private set; }

        public float Points { get; set; }

        public void AddDamage(int damage)
        {
            this.Points -= damage;

            if (this.Points < 0)
            {
                this.Points = 0;
            }
        }
    }
}