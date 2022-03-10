// <copyright file="NewProjectEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Events
{
    using System;

    /// <summary>
    ///   Provides event data for the <see cref="INewProjectViewModel.ProjectCreated"/> event.
    /// </summary>
    public class NewProjectEventArgs : EventArgs
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="NewProjectEventArgs"/> class.
        /// </summary>
        /// <param name="projectName">
        ///   The name of the project.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="projectName"/> parameter cannot be null.
        /// </exception>
        public NewProjectEventArgs(string projectName)
        {
            this.ProjectName = projectName ?? throw new ArgumentNullException(nameof(projectName));
        }

        /// <summary>
        ///   Gets the name of the project.
        /// </summary>
        /// <value>
        ///   The name of the project.
        /// </value>
        public string ProjectName { get; }
    }
}