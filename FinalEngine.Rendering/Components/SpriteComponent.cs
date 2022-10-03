// <copyright file="SpriteComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Components
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.Rendering.Textures;

    /// <summary>
    ///   Provides a component that represents a sprite to be rendered.
    /// </summary>
    /// <seealso cref="IComponent"/>
    public class SpriteComponent : IComponent
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="SpriteComponent"/> class.
        /// </summary>
        /// <param name="texture">
        ///   The texture to use for the sprite (or <c>null</c> if you wish to define it later).
        /// </param>
        public SpriteComponent(ITexture2D? texture = null)
        {
            this.Texture = texture;
            this.Color = Color.White;
            this.Scale = Vector2.One;
            this.Origin = Vector2.Zero;
        }

        /// <summary>
        ///   Gets or sets the color.
        /// </summary>
        /// <value>
        ///   The color.
        /// </value>
        public Color Color { get; set; }

        /// <summary>
        ///   Gets or sets the origin.
        /// </summary>
        /// <value>
        ///   The origin.
        /// </value>
        public Vector2 Origin { get; set; }

        /// <summary>
        ///   Gets or sets the scale.
        /// </summary>
        /// <value>
        ///   The scale.
        /// </value>
        public Vector2 Scale { get; set; }

        /// <summary>
        ///   Gets the height of the sprite.
        /// </summary>
        /// <value>
        ///   The height of the sprite.
        /// </value>
        public float SpriteHeight
        {
            get { return this.TextureHeight * this.Scale.Y; }
        }

        /// <summary>
        ///   Gets the width of the sprite.
        /// </summary>
        /// <value>
        ///   The width of the sprite.
        /// </value>
        public float SpriteWidth
        {
            get { return this.TextureWidth * this.Scale.X; }
        }

        /// <summary>
        ///   Gets or sets the texture.
        /// </summary>
        /// <value>
        ///   The texture.
        /// </value>
        public ITexture2D? Texture { get; set; }

        /// <summary>
        ///   Gets the height of the texture.
        /// </summary>
        /// <value>
        ///   The height of the texture.
        /// </value>
        public float TextureHeight
        {
            get { return this.Texture?.Description.Height ?? 0; }
        }

        /// <summary>
        ///   Gets the width of the texture.
        /// </summary>
        /// <value>
        ///   The width of the texture.
        /// </value>
        public float TextureWidth
        {
            get { return this.Texture?.Description.Width ?? 0; }
        }
    }
}