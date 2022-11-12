// <copyright file="ShaderProgramResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders
{
    using System;
    using System.IO;
    using System.Text.Json;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    public class ShaderProgramResourceLoader : ResourceLoaderBase<IShaderProgram>
    {
        private readonly IGPUResourceFactory factory;

        private readonly IFileSystem fileSystem;

        public ShaderProgramResourceLoader(IGPUResourceFactory factory, IFileSystem fileSystem)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public override IShaderProgram LoadResource(string filePath)
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
                    string content = reader.ReadToEnd();
                    var data = JsonSerializer.Deserialize<ResourceData>(content);

                    string directory = $"{Path.GetDirectoryName(filePath)}\\";

                    var vertexShader = ResourceManager.Instance.LoadResource<IShader>(directory + data.VertexFilePath);
                    var fragmentShader = ResourceManager.Instance.LoadResource<IShader>(directory + data.FragmentFilePath);

                    var shaderProgram = this.factory.CreateShaderProgram(new[]
                    {
                        vertexShader,
                        fragmentShader,
                    });

                    ResourceManager.Instance.UnloadResource(vertexShader);
                    ResourceManager.Instance.UnloadResource(fragmentShader);

                    return shaderProgram;
                }
            }
        }

        private struct ResourceData
        {
            public string FragmentFilePath { get; set; }

            public string VertexFilePath { get; set; }
        }
    }
}
