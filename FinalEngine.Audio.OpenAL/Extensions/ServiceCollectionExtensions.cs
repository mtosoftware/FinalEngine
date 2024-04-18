// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL.Extensions;

using System;
using FinalEngine.Audio.OpenAL.Loaders;
using FinalEngine.Resources.Extensionss;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenAL(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddResourceLoader<ISound, SoundResourceLoader>();

        return services;
    }
}
