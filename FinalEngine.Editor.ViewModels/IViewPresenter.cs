// <copyright file="IViewPresenter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

/// <summary>
/// Defines an interface that provides methods to create a view from a view model.
/// </summary>
public interface IViewPresenter
{
    /// <summary>
    /// Shows a view that matches the specified <typeparamref name="TViewModel"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// The type of the view model used to locate and show the view.
    /// </typeparam>
    void ShowView<TViewModel>();

    /// <summary>
    /// Shows a view that matches the specified <paramref name="viewModel"/>.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// The type of the view model used to locate and show the view.
    /// </typeparam>
    /// <param name="viewModel">
    /// The view model instance.
    /// </param>
    void ShowView<TViewModel>(TViewModel viewModel);
}
