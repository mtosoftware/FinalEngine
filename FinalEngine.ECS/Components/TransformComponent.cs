// <copyright file="TransformComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components
{
    using System.Numerics;

    public class TransformComponent : IComponent
    {
        private float scaleX;

        private float scaleY;

        private float x;

        private float y;

        public TransformComponent()
        {
            this.Scale = Vector2.One;
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }

            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        public float Rotation { get; set; }

        public Vector2 Scale
        {
            get
            {
                return new Vector2(this.ScaleX, this.ScaleY);
            }

            set
            {
                this.ScaleX = value.X;
                this.ScaleY = value.Y;
            }
        }

        public float ScaleX
        {
            get { return scaleX; }
            set { this.scaleX = value; }
        }

        public float ScaleY
        {
            get { return this.scaleY; }
            set { this.scaleY = value; }
        }

        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
    }
}