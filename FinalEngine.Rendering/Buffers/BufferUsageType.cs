// <copyright file="BufferUsageType.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

/// <summary>
///   Enumerates the available buffer usage types.
/// </summary>
public enum BufferUsageType
{
    /// <summary>
    ///   The data contained in the buffer is static and will not change.
    /// </summary>
    Static,

    /// <summary>
    ///   The data contained in the buffer is dynamic and is subject to change.
    /// </summary>
    Dynamic,
}
