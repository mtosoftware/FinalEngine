// <copyright file="EngineInitializer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using System.IO.Abstractions;
using FinalEngine.Audio.OpenAL.Loaders;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Loaders.Models;
using FinalEngine.Rendering.Loaders.Shaders;
using FinalEngine.Rendering.Loaders.Textures;
using FinalEngine.Resources;

public sealed class EngineInitializer : IEngineInitializer
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    private readonly IResourceManager resourceManager;

    public EngineInitializer(IResourceManager resourceManager, IFileSystem fileSystem, IRenderDevice renderDevice)
    {
        this.resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public void LinkShaderHeaders()
    {
        this.renderDevice.Pipeline.AddShaderHeader("lighting", this.fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\lighting.glsl"));
        this.renderDevice.Pipeline.AddShaderHeader("material", this.fileSystem.File.ReadAllText("Resources\\Shaders\\Includes\\material.glsl"));
    }

    public void RegisterLoaders()
    {
        this.resourceManager.RegisterLoader(new SoundResourceLoader(this.fileSystem));
        this.resourceManager.RegisterLoader(new ShaderResourceLoader(this.fileSystem, this.renderDevice));
        this.resourceManager.RegisterLoader(new ShaderProgramResourceLoader(this.resourceManager, this.renderDevice, this.fileSystem));
        this.resourceManager.RegisterLoader(new Texture2DResourceLoader(this.fileSystem, this.renderDevice));
        this.resourceManager.RegisterLoader(new ModelResourceLoader(this.fileSystem, this.renderDevice));
    }
}
