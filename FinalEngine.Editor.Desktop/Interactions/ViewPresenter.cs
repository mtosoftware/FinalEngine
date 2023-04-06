// <copyright file="ViewPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interactions;

using System;
using FinalEngine.Editor.Desktop.Views.Projects;
using FinalEngine.Editor.ViewModels.Interaction;
using FinalEngine.Editor.ViewModels.Projects;
using Microsoft.Extensions.Logging;

//// TODO: Determine how views can know what parent view created it.
public sealed class ViewPresenter : IViewPresenter
{
    private readonly ILogger<ViewPresenter> logger;

    public ViewPresenter(ILogger<ViewPresenter> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void ShowNewProjectView(INewProjectViewModel viewModel)
    {
        if (viewModel == null)
        {
            throw new ArgumentNullException(nameof(viewModel));
        }

        var view = new NewProjectView()
        {
            DataContext = viewModel,
        };

        this.logger.LogInformation($"Showing new project view...");

        view.ShowDialog();
    }
}
