// <copyright file="IProjectFileHandler.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Projects;

public interface IProjectFileHandler
{
    void CreateNewProject(string name, string location);

    void OpenProject(string fullPath);

    void SaveProject(string projectDirectoryPath);
}
