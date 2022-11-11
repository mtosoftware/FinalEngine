// <copyright file="ShaderResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders
{
    using System;
    using System.IO;
    using System.Text;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    public class ShaderResourceLoader : ResourceLoaderBase<IShader>
    {
        private readonly IGPUResourceFactory factory;

        private readonly IFileSystem fileSystem;

        public ShaderResourceLoader(IGPUResourceFactory factory, IFileSystem fileSystem)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
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

            string source = this.LoadShaderSource(filePath);
            var target = GetPipelineTarget(Path.GetExtension(filePath));

            return this.factory.CreateShader(target, source);
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

        private string LoadShaderSource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
            }

            if (!this.fileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
            }

            var sb = new StringBuilder();

            using (var stream = this.fileSystem.OpenFile(filePath, FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string? line = reader.ReadLine();

                        if (line!.Trim().StartsWith("#include", StringComparison.CurrentCulture))
                        {
                            string includePath = line
                                .Replace("#include", string.Empty, StringComparison.CurrentCulture)
                                .Replace("<", string.Empty, StringComparison.CurrentCulture)
                                .Replace(">", string.Empty, StringComparison.CurrentCulture)
                                .Replace("\"", string.Empty, StringComparison.CurrentCulture)
                                .Trim();

                            sb.AppendLine(this.fileSystem.GetVirtualTextFile(includePath));
                        }
                        else
                        {
                            sb.AppendLine(line);
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
