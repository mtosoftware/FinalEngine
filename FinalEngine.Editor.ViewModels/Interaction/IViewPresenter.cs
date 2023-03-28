// <copyright file="IViewPresenter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction;

using FinalEngine.Editor.ViewModels.Projects;

public interface IViewPresenter
{
    void ShowNewProjectView(INewProjectViewModel viewModel);
}
