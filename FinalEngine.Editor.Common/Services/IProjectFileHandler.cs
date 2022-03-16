// <copyright file="IProjectFileHandler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
    using System;
    using FinalEngine.Editor.Common.Events;

    /// <summary>
    ///   Defines an interface that provides functionality for handling projects.
    /// </summary>
    public interface IProjectFileHandler
    {
        event EventHandler<ProjectChangedEventArgs> ProjectChanged;

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
        void OpenProject(string fullPath);

        /// <summary>
        ///   Saves the currently opened project.
        /// </summary>
        void SaveProject();
    }
}