// <copyright file="ProjectChangedEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Events
{
    using System;
    using FinalEngine.Editor.Common.Services;

    /// <summary>
    ///   Provides event data for the <see cref="IProjectFileHandler.ProjectChanged"/> event.
    /// </summary>
    /// <seealso cref="System.EventArgs"/>
    public class ProjectChangedEventArgs : EventArgs
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="ProjectChangedEventArgs"/> class.
        /// </summary>
        /// <param name="name">
        ///   The name.
        /// </param>
        /// <param name="location">
        ///   The location.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="name"/> or <paramref name="location"/> parameter cannot be null.
        /// </exception>
        public ProjectChangedEventArgs(string name, string location)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        /// <summary>
        ///   Gets the location of the project file.
        /// </summary>
        /// <value>
        ///   The location of the project file.
        /// </value>
        public string Location { get; }

        /// <summary>
        ///   Gets the name of the project file.
        /// </summary>
        /// <value>
        ///   The name of the project file.
        /// </value>
        public string Name { get; }
    }
}