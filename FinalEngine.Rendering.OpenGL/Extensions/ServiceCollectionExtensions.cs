// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Extensions;

using System;
using FinalEngine.Rendering.OpenGL.Invocation;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenGL(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();

        services.AddSingleton<IRenderContext, OpenGLRenderContext>();
        services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();
        services.AddSingleton<IRenderPipeline, OpenGLRenderPipeline>();

        services.AddSingleton<IGPUResourceFactory>(x =>
        {
            return x.GetRequiredService<IRenderDevice>().Factory;
        });

        services.AddSingleton<IInputAssembler>(x =>
        {
            return x.GetRequiredService<IRenderDevice>().InputAssembler;
        });

        services.AddSingleton<IOutputMerger>(x =>
        {
            return x.GetRequiredService<IRenderDevice>().OutputMerger;
        });

        services.AddSingleton<IPipeline>(x =>
        {
            return x.GetRequiredService<IRenderDevice>().Pipeline;
        });

        services.AddSingleton<IRasterizer>(x =>
        {
            return x.GetRequiredService<IRenderDevice>().Rasterizer;
        });

        return services;
    }
}
