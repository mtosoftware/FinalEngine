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
    public static void AddDesktopPlatform(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<INativeWindowInvoker>(x =>
        {
            return new NativeWindowInvoker(new NativeWindowSettings()
            {
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 6),

                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,

                AutoLoadBindings = false,
                StartVisible = false,

                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,

                Size = new Vector2i(1280, 720),
                NumberOfSamples = 8,
            });
        });

        services.AddSingleton<IWindow, OpenTKWindow>();

        services.AddSingleton<IEventsProcessor>(x =>
        {
            return (IEventsProcessor)x.GetRequiredService<IWindow>();
        });

        services.AddSingleton<IKeyboardDevice, OpenTKKeyboardDevice>();
        services.AddSingleton<IMouseDevice, OpenTKMouseDevice>();
    }
}
