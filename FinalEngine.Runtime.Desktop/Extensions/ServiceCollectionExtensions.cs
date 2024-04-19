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
    public static IServiceCollection AddRuntime<TGame>(this IServiceCollection services, Action<GameSettings> configure)
        where TGame : GameContainerBase
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configure, nameof(configure));

        var settings = new GameSettings();
        configure(settings);

        services.AddCoreRuntime<TGame>(settings);

        services.AddOpenAL();
        services.AddOpenGL();

        services.AddPlatform(settings.PlatformSettings);

        return services;
    }
}
