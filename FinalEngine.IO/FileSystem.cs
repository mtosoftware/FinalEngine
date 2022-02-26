// <copyright file="FileSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FinalEngine.IO.Invocation;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IFileSystem"/>.
    /// </summary>
    /// <seealso cref="IFileSystem"/>
    public class FileSystem : IFileSystem
    {
        /// <summary>
        ///   The directory invoker.
        /// </summary>
        private readonly IDirectoryInvoker directory;

        /// <summary>
        ///   The file invoker.
        /// </summary>
        private readonly IFileInvoker file;

        private readonly IPathInvoker path;

        /// <summary>
        ///   Initializes a new instance of the <see cref="FileSystem"/> class.
        /// </summary>
        public FileSystem()
            : this(new FileInvoker(), new DirectoryInvoker(), new PathInvoker())
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="FileSystem"/> class.
        /// </summary>
        /// <param name="file">
        ///   Specifies a <see cref="IFileInvoker"/> that represents the invoker used to handle file operations.
        /// </param>
        /// <param name="directory">
        ///   Specifies a <see cref="IDirectoryInvoker"/> that represents the invoker used to handle directory operations.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="file"/> or <paramref name="directory"/> parameter is null.
        /// </exception>
        public FileSystem(IFileInvoker file, IDirectoryInvoker directory, IPathInvoker path)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file), $"The specified {nameof(file)} parameter cannot be null.");
            this.directory = directory ?? throw new ArgumentNullException(nameof(directory), $"The specified {nameof(directory)} parameter cannot be null.");
            this.path = path ?? throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null.");
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public void CreateDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null, empty or contain only whitespace characters");
            }

            this.directory.CreateDirectory(path);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public Stream CreateFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null, empty or contain only whitespace characters");
            }

            return this.file.Create(path);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public void DeleteDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null, empty or contain only whitespace characters");
            }

            this.directory.Delete(path, true);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public void DeleteFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null, empty or contain only whitespace characters");
            }

            this.file.Delete(path);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public bool DirectoryExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null, empty or contain only whitespace characters");
            }

            return this.directory.Exists(path);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public bool FileExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null, empty or contain only whitespace characters");
            }

            return this.file.Exists(path);
        }

        public bool IsValidDirectory(string directory)
        {
            if (directory == null)
            {
                return false;
            }

            IEnumerable<char> invalidCharacters = this.path.GetInvalidPathChars();

            foreach (char c in directory)
            {
                if (invalidCharacters.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsValidFileName(string fileName)
        {
            if (fileName == null)
            {
                return false;
            }

            IEnumerable<char>? invalidCharacters = this.path.GetInvalidFileNameChars();

            foreach (char c in fileName)
            {
                if (invalidCharacters.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public Stream OpenFile(string path, FileAccessMode mode)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path), $"The specified {nameof(path)} parameter cannot be null, empty or contain only whitespace characters");
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

            return this.file.Open(path, FileMode.Open, access);
        }
    }
}