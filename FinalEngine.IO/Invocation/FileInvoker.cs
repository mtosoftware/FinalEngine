// <copyright file="FileInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.IO;

/// <summary>
/// Provides an implementation of an <see cref="IFileInvoker"/>.
/// </summary>
/// <seealso cref="IFileInvoker"/>
[ExcludeFromCodeCoverage]
public class FileInvoker : IFileInvoker
{
    /// <inheritdoc/>
    public FileStream Create(string path)
    {
        return File.Create(path);
    }

    /// <inheritdoc/>
    public void Delete(string path)
    {
        File.Delete(path);
    }

    /// <inheritdoc/>
    public bool Exists(string path)
    {
        return File.Exists(path);
    }

    /// <inheritdoc/>
    public FileStream Open(string path, FileMode mode, FileAccess access)
    {
        return File.Open(path, mode, access);
    }
}
