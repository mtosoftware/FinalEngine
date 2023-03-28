// <copyright file="ProjectContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Projects;

using System;
using System.IO;
using System.Text.Json;
using FinalEngine.Editor.Common.Exceptions.Projects;
using FinalEngine.Editor.Common.Models;
using FinalEngine.IO;
using Microsoft.Extensions.Logging;

public sealed class ProjectContext : IProjectContext, IProjectFileHandler
{
    private readonly IFileSystem fileSystem;

    private readonly ILogger<ProjectContext> logger;

    public ProjectContext(ILogger<ProjectContext> logger, IFileSystem fileSystem)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public bool IsProjectLoaded
    {
        get { return this.Project != null; }
    }

    public Project Project { get; private set; }

    public void CreateNewProject(string name, string location)
    {
        this.logger.LogInformation("Creating a new project...");

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(location))
        {
            throw new ArgumentNullException(nameof(location));
        }

        if (!this.fileSystem.IsValidFileName(name))
        {
            throw new ArgumentException($"The specified {nameof(name)} parameter is not a valid file name.", nameof(name));
        }

        if (!this.fileSystem.IsValidDirectory(location))
        {
            throw new ArgumentException($"The specified {nameof(location)} parameter is not a valid directory.", nameof(location));
        }

        string directoryPath = $"{location}\\{name}";

        // Create the project directory if doesn't exist already.
        if (!this.fileSystem.DirectoryExists(directoryPath))
        {
            this.logger.LogInformation("Creating a new project directory...");
            this.fileSystem.CreateDirectory(directoryPath);
        }

        string fullPath = GetPotentialProjectFilePath(name, directoryPath);

        if (this.fileSystem.FileExists(fullPath))
        {
            string message = $"A project already exists with that name at the specified {nameof(fullPath)}.";
            var exception = new ProjectExistsException(message, fullPath);

            this.logger.LogError(message, exception);
            throw exception;
        }

        this.Project = new Project(name);

        this.SaveProject(directoryPath);
        this.OpenProject(fullPath);
    }

    public void OpenProject(string fullPath)
    {
        this.logger.LogInformation("Opening project...");

        if (string.IsNullOrWhiteSpace(fullPath))
        {
            throw new ArgumentNullException(nameof(fullPath));
        }

        if (!this.fileSystem.FileExists(fullPath))
        {
            throw new FileNotFoundException($"The project file couldn't be located at path: {fullPath}", fullPath);
        }

        using (var stream = this.fileSystem.OpenFile(fullPath, FileAccessMode.Read))
        {
            using (var reader = new StreamReader(stream))
            {
                this.logger.LogInformation("Reading project file...");

                var project = JsonSerializer.Deserialize<Project>(reader.ReadToEnd());

                if (project == null)
                {
                    string message = $"Failed to parse project file at location: {fullPath}.";
                    var exception = new JsonException(message);

                    this.logger.LogError(message);
                    throw exception;
                }

                this.Project = project;
            }
        }
    }

    public void SaveProject(string projectDirectoryPath)
    {
        if (string.IsNullOrWhiteSpace(projectDirectoryPath))
        {
            throw new ArgumentNullException(nameof(projectDirectoryPath));
        }

        if (!this.IsProjectLoaded)
        {
            throw new InvalidOperationException($"No project file has been loaded.");
        }

        using (var stream = this.fileSystem.CreateFile(GetPotentialProjectFilePath(this.Project.Name, projectDirectoryPath)))
        {
            using (var writer = new StreamWriter(stream))
            {
                this.logger.LogInformation("Writing to project file...");

                string result = JsonSerializer.Serialize(this.Project);
                writer.Write(result);
            }
        }
    }

    private static string GetPotentialProjectFilePath(string name, string location)
    {
        return $"{location}\\{name}.feproj";
    }
}
