// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Extensions;

using System;
using FinalEngine.Editor.ViewModels.Interaction;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a view model factory of type <typeparamref name="TViewModel"/> to the specified <paramref name="services"/> collection.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// The type of the view model.
    /// </typeparam>
    /// <param name="services">
    /// The services collection.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="services"/> parameter cannot be null.
    /// </exception>
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
