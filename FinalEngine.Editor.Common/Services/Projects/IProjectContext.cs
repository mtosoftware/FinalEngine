// <copyright file="IProjectContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Projects;

using FinalEngine.Editor.Common.Models;

public interface IProjectContext
{
    bool IsProjectLoaded { get; }

    Project Project { get; }
}
