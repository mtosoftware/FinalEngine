// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Extensions;

using System;
using System.IO.Abstractions;
using FinalEngine.Audio.OpenAL.Extensions;
using FinalEngine.Input.Extensions;
using FinalEngine.Rendering.Extensions;
using FinalEngine.Rendering.OpenGL.Extensions;
using FinalEngine.Resources.Extensionss;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRuntime(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IRuntimeContext, RuntimeContext>();

        services.AddOpenGL();
        services.AddOpenAL();
        services.AddInput();
        services.AddRendering();
        services.AddResourceManagement();

        return services;
    }
}
