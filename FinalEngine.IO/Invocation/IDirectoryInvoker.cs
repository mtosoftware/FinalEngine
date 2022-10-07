// <copyright file="IDirectoryInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.IO;

    /// <summary>
    ///   Defines an interface that provides methods for invocation of the <see cref="Directory"/> class.
    /// </summary>
    public interface IDirectoryInvoker
    {
        /// <inheritdoc cref="Directory.CreateDirectory(string)"/>
        DirectoryInfo CreateDirectory(string path);

        /// <inheritdoc cref="Directory.Delete(string, bool)"/>
        void Delete(string path, bool recurse);

        /// <inheritdoc cref="Directory.Exists(string?)"/>
        bool Exists(string path);
    }
}
