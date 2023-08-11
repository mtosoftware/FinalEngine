// <copyright file="LayoutManagerFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Services.Factories.Layout;

using System;
using System.IO.Abstractions;
using System.Windows;
using AvalonDock;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Desktop.Services.Layout;
using FinalEngine.Editor.Desktop.Views.Docking;
using FinalEngine.Editor.ViewModels.Services.Factories.Layout;
using FinalEngine.Editor.ViewModels.Services.Layout;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ILayoutManagerFactory"/>.
/// </summary>
/// <seealso cref="ILayoutManagerFactory" />
public sealed class LayoutManagerFactory : ILayoutManagerFactory
{
    /// <summary>
    /// The cached instanced to the docking manager.
    /// </summary>
    /// <remarks>
    /// The instance is cached to avoid a <see cref="NullReferenceException"/> when unloading the <see cref="DockView"/>.
    /// </remarks>
    private static readonly DockingManager Instance = Application.Current.MainWindow.FindChild<DockView>().DockManager;

    /// <summary>
    /// The application context, used to instantiate the layout manager.
    /// </summary>
    private readonly IApplicationContext application;

    /// <summary>
    /// The file system, used to instantiate the layout manager.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// The layout manager logger.
    /// </summary>
    private readonly ILogger<LayoutManager> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LayoutManagerFactory"/> class.
    /// </summary>
    /// <param name="logger">
    /// The layout manager logger.
    /// </param>
    /// <param name="application">
    /// The application context, used to instantiate the layout manager.
    /// </param>
    /// <param name="fileSystem">
    /// The file system, used to instantiate the layout manager.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="application"/> or <paramref name="fileSystem"/> parameter cannot be null.
    /// </exception>
    public LayoutManagerFactory(ILogger<LayoutManager> logger, IApplicationContext application, IFileSystem fileSystem)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.application = application ?? throw new ArgumentNullException(nameof(application));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <inheritdoc/>
    public ILayoutManager CreateManager()
    {
        return new LayoutManager(this.logger, Instance, this.application, this.fileSystem);
    }
}
