// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Editor.Common.Services.Factories;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage(Justification = "Extensions")]
public static class ServiceCollectionExtensions
{
    public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

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
