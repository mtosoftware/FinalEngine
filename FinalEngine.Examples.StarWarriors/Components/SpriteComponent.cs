// <copyright file="SpriteComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors.Components
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.Rendering.Textures;

    public class SpriteComponent : IComponent
    {
        public Color Color { get; set; } = Color.White;

        public int Height
        {
            get { return this.Texture == null ? 0 : this.Texture.Description.Height; }
        }

        public Vector2 Origin { get; set; }

        public Vector2 Scale { get; set; } = Vector2.One;

        public ITexture2D? Texture { get; set; }

        public int Width
        {
            get { return this.Texture == null ? 0 : this.Texture.Description.Width; }
        }
    }
}