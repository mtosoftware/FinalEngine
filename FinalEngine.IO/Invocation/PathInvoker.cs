// <copyright file="PathInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IPathInvoker"/>.
    /// </summary>
    /// <seealso cref="IPathInvoker"/>
    public class PathInvoker : IPathInvoker
    {
        /// <inheritdoc cref="Path.GetInvalidFileNameChars"/>
        public IEnumerable<char> GetInvalidFileNameChars()
        {
            return Path.GetInvalidFileNameChars();
        }

        /// <inheritdoc cref="Path.GetInvalidPathChars"/>
        public IEnumerable<char> GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }
    }
}