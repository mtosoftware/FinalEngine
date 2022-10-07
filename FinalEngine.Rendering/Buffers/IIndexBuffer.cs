// <copyright file="IIndexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers
{
    using System;

    /// <summary>
    ///   Defines an interface that represents an index buffer.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public interface IIndexBuffer : IDisposable
    {
        /// <summary>
        ///   Gets an <see cref="int"/> that represents the total amount of indices contained in this <see cref="IIndexBuffer"/>.
        /// </summary>
        /// <value>
        ///   The total amount of indices contained in this <see cref="IIndexBuffer"/>.
        /// </value>
        int Length { get; }

        /// <summary>
        ///   Gets the usage type for this <see cref="IIndexBuffer"/>.
        /// </summary>
        /// <value>
        ///   The usage type for this <see cref="IIndexBuffer"/>.
        /// </value>
        BufferUsageType Type { get; }
    }
}
