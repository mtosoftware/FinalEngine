// <copyright file="IInputLayout.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers
{
    using System.Collections.Generic;

    /// <summary>
    ///   Defines an interface that represents an input layout that describes the formating of vertex buffer data.
    /// </summary>
    public interface IInputLayout
    {
        /// <summary>
        ///   Gets the elements that describe the formating of vertex buffer data for this <see cref="IInputLayout"/>.
        /// </summary>
        /// <value>
        ///   The elements that describe the formating of vertex buffer data for this <see cref="IInputLayout"/>.
        /// </value>
        IEnumerable<InputElement> Elements { get; }
    }
}
