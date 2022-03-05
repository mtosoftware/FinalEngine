// <copyright file="IProjectFileHandler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
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
        void CreateNewProject(string name, string location);

        /// <summary>
        ///   Opens the project at the specified <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">
        ///   The full path/location of the project to open.
        /// </param>
        /// <returns>
        ///   The name of the project that was opened; otherwise, <c>null</c>.
        /// </returns>
        string OpenProject(string fullPath);

        /// <summary>
        ///   Saves the currently opened project.
        /// </summary>
        void SaveProject();
    }
}