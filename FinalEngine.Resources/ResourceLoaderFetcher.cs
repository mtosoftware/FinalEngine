// <copyright file="ResourceLoaderFetcher.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

internal sealed class ResourceLoaderFetcher : IResourceLoaderFetcher
{
    private readonly IServiceProvider serviceProvider;

    public ResourceLoaderFetcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IEnumerable<IResourceLoader> GetResourceLoaders()
    {
        return this.serviceProvider.GetRequiredService<IEnumerable<IResourceLoader>>();
    }
}
