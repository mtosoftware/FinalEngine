// <copyright file="TextureWrapMode.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

/// <summary>
///   Enumerates the available texture wrapping modes.
/// </summary>
public enum TextureWrapMode
{
    /// <summary>
    ///   Specifies the texture will be clamped to the edge.
    /// </summary>
    Clamp,

    /// <summary>
    ///   Specifies the texture will be repeated.
    /// </summary>
    Repeat,
}
