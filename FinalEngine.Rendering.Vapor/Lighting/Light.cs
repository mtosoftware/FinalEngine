// <copyright file="Light.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Lighting;

using System;
using System.Numerics;
using FinalEngine.Maths;

public enum LightType
{
    Ambient,

    Directional,

    Point,

    Spot,
}

public class Light
{
    public Light()
    {
        this.Color = Vector3.One;
        this.Intensity = 1.0f;
        this.Type = LightType.Point;
        this.Attenuation = new Attenuation();
        this.Radius = MathF.Cos(MathHelper.DegreesToRadians(12.5f));
        this.OuterRadius = MathF.Cos(MathHelper.DegreesToRadians(17.5f));
        this.Direction = new Vector3(1, 0, 1);
    }

    public Attenuation Attenuation { get; set; }

    public Vector3 Color { get; set; }

    public Vector3 Direction { get; set; }

    public float Intensity { get; set; }

    public float OuterRadius { get; set; }

    public Vector3 Position { get; set; }

    public float Radius { get; set; }

    public LightType Type { get; set; }
}
