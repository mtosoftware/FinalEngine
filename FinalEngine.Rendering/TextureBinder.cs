// <copyright file="TextureBinder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Textures;

/// <summary>
///   Provides a standard implementation of an <see cref="ITextureBinder"/>.
/// </summary>
/// <seealso cref="ITextureBinder"/>
public class TextureBinder : ITextureBinder
{
    /// <summary>
    ///   The pipeline, used to bind textures.
    /// </summary>
    private readonly IPipeline pipeline;

    /// <summary>
    ///   The texture to texture slot index map.
    /// </summary>
    private readonly IDictionary<ITexture, int> textureToSlotMap;

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextureBinder"/> class.
    /// </summary>
    /// <param name="pipeline">
    ///   The pipeline used to bind textures.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="pipeline"/> parameter cannot be null.
    /// </exception>
    public TextureBinder(IPipeline pipeline)
    {
        this.pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        this.textureToSlotMap = new Dictionary<ITexture, int>(pipeline.MaxTextureSlots);
    }

    /// <summary>
    ///   Gets a value indicating whether the <see cref="Reset"/> method should be called.
    /// </summary>
    /// <value>
    ///   <c>true</c> if should be reset; otherwise, <c>false</c>.
    /// </value>
    /// <seealso cref="Reset"/>
    public bool ShouldReset
    {
        get { return this.textureToSlotMap.Count >= this.pipeline.MaxTextureSlots; }
    }

    /// <summary>
    ///   Gets the texture slot index assigned to the specified <paramref name="texture"/>, or assigns an index to the specified <paramref name="texture"/> and returns the value.
    /// </summary>
    /// <param name="texture">
    ///   The texture to find the index of.
    /// </param>
    /// <returns>
    ///   The texture slot index for the specified <paramref name="texture"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="texture"/> parameter cannot be null.
    /// </exception>
    public int GetTextureSlotIndex(ITexture texture)
    {
        if (texture == null)
        {
            throw new ArgumentNullException(nameof(texture));
        }

        if (this.textureToSlotMap.TryGetValue(texture, out int value))
        {
            return value;
        }

        int slot = this.textureToSlotMap.Count;

        this.pipeline.SetTexture(texture, slot);
        this.pipeline.SetUniform($"u_textures[{slot}]", slot);

        this.textureToSlotMap.Add(texture, slot);

        return slot;
    }

    /// <summary>
    ///   Resets the texture binder by clearing all binded textures from it's cache.
    /// </summary>
    /// <remarks>
    ///   The currently bound textures are <c>not</c> unbound when this method is called.
    /// </remarks>
    public void Reset()
    {
        this.textureToSlotMap.Clear();
    }
}
