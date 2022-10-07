// <copyright file="IFileSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO
{
    using System.IO;

    /// <summary>
    ///   Defines an interface that provides methods for handling file and directory IO operations.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        ///   Creates a directory at the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        ///   Specifies the path of the directory to create.
        /// </param>
        void CreateDirectory(string path);

        /// <summary>
        ///   Creates a file with explicit read/write access and returns it as a <see cref="Stream"/>.
        /// </summary>
        /// <param name="path">
        ///   Specifies the path and name of the file to create.
        /// </param>
        /// <returns>
        ///   The <see cref="Stream"/> representation of the file.
        /// </returns>
        Stream CreateFile(string path);

        /// <summary>
        ///   Deletes the directory at the specified <paramref name="path"/>, including any and all subdirectories and files.
        /// </summary>
        /// <param name="path">
        ///   Specifies the path of the directory to delete.
        /// </param>
        void DeleteDirectory(string path);

        /// <summary>
        ///   Deletes the file at the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        ///   Specifies the path and name of the file to delete.
        /// </param>
        void DeleteFile(string path);

        /// <summary>
        ///   Determines whether a directory located at the specified <paramref name="path"/> exists.
        /// </summary>
        /// <param name="path">
        ///   Specifies the path of the directory to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the directory path exists; otherwise, <c>false</c>.
        /// </returns>
        bool DirectoryExists(string path);

        /// <summary>
        ///   Determines whether a file is located at the specified <paramref name="path"/> exists.
        /// </summary>
        /// <param name="path">
        ///   Specifies the path and name of the file to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the file exists; otherwise, <c>false</c>.
        /// </returns>
        bool FileExists(string path);

        /// <summary>
        ///   Opens a file located at the specified <paramref name="path"/>, with the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="path">
        ///   Specifies the path and name of the file to open.
        /// </param>
        /// <param name="mode">
        ///   Specifies the file access mode of the file.
        /// </param>
        /// <returns>
        ///   The <see cref="Stream"/> representation of the file.
        /// </returns>
        Stream OpenFile(string path, FileAccessMode mode);
    }
}
