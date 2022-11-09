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

            if (!this.fileSystem.DirectoryExists(filePath))
            {
                throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
            }

            string[] files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);

            var shaders = new List<IShader>();

            foreach (string file in files)
            {
                shaders.Add(ResourceManager.Instance.LoadResource<IShader>(file));
            }

            return this.factory.CreateShaderProgram(shaders);
        }
    }
}
