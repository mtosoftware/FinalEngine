// <copyright file="IResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    using System;

    /// <summary>
    ///   Defines an interface that loads, unloads and manages resources.
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    public interface IResourceManager : IDisposable
    {
        /// <summary>
        ///   Loads a resource from the specified <paramref name="filePath"/>, or returns the cached resource if it has already been loaded.
        /// </summary>
        /// <typeparam name="T">
        ///   The type of resource to load.
        /// </typeparam>
        /// <param name="filePath">
        ///   The file path of the resource to load.
        /// </param>
        /// <returns>
        ///   The loaded or cached resource.
        /// </returns>
        T LoadResource<T>(string filePath)
            where T : IResource;

        /// <summary>
        ///   Registers the specified <paramref name="loader"/> to this <see cref="IResourceManager"/>.
        /// </summary>
        /// <typeparam name="T">
        ///   The type of resource the loader can load.
        /// </typeparam>
        /// <param name="loader">
        ///   The loader to register.
        /// </param>
        void RegisterLoader<T>(ResourceLoaderBase<T> loader)
                    where T : IResource;

        /// <summary>
        ///   Unloads the specified <paramref name="resource"/> from this <see cref="IResourceManager"/>.
        /// </summary>
        /// <param name="resource">
        ///   The resource to unload.
        /// </param>
        void UnloadResource(IResource resource);
    }
}