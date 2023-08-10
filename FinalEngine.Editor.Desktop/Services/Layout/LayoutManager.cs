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
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Services.Layout;

public sealed class LayoutManager : ILayoutManager
{
    private readonly IApplicationContext application;

    private readonly DockingManager dockManager;

    private readonly IFileSystem fileSystem;

    private readonly XmlLayoutSerializer serializer;

    public LayoutManager(DockingManager dockManager, IApplicationContext application, IFileSystem fileSystem)
    {
        this.dockManager = dockManager ?? throw new ArgumentNullException(nameof(dockManager));
        this.application = application ?? throw new ArgumentNullException(nameof(application));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

        this.serializer = new XmlLayoutSerializer(this.dockManager);
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
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        return this.LoadLayoutNames().Contains(layoutName);
    }

    public void DeleteLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        if (!this.ContainsLayout(layoutName))
        {
            throw new Exception($"Failed to locate window layout for layout: '{layoutName}'");
        }

        this.fileSystem.File.Delete(this.GetLayoutPath(layoutName));
    }

    public void LoadLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        if (!this.ContainsLayout(layoutName))
        {
            throw new Exception($"Failed to locate window layout for layout: '{layoutName}'");
        }

        this.serializer.Deserialize(this.GetLayoutPath(layoutName));
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
        this.serializer.Deserialize("Resources\\Layouts\\default.config");
    }

    public void SaveLayout(string layoutName)
    {
        if (string.IsNullOrWhiteSpace(layoutName))
        {
            throw new ArgumentException($"'{nameof(layoutName)}' cannot be null or whitespace.", nameof(layoutName));
        }

        this.serializer.Serialize(this.GetLayoutPath(layoutName));
    }

    public void ToggleToolWindow(string contentID)
    {
        if (string.IsNullOrWhiteSpace(contentID))
        {
            throw new ArgumentException($"'{nameof(contentID)}' cannot be null or whitespace.", nameof(contentID));
        }

        var tool = this.dockManager.AnchorablesSource.Cast<IToolViewModel>().FirstOrDefault(x =>
        {
            return x.ContentID == contentID;
        }) ?? throw new ArgumentException($"The specified {nameof(contentID)} couldn't be matched to a tool window.", nameof(contentID));

        tool.IsVisible = !tool.IsVisible;
    }

    private string GetLayoutPath(string layoutName)
    {
        return this.fileSystem.Path.Combine(this.LayoutDirectory, $"{layoutName}.config");
    }
}
