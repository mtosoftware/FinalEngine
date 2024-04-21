// <copyright file="SpriteComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Components;

using System.Drawing;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

public sealed class SpriteComponent : IEntityComponent
{
    public Color Color { get; set; } = Color.White;

    public Vector2 Origin { get; set; }

    public ITexture2D? Texture { get; set; } = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png");
}
