// <copyright file="IViewPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services;

public interface IViewPresenter
{
    void ShowView<TViewModel>();

    void ShowView<TViewModel>(TViewModel viewModel);
}
