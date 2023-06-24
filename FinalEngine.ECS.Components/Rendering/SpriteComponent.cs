// <copyright file="SpriteComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Rendering;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Textures;

/// <summary>
/// Provides a component that represents a sprite (2D texture with an origin) for an entity.
/// </summary>
/// <seealso cref="FinalEngine.ECS.IComponent" />
public sealed class SpriteComponent : IComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SpriteComponent"/> class.
    /// </summary>
    public SpriteComponent()
    {
        this.Texture = null;
        this.Color = Color.White;
        this.Origin = Vector2.Zero;
    }

    /// <summary>
    /// Gets or sets the color of this sprite.
    /// </summary>
    /// <value>
    /// The color of this sprite.
    /// </value>
    public Color Color { get; set; }

    /// <summary>
    /// Gets or sets the origin of this sprite.
    /// </summary>
    /// <value>
    /// The origin of this sprite.
    /// </value>
    public Vector2 Origin { get; set; }

    /// <summary>
    /// Gets or sets the texture of this sprite.
    /// </summary>
    /// <value>
    /// The texture of this sprite.
    /// </value>
    public ITexture2D? Texture { get; set; }
}
