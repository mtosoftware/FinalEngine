// <copyright file="PixelType.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System.Diagnostics.CodeAnalysis;

/// <summary>
///   Enumerates the available pixel data types.
/// </summary>
[SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Required by API")]
public enum PixelType
{
    /// <summary>
    ///   Specifies the data type is a 32-bit unsigned integer - or <see cref="uint"/>.
    /// </summary>
    Int,

    /// <summary>
    ///   Specifies the data type is a 8-bit unsigned integer - or <see cref="byte"/>.
    /// </summary>
    Byte,

    /// <summary>
    ///   Specifies the data type is a 16-bit unsigned integer - or <see cref="ushort"/>.
    /// </summary>
    Short,
}
