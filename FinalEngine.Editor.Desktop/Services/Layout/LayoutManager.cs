// <copyright file="LayoutManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Services.Layout;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using AvalonDock;
using AvalonDock.Layout.Serialization;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Desktop.Exceptions.Layout;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ILayoutManager"/>.
/// </summary>
/// <seealso cref="ILayoutManager" />
public sealed class LayoutManager : ILayoutManager
{
    /// <summary>
    /// The application, used to resolve a directory where the window layouts are saved.
    /// </summary>
    private readonly IApplicationContext application;

    /// <summary>
    /// The docking manager, used to manage the current window layout.
    /// </summary>
    private readonly DockingManager dockManager;

    /// <summary>
    /// The file system, used to create the <see cref="LayoutDirectory"/> if one does not already exist.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<LayoutManager> logger;

    /// <summary>
    /// The layout serializer, used to serialize and deserialize the window layouts where required.
    /// </summary>
    private readonly XmlLayoutSerializer serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="LayoutManager"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="dockManager">
    /// The dock manager, used to manage the current window layout.
    /// </param>
    /// <param name="application">
    /// The application context, used to resolve a directory where window layouts are stored.
    /// </param>
    /// <param name="fileSystem">
    /// The file system, used to create the <see cref="LayoutDirectory"/> if one does not already exist.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="dockManager"/>, <paramref name="application"/> or <paramref name="fileSystem"/> parameter cannot be null.
    /// </exception>
    public LayoutManager(
        ILogger<LayoutManager> logger,
        DockingManager dockManager,
        IApplicationContext application,
        IFileSystem fileSystem)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.dockManager = dockManager ?? throw new ArgumentNullException(nameof(dockManager));
        this.application = application ?? throw new ArgumentNullException(nameof(application));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

        this.serializer = new XmlLayoutSerializer(this.dockManager);
    }

    /// <summary>
    /// Gets the layout directory.
    /// </summary>
    /// <value>
    /// The layout directory.
    /// </value>
    /// <remarks>
    /// If the layout directory doesn't exist on the file system, one will be created. The layout directory is stored the applications roaming data.
    /// </remarks>
    private string LayoutDirectory
    {
        get
        {
            string directory = this.fileSystem.Path.Combine(this.application.DataDirectory, "Layouts");

            if (!this.fileSystem.Directory.Exists(directory))
            {
                this.fileSystem.Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// <paramref name="layoutName"/> cannot be null or whitespace.
    /// </exception>
    public bool ContainsLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        return this.LoadLayoutNames().Contains(layoutName);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// <paramref name="layoutName"/> cannot be null or whitespace.
    /// </exception>
    /// <exception cref="WindowLayoutNotFoundException">
    /// The specified <paramref name="layoutName"/> could not be matched to a save window layout.
    /// </exception>
    public void DeleteLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        if (!this.ContainsLayout(layoutName))
        {
            throw new WindowLayoutNotFoundException(layoutName);
        }

        this.logger.LogInformation($"Deleting window layout: '{layoutName}'.");

        this.fileSystem.File.Delete(this.GetLayoutPath(layoutName));

        this.logger.LogInformation($"Layout deleted.");
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// <paramref name="layoutName"/> cannot be null or whitespace.
    /// </exception>
    /// <exception cref="WindowLayoutNotFoundException">
    /// The specified <paramref name="layoutName"/> could not be matched to a save window layout.
    /// </exception>
    public void LoadLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        if (!this.ContainsLayout(layoutName))
        {
            throw new WindowLayoutNotFoundException(layoutName);
        }

        this.logger.LogInformation($"Loading window layout: '{layoutName}'.");

        this.serializer.Deserialize(this.GetLayoutPath(layoutName));

        this.logger.LogInformation("Layout loaded.");
    }

    /// <inheritdoc/>
    public IEnumerable<string> LoadLayoutNames()
    {
        var directoryInfo = this.fileSystem.DirectoryInfo.New(this.LayoutDirectory);
        var files = directoryInfo.GetFiles("*.config", SearchOption.TopDirectoryOnly);

        return files.Select(x =>
        {
            return this.fileSystem.Path.GetFileNameWithoutExtension(x.Name);
        }).ToArray();
    }

    /// <inheritdoc/>
    public void ResetLayout()
    {
        this.logger.LogInformation("Resting window layout to default layout...");
        this.serializer.Deserialize("Resources\\Layouts\\default.config");
        this.logger.LogInformation("Layout reset.");
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// <paramref name="layoutName"/> cannot be null or whitespace.
    /// </exception>
    public void SaveLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        this.logger.LogInformation($"Saving window layout: '{layoutName}'...");

        this.serializer.Serialize(this.GetLayoutPath(layoutName));

        this.logger.LogInformation("Layout saved.");
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// <paramref name="contentID"/> cannot be null or whitespace.
    /// </exception>
    /// <exception cref="ToolPaneNotFoundException">
    /// The <paramref name="contentID"/> parameter could not be matched to a tool pane.
    /// </exception>
    public void ToggleToolWindow(string contentID)
    {
        if (string.IsNullOrWhiteSpace(contentID))
        {
            throw new ArgumentException($"'{nameof(contentID)}' cannot be null or whitespace.", nameof(contentID));
        }

        var tool = this.dockManager.AnchorablesSource.Cast<IToolViewModel>().FirstOrDefault(x =>
        {
            return x.ContentID == contentID;
        }) ?? throw new ToolPaneNotFoundException(contentID);

        this.logger.LogInformation($"Toggling tool view visibility for view with ID: '{contentID}'");

        tool.IsVisible = !tool.IsVisible;
    }

    /// <summary>
    /// Gets the layout file path of the specified <paramref name="layoutName"/>.
    /// </summary>
    /// <param name="layoutName">
    /// The name of the layout.
    /// </param>
    /// <returns>
    /// The file path of window layout that matches the specified <paramref name="layoutName"/>.
    /// </returns>
    private string GetLayoutPath(string layoutName)
    {
        return this.fileSystem.Path.Combine(this.LayoutDirectory, $"{layoutName}.config");
    }
}
