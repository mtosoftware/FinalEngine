// <copyright file="ApplicationContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;
using System.IO.Abstractions;
using System.Reflection;
using FinalEngine.Editor.Common.Services.Environment;

/// <summary>
/// Provides a standard implementation of an <see cref="IApplicationContext"/>.
/// </summary>
/// <seealso cref="IApplicationContext" />
public sealed class ApplicationContext : IApplicationContext
{
    /// <summary>
    /// The environment service, used when locating the applications roaming data directory.
    /// </summary>
    private readonly IEnvironmentContext environment;

    /// <summary>
    /// The file system service, used to potentially create the applications roaming data directory.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
    /// </summary>
    /// <param name="fileSystem">
    /// The file system service, used to create the required directories for <see cref="DataDirectory"/>, if required.
    /// </param>
    /// <param name="environment">
    /// The environment, used to locate the application data folder for the roaming user.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="fileSystem"/> or <paramref name="environment"/> parameter cannot be null.
    /// </exception>
    public ApplicationContext(IFileSystem fileSystem, IEnvironmentContext environment)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Accessing <see cref="DataDirectory"/> will ensure the applications data directory is created before returning it's location.
    /// </remarks>
    public string DataDirectory
    {
        get
        {
            string directory = this.fileSystem.Path.Combine(this.environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Final Engine");

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
