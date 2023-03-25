// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.Common.Services.Rendering;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for an <see cref="IServiceCollection"/>.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Extension Methods")]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all common services to specified <paramref name="services"/> collection.
    /// </summary>
    /// <param name="services">
    /// The services collection.
    /// </param>
    /// <returns>
    /// The <paramref name="services"/> collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="services"/> parameter cannot be null.
    /// </exception>
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<ISceneRenderer, SceneRenderer>();

        return services;
    }

    /// <summary>
    /// Adds a factory that creates an instance of type <typeparamref name="TViewModel"/> to the specified <paramref name="services"/> collection.
    /// </summary>
    /// <typeparam name="TViewModel">
    /// The type of instance to create.
    /// </typeparam>
    /// <param name="services">
    /// The services collection.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="services"/> parameter cannot be null.
    /// </exception>
    public static void AddFactory<TViewModel>(this IServiceCollection services)
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

        services.AddSingleton<IFactory<TViewModel>, Factory<TViewModel>>();
    }
}
