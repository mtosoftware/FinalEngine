// <copyright file="ShaderResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Loaders.Shaders;

using System;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

public sealed class ShaderResourceLoader : ResourceLoaderBase<IShader>
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    public ShaderResourceLoader(IFileSystem fileSystem, IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
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
                return this.renderDevice.Factory.CreateShader(target, this.LoadShaderSource(reader.ReadToEnd()));
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

    private string LoadShaderSource(string sourceCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceCode, nameof(sourceCode));

        var sb = new StringBuilder();

        using (var reader = new StringReader(sourceCode))
        {
            string? line = null;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#include", StringComparison.InvariantCulture))
                {
                    string includeName = line.Replace("#include", string.Empty, StringComparison.InvariantCulture)
                                             .Replace("<", string.Empty, StringComparison.InvariantCulture)
                                             .Replace(">", string.Empty, StringComparison.InvariantCulture)
                                             .Replace("\"", string.Empty, StringComparison.InvariantCulture)
                                             .Trim();

                    sb.AppendLine(this.LoadShaderSource(this.renderDevice.Pipeline.GetShaderHeader(includeName)));
                }
                else
                {
                    sb.AppendLine(line);
                }
            }
        }

        return sb.ToString();
    }
}
