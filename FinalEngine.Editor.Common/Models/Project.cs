// <copyright file="Project.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models
{
    using System;

    /// <summary>
    ///   Represents a game project used to store resources used throughout the lifetime of a game.
    /// </summary>
    public class Project
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="name">
        ///   The name of the project.
        /// </param>
        /// <param name="location">
        ///   The location of the project on the file system.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="name"/> or <paramref name="location"/> parameter cannot be null, empty or consist of only whitespace characters.
        /// </exception>
        public Project(string name, string location)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            this.Name = name;
            this.Location = location;
        }

        /// <summary>
        ///   Gets the location of the project on the file system.
        /// </summary>
        /// <value>
        ///   The location of the project on the file system.
        /// </value>
        public string Location { get; }

        /// <summary>
        ///   Gets the name of the project.
        /// </summary>
        /// <value>
        ///   The name of the project.
        /// </value>
        public string Name { get; }
    }
}