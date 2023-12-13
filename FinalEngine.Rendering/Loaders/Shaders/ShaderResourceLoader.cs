// <copyright file="ShaderResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders.Shaders;

using System;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

public sealed class ShaderResourceLoader : ResourceLoaderBase<IShader>
{
    private readonly IGPUResourceFactory factory;

    private readonly IFileSystem fileSystem;

    public ShaderResourceLoader(IFileSystem fileSystem, IGPUResourceFactory factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public override IShader LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} couldn't be located.", filePath);
        }

        var target = this.GetPipelineTarget(this.fileSystem.Path.GetExtension(filePath));

        using (var stream = this.fileSystem.File.OpenRead(filePath))
        {
            using (var reader = new StreamReader(stream))
            {
                return this.factory.CreateShader(target, reader.ReadToEnd());
            }
        }
    }

    private PipelineTarget GetPipelineTarget(string? extension)
    {
        return extension switch
        {
            ".vs" or ".vert" => PipelineTarget.Vertex,
            ".fs" or ".frag" => PipelineTarget.Fragment,
            _ => throw new NotSupportedException($"The specified {nameof(extension)} is not supported by the {nameof(ShaderResourceLoader)}."),
        };
    }
}
