// <copyright file="ApplicationDataContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

public sealed class ApplicationDataContext : IApplicationDataContext
{
    private readonly IFileSystem fileSystem;

    public ApplicationDataContext(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    private string Directory
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

    private string LayoutDirectory
    {
        get
        {
            string directory = this.fileSystem.Path.Combine(this.Directory, "Layouts");

            if (!this.fileSystem.Directory.Exists(directory))
            {
                this.fileSystem.Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }

    private IEnumerable<string> LayoutNames
    {
        get
        {
            var directoryInfo = this.fileSystem.DirectoryInfo.New(this.LayoutDirectory);
            var files = directoryInfo.GetFiles("*.config", SearchOption.TopDirectoryOnly);

            return files.Select(x =>
            {
                return this.fileSystem.Path.GetFileNameWithoutExtension(x.Name);
            }).ToArray();
        }
    }

    public bool ContainsLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        return this.LayoutNames.Contains(layoutName);
    }

    public string GetLayoutPath(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        return this.fileSystem.Path.Combine(this.LayoutDirectory, $"{layoutName}.config");
    }
}
