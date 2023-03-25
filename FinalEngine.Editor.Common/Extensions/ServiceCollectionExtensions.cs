// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Extensions;

using System;
using FinalEngine.Editor.Common.Services.Rendering;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<ISceneRenderer, SceneRenderer>();

        return services;
    }
}
