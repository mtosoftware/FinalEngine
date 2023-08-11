// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Editor.Common.Services.Factories;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for an <see cref="IServiceCollection"/>.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Extensions")]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds an <see cref="IFactory{T}"/> that can create a <typeparamref name="TService"/> to the specified <paramref name="services"/>.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of the service to register.
    /// </typeparam>
    /// <typeparam name="TImplementation">
    /// The type of service implementation.
    /// </typeparam>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> used to register the service.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="services"/> parameter cannot be null.
    /// </exception>
    public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<TService, TImplementation>();
        services.AddSingleton<Func<TService>>(x =>
        {
            return () =>
            {
                return x.GetRequiredService<TService>();
            };
        });

        services.AddSingleton<IFactory<TService>, Factory<TService>>();
    }
}
