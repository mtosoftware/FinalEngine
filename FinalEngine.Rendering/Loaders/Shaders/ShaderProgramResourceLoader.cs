// <copyright file="ShaderProgramResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders.Shaders;

using System;
using System.IO;
using System.IO.Abstractions;
using System.Text.Json;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

internal sealed class ShaderProgramResourceLoader : ResourceLoaderBase<IShaderProgram>
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    private readonly IResourceManager resourceManager;

    public ShaderProgramResourceLoader(IResourceManager resourceManager, IRenderDevice renderDevice, IFileSystem fileSystem)
    {
        this.resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public override IShaderProgram LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} couldn't be located.", filePath);
        }

        string content = this.fileSystem.File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<ProgramData>(content);

        if (data == null)
        {
            throw new ArgumentException($"The specified {nameof(filePath)} couldn't be converted into an {nameof(IShaderProgram)}, please check your formatting.", nameof(filePath));
        }

        string directory = this.fileSystem.FileInfo.New(filePath).Directory?.FullName ?? string.Empty;

        return this.renderDevice.Factory.CreateShaderProgram(new[]
        {
            this.resourceManager.LoadResource<IShader>($"{directory}\\{data.Vertex}"),
            this.resourceManager.LoadResource<IShader>($"{directory}\\{data.Fragment}"),
        });
    }

    private class ProgramData
    {
        public string Fragment { get; set; }

        public string Vertex { get; set; }
    }
}
