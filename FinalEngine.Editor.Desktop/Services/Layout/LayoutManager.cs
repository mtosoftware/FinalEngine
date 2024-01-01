// <copyright file="LayoutManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Services.Layout;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Windows;
using AvalonDock;
using AvalonDock.Layout.Serialization;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Desktop.Exceptions.Layout;
using FinalEngine.Editor.Desktop.Views.Docking;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Services.Layout;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Logging;

public sealed class LayoutManager : ILayoutManager
{
    private static readonly DockingManager Instance = Application.Current.MainWindow.FindChild<DockView>().DockManager;

    private readonly IApplicationContext application;

    private readonly IFileSystem fileSystem;

    private readonly ILogger<LayoutManager> logger;

    public LayoutManager(
        ILogger<LayoutManager> logger,
        IApplicationContext application,
        IFileSystem fileSystem)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.application = application ?? throw new ArgumentNullException(nameof(application));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

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

    public bool ContainsLayout(string layoutName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(layoutName, nameof(layoutName));
        return this.LoadLayoutNames().Contains(layoutName);
    }

    public void DeleteLayout(string layoutName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(layoutName, nameof(layoutName));

        if (!this.ContainsLayout(layoutName))
        {
            throw new WindowLayoutNotFoundException(layoutName);
        }

        this.logger.LogInformation($"Deleting window layout: '{layoutName}'.");

        this.fileSystem.File.Delete(this.GetLayoutPath(layoutName));

        this.logger.LogInformation($"Layout deleted.");
    }

    public void LoadLayout(string layoutName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(layoutName, nameof(layoutName));

        if (!this.ContainsLayout(layoutName))
        {
            throw new WindowLayoutNotFoundException(layoutName);
        }

        this.logger.LogInformation($"Loading window layout: '{layoutName}'.");

        var serializer = new XmlLayoutSerializer(Instance);
        serializer.Deserialize(this.GetLayoutPath(layoutName));

        this.logger.LogInformation("Layout loaded.");
    }

    public IEnumerable<string> LoadLayoutNames()
    {
        var directoryInfo = this.fileSystem.DirectoryInfo.New(this.LayoutDirectory);
        var files = directoryInfo.GetFiles("*.config", SearchOption.TopDirectoryOnly);

        return files.Select(x =>
        {
            return this.fileSystem.Path.GetFileNameWithoutExtension(x.Name);
        }).ToArray();
    }

    public void ResetLayout()
    {
        const string defaultLayoutPath = "Resources\\Layouts\\default.config";

        this.logger.LogInformation("Resting window layout to default layout...");

        var serializer = new XmlLayoutSerializer(Instance);
        serializer.Deserialize(defaultLayoutPath);

        this.logger.LogInformation("Layout reset.");
    }

    public void SaveLayout(string layoutName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(layoutName, nameof(layoutName));

        this.logger.LogInformation($"Saving window layout: '{layoutName}'...");

        var serializer = new XmlLayoutSerializer(Instance);
        serializer.Serialize(this.GetLayoutPath(layoutName));

        this.logger.LogInformation("Layout saved.");
    }

    public void ToggleToolWindow(string contentID)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(contentID, nameof(contentID));

        var tool = Instance.AnchorablesSource.Cast<IToolViewModel>().FirstOrDefault(x =>
        {
            return x.ContentID == contentID;
        }) ?? throw new ToolPaneNotFoundException(contentID);

        this.logger.LogInformation($"Toggling tool view visibility for view with ID: '{contentID}'");

        tool.IsVisible = !tool.IsVisible;
    }

    private string GetLayoutPath(string layoutName)
    {
        return this.fileSystem.Path.Combine(this.LayoutDirectory, $"{layoutName}.config");
    }
}
