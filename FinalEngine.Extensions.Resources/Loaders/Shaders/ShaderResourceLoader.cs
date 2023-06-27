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

/// <summary>
/// Provides a resource loader that can load an <see cref="IShader"/> resource.
/// </summary>
public sealed class ShaderResourceLoader : ResourceLoaderBase<IShader>
{
    /// <summary>
    /// The GPU resource factory, used to load shaders into memory.
    /// </summary>
    private readonly IGPUResourceFactory factory;

    /// <summary>
    /// The file system, used to load shader source code into memory.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderResourceLoader"/> class.
    /// </summary>
    /// <param name="factory">
    /// The GPU resource factory, used to load shaders into memory.
    /// </param>
    /// <param name="fileSystem">
    /// The file system, used to load shader source code into memory.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="factory"/> or <paramref name="fileSystem"/> parameter cannot be null.
    /// </exception>
    public ShaderResourceLoader(IGPUResourceFactory factory, IFileSystem fileSystem)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    /// <summary>
    /// Loads an <see cref="IShader"/> resource from the specified <paramref name="filePath" />.
    /// </summary>
    /// <param name="filePath">
    /// The file path to load the resource.
    /// </param>
    /// <returns>
    /// The newly loaded resource.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="filePath"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// The specified <paramref name="filePath"/> couldn't be located.
    /// </exception>
    public override IShader LoadResource(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (!this.fileSystem.FileExists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} couldn't be located.", filePath);
        }

        var target = this.GetPipelineTarget(this.fileSystem.GetExtension(filePath));

        using (var stream = this.fileSystem.OpenFile(filePath, FileAccessMode.Read))
        {
            using (var reader = new StreamReader(stream))
            {
                return this.factory.CreateShader(target, reader.ReadToEnd());
            }
        }
    }

    /// <summary>
    /// Gets the pipeline target based on the specified file <paramref name="extension"/>.
    /// </summary>
    /// <param name="extension">
    /// The file extension (including the period).
    /// </param>
    /// <returns>
    /// The pipeline target that relates to the specified <paramref name="extension"/>.
    /// </returns>
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
