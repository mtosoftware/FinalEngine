// <copyright file="ProjectChangedEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Events
{
    using System;

    public class ProjectChangedEventArgs : EventArgs
    {
        public ProjectChangedEventArgs(string name, string location)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public string Location { get; }

        public string Name { get; }
    }
}