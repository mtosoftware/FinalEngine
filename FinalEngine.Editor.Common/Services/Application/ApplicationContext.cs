// <copyright file="ApplicationContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;
using System.IO;
using System.Reflection;
using FinalEngine.IO;

/// <summary>
/// Provides a standard implementation of an <see cref="IApplicationContext"/>.
/// </summary>
/// <seealso cref="IApplicationContext" />
public sealed class ApplicationContext : IApplicationContext
{
    private readonly IFileSystem fileSystem;

    public ApplicationContext(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public string DataDirectory
    {
        get
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Final Engine");

            if (!this.fileSystem.DirectoryExists(directory))
            {
                this.fileSystem.CreateDirectory(directory);
            }

            return directory;
        }
    }

    /// <inheritdoc/>
    public string Title
    {
        get { return $"Final Engine - {this.Version}"; }
    }

    /// <inheritdoc/>
    public Version Version
    {
        get { return Assembly.GetExecutingAssembly().GetName().Version!; }
    }
}
