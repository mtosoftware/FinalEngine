// <copyright file="ViewPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interaction
{
    using System;
    using FinalEngine.Editor.Desktop.Views;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Interaction;
    using Microsoft.Extensions.Logging;

    public class ViewPresenter : IViewPresenter
    {
        private readonly ILogger<ViewPresenter> logger;

        public ViewPresenter(ILogger<ViewPresenter> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void ShowNewProjectView(INewProjectViewModel newProjectViewModel)
        {
            if (newProjectViewModel == null)
            {
                throw new ArgumentNullException(nameof(newProjectViewModel));
            }

            var view = new NewProjectView()
            {
                DataContext = newProjectViewModel,
            };

            this.logger.LogInformation("Display new project view...");

            view.ShowDialog();
        }
    }
}