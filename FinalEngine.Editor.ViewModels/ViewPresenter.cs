// <copyright file="ViewPresenter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Interactions;

public sealed class ViewPresenter : IViewPresenter
{
    private readonly IServiceProvider provider;

    public ViewPresenter(IServiceProvider provider)
    {
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public void ShowView<TViewModel>()
    {
        if (this.provider.GetService(typeof(IFactory<TViewModel>)) is not IFactory<TViewModel> factory)
        {
            throw new ArgumentException($"The specified {nameof(TViewModel)} has not been registered with an {nameof(IFactory<TViewModel>)}.");
        }

        this.ShowView(factory.Create());
    }

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
