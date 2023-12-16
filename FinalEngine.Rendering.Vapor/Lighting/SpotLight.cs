// <copyright file="SpotLight.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Lighting;

using System.Numerics;

public class SpotLight : PointLight
{
    public Vector3 Direction { get; set; }

    public float OuterRadius { get; set; }

    public float Radius { get; set; }
}
