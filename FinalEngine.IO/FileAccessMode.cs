// <copyright file="FileAccessMode.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO;

/// <summary>
/// Enumerates the available file access modes for the <see cref="IFileSystem.OpenFile(string, FileAccessMode)"/> method.
/// </summary>
public enum FileAccessMode
{
    /// <summary>
    /// The file stream is read-only.
    /// </summary>
    Read,

    /// <summary>
    /// The file stream is write-only.
    /// </summary>
    Write,

    /// <summary>
    /// The file stream can be both read from and written too.
    /// </summary>
    ReadAndWrite,
}
