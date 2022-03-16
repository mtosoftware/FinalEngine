// <copyright file="ProjectChangedEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Events
{
    using System;
    using FinalEngine.Editor.Common.Models;

    public class ProjectChangedEventArgs : EventArgs
    {
        public ProjectChangedEventArgs(Project project)
        {
            this.Project = project ?? throw new ArgumentNullException(nameof(project));
        }

        public Project Project { get; }
    }
}