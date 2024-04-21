// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Extensions;

using System;
using FinalEngine.Rendering.Batching;
using FinalEngine.Rendering.Loaders.Shaders;
using FinalEngine.Rendering.Loaders.Textures;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Systems;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources.Extensions;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRendering(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<ISpriteBatcher, SpriteBatcher>();
        services.AddSingleton<ITextureBinder, TextureBinder>();
        services.AddSingleton<ISpriteDrawer, SpriteDrawer>();

        services.AddResourceLoader<IShader, ShaderResourceLoader>();
        services.AddResourceLoader<IShaderProgram, ShaderProgramResourceLoader>();
        services.AddResourceLoader<ITexture2D, Texture2DResourceLoader>();

        services.AddSingleton<SpriteRenderEntitySystem>();

        return services;
    }
}
