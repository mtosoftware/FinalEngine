// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Extensions;

using System;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddResourceLoader<TResource, TResourceLoader>(this IServiceCollection services)
        where TResource : IResource
        where TResourceLoader : ResourceLoaderBase<TResource>
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<ResourceLoaderBase<TResource>, TResourceLoader>();

        services.AddSingleton<IResourceLoader>(x =>
        {
            return x.GetRequiredService<ResourceLoaderBase<TResource>>();
        });

        return services;
    }

    public static IServiceCollection AddResourceManager(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<IResourceLoaderFetcher, ResourceLoaderFetcher>();
        services.AddSingleton<IResourceManager>(ResourceManager.Instance);

        return services;
    }
}
