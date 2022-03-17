// <copyright file="Project.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models
{
    /// <summary>
    ///   Represents a game project used to store resources used throughout the lifetime of a game.
    /// </summary>
    internal class Project
    {
        /// <summary>
        ///   Gets the location of the project on the file system.
        /// </summary>
        /// <value>
        ///   The location of the project on the file system.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        ///   Gets the name of the project.
        /// </summary>
        /// <value>
        ///   The name of the project.
        /// </value>
        public string Name { get; set; }
    }
}