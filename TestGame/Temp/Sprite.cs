// <copyright file="Sprite.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame.Temp
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.Rendering.Textures;

    public sealed class Sprite : IComponent
    {
        public Color Color { get; set; }

        public Vector2 Origin { get; set; }

        public ITexture2D Texture { get; set; }
    }
}