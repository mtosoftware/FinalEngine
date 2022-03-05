// <copyright file="INewProjectViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Windows.Input;
    using FinalEngine.Editor.ViewModels.Events;

    /// <summary>
    ///   Defines an interface that represents a new project view.
    /// </summary>
    public interface INewProjectViewModel
    {
        /// <summary>
        ///   Occurs when a new project is created.
        /// </summary>
        event EventHandler<NewProjectEventArgs>? ProjectCreated;

        /// <summary>
        ///   Gets the browse command.
        /// </summary>
        /// <value>
        ///   The browse command.
        /// </value>
        ICommand BrowseCommand { get; }

        /// <summary>
        ///   Gets the create command.
        /// </summary>
        /// <value>
        ///   The create command.
        /// </value>
        ICommand CreateCommand { get; }

        /// <summary>
        ///   Gets or sets the project location.
        /// </summary>
        /// <value>
        ///   The project location.
        /// </value>
        string ProjectLocation { get; set; }

        /// <summary>
        ///   Gets or sets the name of the project.
        /// </summary>
        /// <value>
        ///   The name of the project.
        /// </value>
        string ProjectName { get; set; }
    }
}