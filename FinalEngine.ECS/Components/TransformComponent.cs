// <copyright file="TransformComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components
{
    using System.Numerics;

    /// <summary>
    ///   Provides a component that represents the position, rotation and scale of an entity.
    /// </summary>
    /// <seealso cref="IComponent"/>
    public class TransformComponent : IComponent
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="TransformComponent"/> class.
        /// </summary>
        public TransformComponent()
        {
            this.Scale = Vector2.One;
        }

        /// <summary>
        ///   Gets or sets the position.
        /// </summary>
        /// <value>
        ///   The position.
        /// </value>
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

        /// <summary>
        ///   Gets or sets the rotation.
        /// </summary>
        /// <value>
        ///   The rotation.
        /// </value>
        public float Rotation { get; set; }

        /// <summary>
        ///   Gets or sets the scale.
        /// </summary>
        /// <value>
        ///   The scale.
        /// </value>
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

        /// <summary>
        ///   Gets or sets the X component of the <see cref="Scale"/> property.
        /// </summary>
        /// <value>
        ///   The scale X component of the <see cref="Scale"/> property.
        /// </value>
        public float ScaleX { get; set; }

        /// <summary>
        ///   Gets or sets the Y component of the <see cref="Scale"/> property.
        /// </summary>
        /// <value>
        ///   The scale Y component of the <see cref="Scale"/> property.
        /// </value>
        public float ScaleY { get; set; }

        /// <summary>
        ///   Gets or sets the X component of the <see cref="Position"/> property.
        /// </summary>
        /// <value>
        ///   The X component of the <see cref="Position"/> property.
        /// </value>
        public float X { get; set; }

        /// <summary>
        ///   Gets or sets the Y component of the <see cref="Position"/> property.
        /// </summary>
        /// <value>
        ///   The Y component of the <see cref="Position"/> property.
        /// </value>
        public float Y { get; set; }
    }
}