// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Editor.Common.Services.Projects;
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
        services.AddSingleton<ProjectContext>();

        services.AddSingleton<IProjectContext>(x =>
        {
            return x.GetRequiredService<ProjectContext>();
        });

        services.AddSingleton<IProjectFileHandler>(x =>
        {
            return x.GetRequiredService<ProjectContext>();
        });

        return services;
    }
}
