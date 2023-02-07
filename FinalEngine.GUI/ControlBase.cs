// <copyright file="ControlBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.GUI;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Textures;

//// TODO: Anchors
//// TODO: Depth
//// TODO: Events

public abstract class ControlBase
{
    public ControlBase()
    {
        this.IsVisible = true;
    }

    public bool IsVisible { get; set; }

    public ITexture2D? Texture { get; set; }

    public void Draw(ISpriteDrawer drawer)
    {
        if (drawer == null)
        {
            throw new ArgumentNullException(nameof(drawer));
        }

        if (this.Texture == null || !this.IsVisible)
        {
            return;
        }

        drawer.Draw(
            texture: this.Texture,
            color: Color.White,
            origin: Vector2.Zero,
            position: Vector2.Zero,
            rotation: 0,
            scale: Vector2.One);
    }
}
