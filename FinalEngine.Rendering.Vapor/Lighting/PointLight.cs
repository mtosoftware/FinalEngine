// <copyright file="PointLight.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Lighting;

using System.Numerics;

public class PointLight : LightBase
{
    public Attenuation Attenuation { get; set; }

    public Vector3 Position { get; set; }
}
