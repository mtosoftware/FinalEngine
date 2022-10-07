// <copyright file="ITexture2D.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures
{
    /// <summary>
    ///   Defines an interface that represents a two-dimensional texture.
    /// </summary>
    /// <seealso cref="ITexture"/>
    public interface ITexture2D : ITexture
    {
        /// <summary>
        ///   Gets the description that describes the properties of this <see cref="ITexture2D"/>.
        /// </summary>
        /// <value>
        ///   The description that describes the properties of this <see cref="ITexture2D"/>.
        /// </value>
        Texture2DDescription Description { get; }
    }
}
