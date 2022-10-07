// <copyright file="IFileInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.IO;

    /// <summary>
    ///   Defines an interface that provides methods for invocation of the <see cref="File"/> class.
    /// </summary>
    public interface IFileInvoker
    {
        /// <inheritdoc cref="File.Create(string)"/>
        FileStream Create(string path);

        /// <inheritdoc cref="File.Delete(string)"/>
        void Delete(string path);

        /// <inheritdoc cref="File.Exists(string?)"/>
        bool Exists(string path);

        /// <inheritdoc cref="File.Open(string, FileMode, FileAccess)"/>
        FileStream Open(string path, FileMode mode, FileAccess access);
    }
}
