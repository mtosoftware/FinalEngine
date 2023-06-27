// <copyright file="Material.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

public sealed class Material : IMaterial
{
    /// <summary>
    /// The default diffuse texture used when no diffuse texture has been defined.
    /// </summary>
    private static readonly ITexture2D DefaultDiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png");

    /// <summary>
    /// The diffuse texture.
    /// </summary>
    private ITexture2D? diffuseTexture;

    /// <summary>
    /// Gets or sets the diffuse texture.
    /// </summary>
    /// <value>
    /// The diffuse texture.
    /// </value>
    public ITexture2D DiffuseTexture
    {
        get { return this.diffuseTexture ??= DefaultDiffuseTexture; }
        set { this.diffuseTexture = value; }
    }

    /// <summary>
    /// Updates the currently bound <see cref="IShaderProgram" /> with the properties of this <see cref="IMaterial" />.
    /// </summary>
    /// <param name="pipeline">
    /// The pipeline.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="pipeline"/> parameter cannot be null.
    /// </exception>
    public void Bind(IPipeline pipeline)
    {
        if (pipeline == null)
        {
            throw new ArgumentNullException(nameof(pipeline));
        }

        pipeline.SetTexture(this.DiffuseTexture, 0);
    }
}
