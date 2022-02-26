// <copyright file="ProjectFileHandler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
    using System;
    using System.IO;
    using System.Text.Json;
    using FinalEngine.Editor.Common.Exceptions;
    using FinalEngine.Editor.Common.Models;
    using FinalEngine.IO;
    using Microsoft.Extensions.Logging;

    public class ProjectFileHandler : IProjectFileHandler
    {
        private readonly IApplicationContext context;

        private readonly IFileSystem fileSystem;

        private readonly ILogger<ProjectFileHandler> logger;

        public ProjectFileHandler(IApplicationContext context, IFileSystem fileSystem, ILogger<ProjectFileHandler> logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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

            if (!this.fileSystem.DirectoryExists(directoryPath))
            {
                this.logger.LogInformation("Creating a new project directory...");
                this.fileSystem.CreateDirectory(directoryPath);
            }

            string fullPath = GetPotentialProjectFilePath(name, location);

            if (this.fileSystem.FileExists(fullPath))
            {
                string message = $"A project already exists with that name at the specified {nameof(fullPath)}.";
                var exception = new ProjectExistsException(message, fullPath);

                this.logger.LogError(message, exception);
                throw exception;
            }

            var project = new Project(name, location);

            this.SaveProject(project);
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
                throw new FileNotFoundException($"The specified project couldn't be located at path: {fullPath}", fullPath);
            }

            using (Stream stream = this.fileSystem.OpenFile(fullPath, FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    this.logger.LogInformation("Reading project file...");

                    Project? project = JsonSerializer.Deserialize<Project>(reader.ReadToEnd());

                    if (project == null)
                    {
                        string message = $"Failed to parse project file at location: {fullPath}.";
                        var exception = new JsonException(message);

                        this.logger.LogError(message);
                        throw exception;
                    }

                    this.context.SetCurrentProject(ApplicationContext.Guid, project);
                }
            }
        }

        public void SaveProject(Project project)
        {
            this.logger.LogInformation("Saving project...");

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            using (Stream stream = this.fileSystem.CreateFile(GetPotentialProjectFilePath(project.Name, project.Location)))
            {
                using (var writer = new StreamWriter(stream))
                {
                    this.logger.LogInformation("Writing to project file...");

                    string result = JsonSerializer.Serialize(project);
                    writer.Write(result);
                }
            }
        }

        private static string GetPotentialProjectFilePath(string name, string location)
        {
            return $"{location}\\{name}\\{name}.feproj";
        }
    }
}