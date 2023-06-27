// <copyright file="IMaterial.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;

/// <summary>
/// Defines an interface that represents a material and it's properties.
/// </summary>
public interface IMaterial
{
    /// <summary>
    /// Gets or sets the diffuse texture.
    /// </summary>
    /// <value>
    /// The diffuse texture.
    /// </value>
    ITexture2D DiffuseTexture { get; set; }

    /// <summary>
    /// Updates the currently bound <see cref="IShaderProgram"/> with the properties of this <see cref="IMaterial"/>.
    /// </summary>
    /// <param name="pipeline">
    /// The pipeline.
    /// </param>
    void Bind(IPipeline pipeline);
}
