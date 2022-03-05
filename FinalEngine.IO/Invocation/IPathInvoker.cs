// <copyright file="IPathInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///   Defines an interface that provides for invocation of the <see cref="Path"/> class.
    /// </summary>
    public interface IPathInvoker
    {
        /// <inheritdoc cref="Path.GetInvalidFileNameChars"/>
        IEnumerable<char> GetInvalidFileNameChars();

        /// <inheritdoc cref="Path.GetInvalidPathChars"/>
        IEnumerable<char> GetInvalidPathChars();
    }
}