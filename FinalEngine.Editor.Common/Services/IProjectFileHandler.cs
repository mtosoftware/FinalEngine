// <copyright file="IProjectFileHandlerService.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
    using FinalEngine.Editor.Common.Models;

    public interface IProjectFileHandler
    {
        void CreateNewProject(string name, string location);

        void OpenProject(string location);

        void SaveProject(Project project);
    }
}