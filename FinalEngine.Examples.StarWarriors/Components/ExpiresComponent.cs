// <copyright file="ExpiresComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Components
{
    using FinalEngine.ECS;

    public class ExpiresComponent : IComponent
    {
        public bool IsExpired
        {
            get
            {
                return this.LifeTime <= 0;
            }
        }

        public float LifeTime { get; set; }

        public void ReduceLifeTime(float lifeTimeDelta)
        {
            this.LifeTime -= lifeTimeDelta;

            if (this.LifeTime < 0)
            {
                this.LifeTime = 0;
            }
        }
    }
}