// <copyright file="ResourceLoaderRegistrar.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Services.Resources
{
    using System;
    using FinalEngine.Extensions.Resources.Loaders;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Resources;

    public class ResourceLoaderRegistrar : IResourceLoaderRegistrar
    {
        private readonly IFileSystem fileSystem;

        private readonly IRenderDevice renderDevice;

        private readonly IResourceManager resourceManager;

        public ResourceLoaderRegistrar(IResourceManager resourceManager, IFileSystem fileSystem, IRenderDevice renderDevice)
        {
            this.resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        }

        public void RegisterAll()
        {
            this.resourceManager.RegisterLoader(
                new Texture2DResourceLoader(
                    this.fileSystem,
                    this.renderDevice.Factory));

            this.resourceManager.RegisterLoader(
                new ShaderResourceLoader(
                    this.renderDevice.Factory,
                    this.fileSystem));
        }
    }
}
