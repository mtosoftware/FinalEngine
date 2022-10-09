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

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IResourceLoaderRegistrar"/>.
    /// </summary>
    /// <seealso cref="IResourceLoaderRegistrar"/>
    public class ResourceLoaderRegistrar : IResourceLoaderRegistrar
    {
        /// <summary>
        ///   The file system, used when creating resource loaders.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        ///   The render device, used when creating resource loaders.
        /// </summary>
        private readonly IRenderDevice renderDevice;

        /// <summary>
        ///   The resource manager, used when registering resource loaders.
        /// </summary>
        private readonly IResourceManager resourceManager;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ResourceLoaderRegistrar"/> class.
        /// </summary>
        /// <param name="resourceManager">
        ///   The resource manager, used when registering resource loaders.
        /// </param>
        /// <param name="fileSystem">
        ///   The file system, used when creating resource loaders.
        /// </param>
        /// <param name="renderDevice">
        ///   The render device, used when creating resource loaders.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="resourceManager"/>, <paramref name="fileSystem"/> or <paramref name="renderDevice"/> parameter cannot be null.
        /// </exception>
        public ResourceLoaderRegistrar(IResourceManager resourceManager, IFileSystem fileSystem, IRenderDevice renderDevice)
        {
            this.resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        }

        /// <summary>
        ///   Registers all resource loaders to the resource manager.
        /// </summary>
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
