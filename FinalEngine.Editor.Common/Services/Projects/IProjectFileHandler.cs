// <copyright file="IProjectFileHandler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Projects
{
    using FinalEngine.Editor.Common.Models;

    /// <summary>
    ///   Defines an interface that provides functionality for handling projects.
    /// </summary>
    public interface IProjectFileHandler
    {
        /// <summary>
        ///   Creates a new project with specified <paramref name="name"/> at the specified <paramref name="location"/> and opens it.
        /// </summary>
        /// <param name="name">
        ///   The name of the project.
        /// </param>
        /// <param name="location">
        ///   The location of the project on the file system.
        /// </param>
        /// <returns>
        ///   The newly created, saved and opened project.
        /// </returns>
        Project CreateNewProject(string name, string location);

        /// <summary>
        ///   Opens the project at the specified <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">
        ///   The full path/location of the project to open.
        /// </param>
        /// <returns>
        ///   The project that has been opened.
        /// </returns>
        Project OpenProject(string fullPath);

        /// <summary>
        ///   Saves the currently opened project.
        /// </summary>
        /// <param name="project">
        ///   The project to save.
        /// </param>
        void SaveProject(Project project);
    }
}