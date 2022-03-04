// <copyright file="NewProjectEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Events
{
    using System;

    public class NewProjectEventArgs
    {
        public NewProjectEventArgs(string projectName)
        {
            this.ProjectName = projectName ?? throw new ArgumentNullException(nameof(projectName));
        }

        public string ProjectName { get; }
    }
}