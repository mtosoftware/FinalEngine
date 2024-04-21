// <copyright file="EntitySystemResolver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Resolving;

using System;
using Microsoft.Extensions.DependencyInjection;

internal sealed class EntitySystemResolver : IEntitySystemResolver
{
    private readonly IServiceProvider serviceProvider;

    public EntitySystemResolver(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public TSystem GetEntitySystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        return this.serviceProvider.GetRequiredService<TSystem>();
    }
}
