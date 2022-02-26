// <copyright file="ApplicationContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services
{
    using System;
    using FinalEngine.Editor.Common.Models;
    using Microsoft.Extensions.Logging;

    public class ApplicationContext : IApplicationContext
    {
        internal static Guid Guid = Guid.NewGuid();

        private readonly ILogger<ApplicationContext> logger;

        public ApplicationContext(ILogger<ApplicationContext> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Project? Project { get; private set; }

        public void SetCurrentProject(Guid guid, Project project)
        {
            if (guid != Guid)
            {
                throw new ArgumentException($"The specified {nameof(guid)} is not valid. Are you trying to makes changes from the view model layer?");
            }

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            this.logger.LogInformation($"Setting current project to: {project.Name}...");

            this.Project = project;
        }
    }
}