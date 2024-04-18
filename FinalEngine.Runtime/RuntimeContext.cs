// <copyright file="RuntimeContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using FinalEngine.Rendering;
using FinalEngine.Resources;

internal sealed class RuntimeContext : IRuntimeContext
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    private readonly IEnumerable<IResourceLoader> resourceLoaders;

    private readonly IResourceManager resourceManager;

    public RuntimeContext(
        IFileSystem fileSystem,
        IResourceManager resourceManager,
        IEnumerable<IResourceLoader> resourceLoaders,
        IRenderDevice renderDevice)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
        this.resourceLoaders = resourceLoaders ?? throw new ArgumentNullException(nameof(resourceLoaders));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public void Initialize()
    {
        this.RegisterResourceLoaders();
        this.AddShaderHeaderFiles();
    }

    private void AddShaderHeaderFiles()
    {
        this.renderDevice.Pipeline.AddShaderHeader("lighting", this.fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\lighting.glsl"));
        this.renderDevice.Pipeline.AddShaderHeader("material", this.fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\material.glsl"));
    }

    private void RegisterResourceLoaders()
    {
        foreach (var loader in this.resourceLoaders)
        {
            this.resourceManager.RegisterLoader(loader.GetResourceType(), loader);
        }
    }
}
