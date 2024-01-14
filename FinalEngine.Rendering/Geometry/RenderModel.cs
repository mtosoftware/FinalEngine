// <copyright file="RenderModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

using FinalEngine.Rendering.Core;

public sealed class RenderModel
{
    private IMaterial? material;

    private Transform? transform;

    public IMaterial Material
    {
        get { return this.material ??= new Material(); }
        set { this.material = value; }
    }

    public IMesh? Mesh { get; set; }

    public Transform Transform
    {
        get { return this.transform ??= new Transform(); }
        set { this.transform = value; }
    }
}
