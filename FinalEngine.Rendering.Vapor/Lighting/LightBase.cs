// <copyright file="LightBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Lighting;

using System.Numerics;

public abstract class LightBase
{
    public Vector3 AmbientColor { get; set; }

    public Vector3 DiffuseColor { get; set; }

    public Vector3 SpecularColor { get; set; }
}
