// <copyright file="ApplicationContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;
using System.IO.Abstractions;
using System.Reflection;

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
            string directory = this.fileSystem.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Final Engine");

            if (!this.fileSystem.Directory.Exists(directory))
            {
                this.fileSystem.Directory.CreateDirectory(directory);
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
