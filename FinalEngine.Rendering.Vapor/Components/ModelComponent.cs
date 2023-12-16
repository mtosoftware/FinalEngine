// <copyright file="ModelComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Components;

using FinalEngine.ECS;
using FinalEngine.Rendering.Vapor.Geometry;

public sealed class ModelComponent : IEntityComponent
{
    private IMaterial? material;

    public IMaterial Material
    {
        get { return this.material ??= new Material(); }
        set { this.material = value; }
    }

    public IMesh? Mesh { get; set; }
}
