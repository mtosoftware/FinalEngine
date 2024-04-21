// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Extensions;

using System;
using FinalEngine.Audio.OpenAL.Extensions;
using FinalEngine.Platform.Desktop.Extensions;
using FinalEngine.Rendering.OpenGL.Extensions;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRuntime<TGame>(this IServiceCollection services)
        where TGame : GameContainerBase
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddCoreRuntime<TGame>();

        services.AddOpenAL();
        services.AddOpenGL();

        services.AddPlatform();

        return services;
    }
}
