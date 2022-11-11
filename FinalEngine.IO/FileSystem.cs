// <copyright file="FileSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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

        private readonly IDictionary<string, string> nameToConentMap;

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
        public FileSystem(IFileInvoker file, IDirectoryInvoker directory)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
            this.directory = directory ?? throw new ArgumentNullException(nameof(directory));
            this.nameToConentMap = new Dictionary<string, string>();
        }

        public void AddVirtualTextFile(string name, string filePath)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
            }

            if (!this.FileExists(filePath))
            {
                throw new FileNotFoundException($"Failed to locate file at path: '{filePath}'.");
            }

            if (this.nameToConentMap.ContainsKey(name))
            {
                throw new ArgumentException($"A virtual file has already been added with the specified {nameof(name)}.", nameof(name));
            }

            using (var stream = this.OpenFile(filePath, FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    this.nameToConentMap.Add(name, content);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public void CreateDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
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
                throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
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
                throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
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
                throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
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
                throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
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
                throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
            }

            return this.file.Exists(path);
        }

        public string GetVirtualTextFile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.nameToConentMap.TryGetValue(name, out string? content))
            {
                throw new ArgumentException($"A virtual file that matches the specified {nameof(name)} couldn't be located.", nameof(name));
            }

            return content;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="path"/> parameter is null, empty of contains only whitespace characters.
        /// </exception>
        public Stream OpenFile(string path, FileAccessMode mode)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"The specified {nameof(path)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(path));
            }

            var access = mode switch
            {
                FileAccessMode.Read => FileAccess.Read,
                FileAccessMode.Write => FileAccess.Write,
                FileAccessMode.ReadAndWrite => FileAccess.ReadWrite,
                _ => FileAccess.Read,
            };

            return this.file.Open(path, FileMode.Open, access);
        }

        public void RemoveVirtualTextFile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.nameToConentMap.TryGetValue(name, out _))
            {
                throw new ArgumentException($"A virtual file that matches the specified {nameof(name)} couldn't be located.", nameof(name));
            }

            this.nameToConentMap.Remove(name);
        }
    }
}
