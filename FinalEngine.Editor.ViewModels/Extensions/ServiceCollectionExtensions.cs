// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Extensions;

using System;
using FinalEngine.Editor.ViewModels.Interaction;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddViewModelFactory<TViewModel>(this IServiceCollection services)
        where TViewModel : class
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<TViewModel>();
        services.AddSingleton<Func<TViewModel>>(x =>
        {
            return () =>
            {
                return x.GetRequiredService<TViewModel>();
            };
        });

        services.AddSingleton<IAbstractFactory<TViewModel>, AbstractFactory<TViewModel>>();
    }
}
