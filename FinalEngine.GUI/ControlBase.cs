// <copyright file="ControlBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.GUI;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Textures;

//// TODO: Add support to anchor to pamnels.
//// TODO: Depth
//// TODO: Events
//// dfgm

public abstract class ControlBase
{
    public ControlBase()
    {
        this.IsVisible = true;
    }

    public Anchor Anchor { get; set; }

    public bool IsVisible { get; set; }

    public Vector2 Offset { get; set; }

    public ITexture2D? Texture { get; set; }

    protected Vector2 Origin { get; private set; }

    protected Vector2 Position { get; private set; }

    public void Draw(ISpriteDrawer drawer, IRasterizer rasterizer)
    {
        if (drawer == null)
        {
            throw new ArgumentNullException(nameof(drawer));
        }

        if (rasterizer == null)
        {
            throw new ArgumentNullException(nameof(rasterizer));
        }

        if (this.Texture == null || !this.IsVisible)
        {
            return;
        }

        this.CalculatePositionAndOrigin(rasterizer.GetViewport());

        drawer.Draw(
            texture: this.Texture,
            color: Color.White,
            origin: this.Origin,
            position: this.Position + this.Offset,
            rotation: 0,
            scale: Vector2.One);
    }

    private void CalculatePositionAndOrigin(Rectangle bounds)
    {
        var position = Vector2.Zero;
        var origin = Vector2.Zero;

        if (this.Texture == null)
        {
            return;
        }

        int width = this.Texture.Description.Width;
        int height = this.Texture.Description.Height;

        switch (this.Anchor)
        {
            case Anchor.TopLeft:
                position = Vector2.Zero;
                origin = Vector2.Zero;
                break;

            case Anchor.TopCenter:
                position = new Vector2(bounds.Width / 2, 0);
                origin = new Vector2(width / 2, 0);
                break;

            case Anchor.TopRight:
                position = new Vector2(bounds.Width, 0);
                origin = new Vector2(width, 0);
                break;

            case Anchor.CenterLeft:
                position = new Vector2(0, bounds.Height / 2);
                origin = new Vector2(0, height / 2);
                break;

            case Anchor.Center:
                position = new Vector2(bounds.Width / 2, bounds.Height / 2);
                origin = new Vector2(width / 2, height / 2);
                break;

            case Anchor.CenterRight:
                position = new Vector2(bounds.Width, bounds.Height / 2);
                origin = new Vector2(width, height / 2);
                break;

            case Anchor.BottomLeft:
                position = new Vector2(0, bounds.Height);
                origin = new Vector2(0, height);
                break;

            case Anchor.BottomCenter:
                position = new Vector2(bounds.Width / 2, bounds.Height);
                origin = new Vector2(width / 2, height);
                break;

            case Anchor.BottomRight:
                position = new Vector2(bounds.Width, bounds.Height);
                origin = new Vector2(width, height);
                break;

            default:
                break;
        }

        this.Position = position;
        this.Origin = origin;
    }
}
