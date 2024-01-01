// <copyright file="PixelType.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Required by API")]
public enum PixelType
{
    Int,

    Byte,

    Short,

    Float,
}
