// <copyright file="LayoutSerializerFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Factories;

using System;
using System.IO.Abstractions;
using System.Windows;
using AvalonDock;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Desktop.Interactions;
using FinalEngine.Editor.Desktop.Views.Docking;
using FinalEngine.Editor.ViewModels.Factories;
using FinalEngine.Editor.ViewModels.Interactions;
using MahApps.Metro.Controls;

public sealed class LayoutManagerFactory : ILayoutManagerFactory
{
    // Easier to cache, plus when unloading the DockView MainWindow throws a NullReferenceException.
    private static readonly DockingManager Instance = Application.Current.MainWindow.FindChild<DockView>().DockManager;

    private readonly IApplicationContext application;

    private readonly IFileSystem fileSystem;

    public LayoutManagerFactory(IApplicationContext application, IFileSystem fileSystem)
    {
        this.application = application ?? throw new ArgumentNullException(nameof(application));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public ILayoutManager CreateManager()
    {
        return new LayoutSerializer(Instance, this.application, this.fileSystem);
    }
}
