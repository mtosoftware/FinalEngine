// <copyright file="Project.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models
{
    using System;

    /// <summary>
    ///   Represents a game project used to store resources used throughout the lifetime of a game.
    /// </summary>
    internal class Project
    {
        /// <summary>
        ///   The location of the project on the file system.
        /// </summary>
        private string location;

        /// <summary>
        ///   The name of the project on the file system (excluding the extension).
        /// </summary>
        private string name;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="name">
        ///   The name.
        /// </param>
        /// <param name="location">
        ///   The location.
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

            this.name = name;
            this.location = location;
        }

        /// <summary>
        ///   Gets or sets the location of the project on thew file system.
        /// </summary>
        /// <value>
        ///   The location of the project on the file system.
        /// </value>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <c>value</c> parameter cannot be null, empty or consist of only whitespace characters.
        /// </exception>
        public string Location
        {
            get
            {
                return this.location;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.location = value;
            }
        }

        /// <summary>
        ///   Gets or sets the name of the project on the file system (excluding the extension).
        /// </summary>
        /// <value>
        ///   The name of the project on the file system.
        /// </value>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <c>value</c> parameter cannot be null, empty or consist of only whitespace characters.
        /// </exception>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.name = value;
            }
        }
    }
}