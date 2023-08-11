// <copyright file="ViewPresenter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Interactions;

/// <summary>
/// Provides a standard implementation of an <see cref="IViewPresenter"/>.
/// </summary>
/// <seealso cref="IViewPresenter" />
public sealed class ViewPresenter : IViewPresenter
{
    /// <summary>
    /// The service provider, used to request services required to show views.
    /// </summary>
    private readonly IServiceProvider provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewPresenter"/> class.
    /// </summary>
    /// <param name="provider">The service provider, used to request services required to show views.</param>
    /// <exception cref="ArgumentNullException">
    /// THe specified <paramref name="provider"/> parameter cannot be null.
    /// </exception>
    /// <remarks>
    /// The view presenter should be registered with the application inversion of control container and typically you should never need to instantiate this class manually.
    /// </remarks>
    public ViewPresenter(IServiceProvider provider)
    {
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// The specified <typeparamref name="TViewModel"/> has not been registered with an <see cref="IFactory{T}"/> of type <typeparamref name="TViewModel"/>.
    /// </exception>
    public void ShowView<TViewModel>()
    {
        if (this.provider.GetService(typeof(IFactory<TViewModel>)) is not IFactory<TViewModel> factory)
        {
            throw new ArgumentException($"The specified {nameof(TViewModel)} has not been registered with an {nameof(IFactory<TViewModel>)}.");
        }

        this.ShowView(factory.Create());
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="viewModel"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <typeparamref name="TViewModel"/> has not been registered with an <see cref="IFactory{T}"/> of type <typeparamref name="TViewModel"/>.
    /// </exception>
    public void ShowView<TViewModel>(TViewModel viewModel)
    {
        if (viewModel == null)
        {
            throw new ArgumentNullException(nameof(viewModel));
        }

        if (this.provider.GetService(typeof(IViewable<TViewModel>)) is not IViewable<TViewModel> view)
        {
            throw new ArgumentException($"The specified {nameof(TViewModel)} couldn't be converted to an {nameof(IViewable<TViewModel>)}.");
        }

        view.DataContext = viewModel;

        view.ShowDialog();
    }
}
