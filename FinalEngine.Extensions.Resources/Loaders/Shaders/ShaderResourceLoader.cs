// <copyright file="ShaderResourceLoader.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders.Shaders;

using System;
using System.IO;
using FinalEngine.IO;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

public sealed class ShaderResourceLoader : ResourceLoaderBase<IShader>
{
    private readonly IGPUResourceFactory factory;

    private readonly IFileSystem fileSystem;

    public ShaderResourceLoader(IFileSystem fileSystem, IGPUResourceFactory factory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public override IShader LoadResource(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
        }

        if (!this.fileSystem.FileExists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        using (var stream = this.fileSystem.OpenFile(filePath, FileAccessMode.Read))
        {
            using (var reader = new StreamReader(stream))
            {
                string source = reader.ReadToEnd();
                var target = GetPipelineTarget(Path.GetExtension(filePath));

                return this.factory.CreateShader(target, source);
            }
        }
    }

    private static PipelineTarget GetPipelineTarget(string extension)
    {
        return extension switch
        {
            ".vert" or ".vs" => PipelineTarget.Vertex,
            ".frag" or ".fs" => PipelineTarget.Fragment,
            _ => throw new NotSupportedException($"The specified {nameof(extension)} parameter is not supported: '{extension}'."),
        };
    }
}
