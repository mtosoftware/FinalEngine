// <copyright file="IResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;
using FinalEngine.Resources.Exceptions;

/// <summary>
/// Defines an interface that represents a resource manager.
/// </summary>
///
/// <remarks>
/// In almost all scenarios you should never have to implement this interface; if you require management of resources you should use the standard <see cref="ResourceManager"/> implementation.
/// </remarks>
///
/// <seealso cref="System.IDisposable" />
public interface IResourceManager : IDisposable
{
    /// <summary>
    /// Loads a resource at the specified <paramref name="filePath"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of resource to load.
    /// </typeparam>
    ///
    /// <param name="filePath">
    /// The file path of the resource to load.
    /// </param>
    ///
    /// <remarks>
    /// The typical implementation of <see cref="IResourceManager"/> should attempt to cache resources; this way if <see cref="LoadResource{T}(string)"/> is called for the same file a reference the already loaded resource can be fetched.
    /// </remarks>
    ///
    /// <returns>
    /// The loaded resource, of type <typeparamref name="T"/>.
    /// </returns>
    T LoadResource<T>(string filePath)
        where T : IResource;

    /// <summary>
    /// Registers the specified <paramref name="loader"/> to this <see cref="IResourceManager"/>.
    /// </summary>
    ///
    /// <typeparam name="T">
    /// The type of resource that can be loaded.
    /// </typeparam>
    ///
    /// <param name="loader">
    /// The resource loader to be used when attempting to resolve a resource of type <typeparamref name="T"/>.
    /// </param>
    ///
    /// <remarks>
    /// The typical implementation of an <see cref="IResourceManager"/> should likely throw a <see cref="ResourceLoaderNotRegisteredException"/> if a loader of the specified <typeparamref name="T"/> type has already been registered.
    /// </remarks>
    void RegisterLoader<T>(ResourceLoaderBase<T> loader)
                where T : IResource;

    void RegisterLoader(Type type, IResourceLoader loader);

    /// <summary>
    /// Unloads the specified  <paramref name="resource"/> from this <see cref="IResourceManager"/> (calling it's dispose method if <see cref="IDisposable"/> is implemented and there are no references).
    /// </summary>
    ///
    /// <param name="resource">
    /// The resource to unload.
    /// </param>
    void UnloadResource(IResource resource);
}
