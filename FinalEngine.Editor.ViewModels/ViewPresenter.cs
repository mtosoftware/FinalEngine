// <copyright file="ViewPresenter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Interactions;
using Microsoft.Extensions.DependencyInjection;

public sealed class ViewPresenter : IViewPresenter
{
    private readonly IServiceProvider provider;

    public ViewPresenter(IServiceProvider provider)
    {
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public void ShowView<TViewModel>()
    {
        this.ShowView(this.provider.GetRequiredService<IFactory<TViewModel>>().Create());
    }

    public void ShowView<TViewModel>(TViewModel viewModel)
    {
        if (viewModel == null)
        {
            throw new ArgumentNullException(nameof(viewModel));
        }

        var view = this.provider.GetRequiredService<IViewable<TViewModel>>();
        view.DataContext = viewModel;

        view.ShowDialog();
    }
}
