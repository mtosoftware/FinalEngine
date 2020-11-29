﻿// <copyright file="FileSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <summary>
    ///     Provides a standard implementation of an <see cref="IFileSystem"/>.
    /// </summary>
    /// <seealso cref="FinalEngine.IO.IFileSystem"/>
    [ExcludeFromCodeCoverage]
    public class FileSystem : IFileSystem
    {
        /// <summary>
        ///     Creates a directory at the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        ///     Specifies the path of the directory to create.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     The specified <paramref name="path"/> parameter is null.
        /// </exception>
        public void CreateDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
            }

            Directory.CreateDirectory(path);
        }

        /// <summary>
        ///     Creates a file with explicit read/write access and returns it as a <see
        ///     cref="Stream"/>.
        /// </summary>
        /// <param name="path">
        ///     Specifies the path and name of the file to create.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream"/> representation of the file.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The specified <paramref name="path"/> parameter is null.
        /// </exception>
        public Stream CreateFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
            }

            return File.Create(path);
        }

        /// <summary>
        ///     Deletes the directory at the specified <paramref name="path"/>, including any and
        ///     all subdirectories and files.
        /// </summary>
        /// <param name="path">
        ///     Specifies the path of the directory to delete.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     The specified <paramref name="path"/> parameter is null.
        /// </exception>
        public void DeleteDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
            }

            Directory.Delete(path, true);
        }

        /// <summary>
        ///     Deletes the file at the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">
        ///     Specifies the path and name of the file to delete.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///     The specified <paramref name="path"/> parameter is null.
        /// </exception>
        public void DeleteFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
            }

            File.Delete(path);
        }

        /// <summary>
        ///     Determines whether a directory located at the specified <paramref name="path"/>
        ///     exists.
        /// </summary>
        /// <param name="path">
        ///     Specifies the path of the directory to check.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the directory path exists; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The specified <paramref name="path"/> parameter is null.
        /// </exception>
        public bool DirectoryExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
            }

            return Directory.Exists(path);
        }

        /// <summary>
        ///     Determines whether a file located at the specified <paramref name="path"/> exists.
        /// </summary>
        /// <param name="path">
        ///     Specifies the path and name of the file to check.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the file exists; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The specified <paramref name="path"/> parameter is null.
        /// </exception>
        public bool FileExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
            }

            return File.Exists(path);
        }

        /// <summary>
        ///     Opens a file located at the specified <paramref name="path"/>, with the specified
        ///     <paramref name="mode"/>.
        /// </summary>
        /// <param name="path">
        ///     Specifies the path and name of the file to open.
        /// </param>
        /// <param name="mode">
        ///     Specifies the file access mode of the file.
        /// </param>
        /// <returns>
        ///     The <see cref="Stream"/> representation of the file.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     The specified <paramref name="path"/> parameter is null.
        /// </exception>
        public Stream OpenFile(string path, FileAccessMode mode)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
            }

            FileAccess access = FileAccess.Read;

            switch (mode)
            {
                case FileAccessMode.Read:
                    access = FileAccess.Read;
                    break;

                case FileAccessMode.Write:
                    access = FileAccess.Write;
                    break;

                case FileAccessMode.ReadAndWrite:
                    access = FileAccess.ReadWrite;
                    break;
            }

            return File.Open(path, FileMode.Open, access);
        }
    }
}