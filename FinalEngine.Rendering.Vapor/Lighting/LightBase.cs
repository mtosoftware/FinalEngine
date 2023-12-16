// <copyright file="LightBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Lighting;

using System.Drawing;

public abstract class LightBase
{
    protected LightBase()
    {
        this.Color = Color.White;
        this.Intensity = 0.4f;
    }

    public Color Color { get; set; }

    public float Intensity { get; set; }
}
