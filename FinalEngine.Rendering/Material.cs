// <copyright file="Material.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

public sealed class Material : IMaterial
{
    private static readonly ITexture2D DefaultDiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png");

    private ITexture2D? diffuseTexture;

    public ITexture2D DiffuseTexture
    {
        get { return this.diffuseTexture ??= DefaultDiffuseTexture; }
        set { this.diffuseTexture = value; }
    }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline, nameof(pipeline));
        pipeline.SetTexture(this.DiffuseTexture, 0);
    }
}
