// <copyright file="PixelFormat.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures
{
    /// <summary>
    ///   Enumerates the available pixel formats.
    /// </summary>
    public enum PixelFormat
    {
        /// <summary>
        ///   Specifies each component is an 8-bit red component.
        /// </summary>
        R,

        /// <summary>
        ///   Specifies each component is an 8-bit red-green component.
        /// </summary>
        Rg,

        /// <summary>
        ///   Specifies each component is an 8-bit red-green-blue component.
        /// </summary>
        Rgb,

        /// <summary>
        ///   Specifies each component is an 8-bit red-green-blue-alpha component.
        /// </summary>
        Rgba,

        /// <summary>
        ///   Specifies each component is a depth component.
        /// </summary>
        Depth,

        /// <summary>
        ///   Specifies each component is a depth-stencil component.
        /// </summary>
        DepthAndStencil,
    }

    /// <summary>
    ///   Enumerates the available sized internal formats.
    /// </summary>
    public enum SizedFormat
    {
        /// <summary>
        ///   Specifies each component is a 8-bit red component.
        /// </summary>
        R8,

        /// <summary>
        ///   Specifies each component is an 8-bit red-green component.
        /// </summary>
        Rg8,

        /// <summary>
        ///   Specifies each component is an 8-bit red-green-blue component.
        /// </summary>
        Rgb8,

        /// <summary>
        ///   Specifies each component is an 8-bit red-green-blue-alpha component.
        /// </summary>
        Rgba8,
    }
}
