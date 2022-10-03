// <copyright file="SpriteComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Components
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.Rendering.Textures;

    public class SpriteComponent : IComponent
    {
        public SpriteComponent(ITexture2D? texture = null)
        {
            this.Texture = texture;
            this.Color = Color.White;
            this.Scale = Vector2.One;
            this.Origin = Vector2.Zero;
        }

        public Color Color { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Scale { get; set; }

        public int SpriteHeight
        {
            get { return this.TextureHeight * (int)this.Scale.Y; }
        }

        public int SpriteWidth
        {
            get { return this.TextureWidth * (int)this.Scale.X; }
        }

        public ITexture2D? Texture { get; set; }

        public int TextureHeight
        {
            get { return this.Texture?.Description.Width ?? 0; }
        }

        public int TextureWidth
        {
            get { return this.Texture?.Description.Height ?? 0; }
        }
    }
}