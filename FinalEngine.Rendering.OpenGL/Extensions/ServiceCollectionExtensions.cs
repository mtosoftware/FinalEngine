// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Extensions;

using System;
using FinalEngine.Rendering.OpenGL.Invocation;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRendering(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
        services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

        return services;
    }
}
