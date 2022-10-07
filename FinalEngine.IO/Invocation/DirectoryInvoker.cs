// <copyright file="DirectoryInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <summary>
    ///   Provides an implementation of an <see cref="IDirectoryInvoker"/>.
    /// </summary>
    /// <seealso cref="IDirectoryInvoker"/>
    [ExcludeFromCodeCoverage(Justification = "Invocation Class")]
    public class DirectoryInvoker : IDirectoryInvoker
    {
        /// <inheritdoc/>
        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        /// <inheritdoc/>
        public void Delete(string path, bool recurse)
        {
            Directory.Delete(path, recurse);
        }

        /// <inheritdoc/>
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
