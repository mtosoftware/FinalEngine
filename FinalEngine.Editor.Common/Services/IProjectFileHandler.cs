// <copyright file="IProjectFileHandler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
    public interface IProjectFileHandler
    {
        void CreateNewProject(string name, string location);

        string? OpenProject(string fullPath);

        void SaveProject();
    }
}