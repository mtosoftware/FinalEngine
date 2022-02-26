// <copyright file="IApplicationContextService.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
    using System;
    using FinalEngine.Editor.Common.Models;

    public interface IApplicationContext
    {
        Project? Project { get; }

        void SetCurrentProject(Guid guid, Project project);
    }
}