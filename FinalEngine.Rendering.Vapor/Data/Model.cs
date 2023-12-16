// <copyright file="Model.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Data;

using System;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Geometry;

public sealed class Model
{
    private IMaterial? material;

    public Guid EntityId { get; init; }

    public IMaterial Material
    {
        get { return this.material ??= new Material(); }
        set { this.material = value; }
    }

    public IMesh? Mesh { get; set; }

    public Transform Transform { get; set; }
}
