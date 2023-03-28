// <copyright file="ViewModelFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Factories;

using System;
using Microsoft.Extensions.DependencyInjection;

public sealed class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider serviceProvider;

    public ViewModelFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public TViewModel CreateViewModel<TViewModel>()
        where TViewModel : IViewModel
    {
        return this.serviceProvider.GetRequiredService<TViewModel>();
    }
}
