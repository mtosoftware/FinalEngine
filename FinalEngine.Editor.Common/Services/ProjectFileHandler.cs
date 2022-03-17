// <copyright file="ProjectFileHandler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
    using System;
    using System.IO;
    using System.Text.Json;
    using FinalEngine.Editor.Common.Events;
    using FinalEngine.Editor.Common.Exceptions;
    using FinalEngine.Editor.Common.Models;
    using FinalEngine.IO;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IProjectFileHandler"/>.
    /// </summary>
    /// <seealso cref="IProjectFileHandler"/>
    public class ProjectFileHandler : IProjectFileHandler
    {
        /// <summary>
        ///   The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger<ProjectFileHandler> logger;

        /// <summary>
        ///   The currently opened project, or <c>null</c> if one has not been opened.
        /// </summary>
        private Project? project;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectFileHandler"/> class.
        /// </summary>
        /// <param name="fileSystem">
        ///   The file system used to open and save projects.
        /// </param>
        /// <param name="logger">
        ///   The logger.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="fileSystem"/> or <paramref name="logger"/> parameter cannot be null.
        /// </exception>
        public ProjectFileHandler(IFileSystem fileSystem, ILogger<ProjectFileHandler> logger)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public event EventHandler<ProjectChangedEventArgs> ProjectChanged;

        /// <summary>
        ///   Creates a new project with specified <paramref name="name"/> at the specified <paramref name="location"/> and opens it.
        /// </summary>
        /// <param name="name">
        ///   The name of the project.
        /// </param>
        /// <param name="location">
        ///   The location of the project on the file system.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> or <paramref name="location"/> parameter cannot be null, empty or consist of only whitespace characters.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="name"/> parameter is not a valid file name or the specified <paramref name="location"/> parameter is not a valid directory.
        /// </exception>
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

            this.project = new Project()
            {
                Name = name,
                Location = directoryPath,
            };

            this.SaveProject();
            this.OpenProject(fullPath);
        }

        /// <summary>
        ///   Opens the project at the specified <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">
        ///   The full path/location of the project to open.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="fullPath"/> parameter cannot be null, empty or consist of only whitespace characters.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///   The project file could not be located at the specified <paramref name="fullPath"/>.
        /// </exception>
        public void OpenProject(string fullPath)
        {
            this.logger.LogInformation("Opening project...");

            if (string.IsNullOrWhiteSpace(fullPath))
            {
                throw new ArgumentNullException(nameof(fullPath));
            }

            if (!this.fileSystem.FileExists(fullPath))
            {
                throw new FileNotFoundException($"The specified project file couldn't be located at path: {fullPath}", fullPath);
            }

            using (Stream stream = this.fileSystem.OpenFile(fullPath, FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    this.logger.LogInformation("Reading project file...");

                    this.project = JsonSerializer.Deserialize<Project>(reader.ReadToEnd());

                    if (this.project == null)
                    {
                        string message = $"Failed to parse project file at location: {fullPath}.";
                        var exception = new JsonException(message);

                        this.logger.LogError(message);
                        throw exception;
                    }

                    string? directoryPath = Path.GetDirectoryName(fullPath);

                    if (string.IsNullOrWhiteSpace(directoryPath))
                    {
                        throw new InvalidOperationException($"Failed to change project directory to path: {fullPath}");
                    }

                    if (this.project.Location != directoryPath)
                    {
                        this.project.Location = directoryPath;
                    }
                }
            }

            this.ProjectChanged?.Invoke(this, new ProjectChangedEventArgs(this.project.Name, this.project.Location));
        }

        /// <summary>
        ///   Saves the currently opened project.
        /// </summary>
        public void SaveProject()
        {
            this.logger.LogInformation("Saving project...");

            if (this.project == null)
            {
                throw new InvalidProgramException(nameof(this.project));
            }

            using (Stream stream = this.fileSystem.CreateFile(GetPotentialProjectFilePath(this.project.Name, this.project.Location)))
            {
                using (var writer = new StreamWriter(stream))
                {
                    this.logger.LogInformation("Writing to project file...");

                    string result = JsonSerializer.Serialize(this.project);
                    writer.Write(result);
                }
            }
        }

        /// <summary>
        ///   Gets the potential project file path.
        /// </summary>
        /// <param name="name">
        ///   The name of the project.
        /// </param>
        /// <param name="location">
        ///   The location of the project on the file system.
        /// </param>
        /// <returns>
        ///   The potential project file path.
        /// </returns>
        private static string GetPotentialProjectFilePath(string name, string location)
        {
            return $"{location}\\{name}\\{name}.feproj";
        }
    }
}