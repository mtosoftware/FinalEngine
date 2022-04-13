// <copyright file="ProjectChangedMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages
{
    using System;
    using FinalEngine.Editor.Common.Models;

    /// <summary>
    ///   Provides data for a message to be sent when the currently opened project has changed to a different project.
    /// </summary>
    public class ProjectChangedMessage
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectChangedMessage"/> class.
        /// </summary>
        /// <param name="project">
        ///   The project to has caused the message to be sent.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="project"/> parameter cannot be null.
        /// </exception>
        public ProjectChangedMessage(Project project)
        {
            this.Project = project ?? throw new ArgumentNullException(nameof(project));
        }

        /// <summary>
        ///   Gets the project.
        /// </summary>
        /// <value>
        ///   The project.
        /// </value>
        public Project Project { get; }
    }
}