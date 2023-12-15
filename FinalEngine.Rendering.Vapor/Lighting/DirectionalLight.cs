// <copyright file="DirectionalLight.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Lighting;

using System.Numerics;

public sealed class DirectionalLight : LightBase
{
    public Vector3 Direction { get; set; }
}
