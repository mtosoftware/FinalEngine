// <copyright file="SpriteComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Rendering;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Textures;

public sealed class SpriteComponent : IComponent
{
    public SpriteComponent()
    {
        this.Texture = null;
        this.Color = Color.White;
        this.Origin = Vector2.Zero;
    }

    public Color Color { get; set; }

    public Vector2 Origin { get; set; }

    public ITexture2D? Texture { get; set; }
}
