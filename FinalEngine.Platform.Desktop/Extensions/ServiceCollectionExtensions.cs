// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Extensions;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using global::OpenTK.Mathematics;
using global::OpenTK.Windowing.Common;
using global::OpenTK.Windowing.Desktop;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlatform(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<INativeWindowInvoker>(x =>
        {
            var settings = new NativeWindowSettings()
            {
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 6),

                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,

                AutoLoadBindings = false,

                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,
                Size = new Vector2i(1280, 720),

                StartVisible = true,

                NumberOfSamples = 8,
            };

            return new NativeWindowInvoker(settings);
        });

        services.AddSingleton<IGLFWGraphicsContext>(x =>
        {
            return x.GetRequiredService<INativeWindowInvoker>().Context;
        });

        services.AddSingleton<IWindow, OpenTKWindow>();
        services.AddSingleton<IKeyboardDevice, OpenTKKeyboardDevice>();
        services.AddSingleton<IMouseDevice, OpenTKMouseDevice>();

        return services;
    }
}
