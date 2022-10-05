// <copyright file="ShaderProgramResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    //// TODO: Refactor and really test this, catch exceptions, etc.
    //// TODO: Loading all shaders in the directory is a little yuck - what about ensuring a shader of that type hasn't been loaded already?

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
                throw new ArgumentNullException(nameof(filePath), $"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.");
            }

            if (!this.fileSystem.DirectoryExists(filePath))
            {
                throw new DirectoryNotFoundException($"The specified {nameof(filePath)} parameter cannot be located at path: '{filePath}'.");
            }

            var shaders = new List<IShader>();
            string[] files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);

            foreach (string? file in files)
            {
                string extension = Path.GetExtension(file);

                PipelineTarget target;

                switch (extension)
                {
                    case ".vert":
                        target = PipelineTarget.Vertex;
                        break;

                    case ".frag":
                        target = PipelineTarget.Fragment;
                        break;

                    default:
                        throw new NotSupportedException($"The file extension: '{extension}' is not a recognized shader file type for file path: '{file}'.");
                }

                using (Stream? stream = this.fileSystem.OpenFile(file, FileAccessMode.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        shaders.Add(this.factory.CreateShader(target, reader.ReadToEnd()));
                    }
                }
            }

            IShaderProgram program = this.factory.CreateShaderProgram(shaders);

            foreach (IShader shader in shaders)
            {
                shader.Dispose();
            }

            return program;
        }
    }
}