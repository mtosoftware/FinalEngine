// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Extensions;

using System;
using System.IO.Abstractions;
using FinalEngine.Editor.Desktop.Views.Scenes;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEditorPlatform(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IKeyboardDevice>(SceneView.KeyboardDevice);
        services.AddSingleton<IMouseDevice>(SceneView.MouseDevice);

        return services;
    }
}
