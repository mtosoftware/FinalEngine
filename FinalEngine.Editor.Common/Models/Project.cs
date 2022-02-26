// <copyright file="Project.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models
{
    using System;

    public class Project
    {
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

        public string Location { get; }

        public string Name { get; }
    }
}