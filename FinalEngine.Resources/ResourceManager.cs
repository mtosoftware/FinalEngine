// <copyright file="ResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;
using System.Collections.Generic;
using System.Linq;
using FinalEngine.Resources.Exceptions;

/// <summary>
/// Provides a standard implementation of an <see cref="IResourceManager"/>.
/// </summary>
///
/// <seealso cref="IResourceManager" />
public sealed class ResourceManager : IResourceManager
{
    private static IResourceManager? instance;

    private readonly Dictionary<string, ResourceData> pathToResourceDataMap;

    private readonly Dictionary<Type, IResourceLoader> typeToLoaderMap;

    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceManager"/> class.
    /// </summary>
    public ResourceManager()
    {
        this.typeToLoaderMap = [];
        this.pathToResourceDataMap = [];
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="ResourceManager"/> class.
    /// </summary>
    ~ResourceManager()
    {
        this.Dispose(false);
    }

    /// <summary>
    /// Gets the instance.
    /// </summary>
    ///
    /// <value>
    /// The instance.
    /// </value>
    public static IResourceManager Instance
    {
        get
        {
            return instance ??= new ResourceManager();
        }
    }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

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
    ///
    /// <exception cref="ResourceLoaderNotRegisteredException">
    /// The specified <typeparamref name="T"/> parameter does not have an associated registered loader. You should call <see cref="RegisterLoader{T}(ResourceLoaderBase{T})"/> on the resource type you wish to load.
    /// </exception>
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="ResourceManager"/> has been disposed.
    /// </exception>
    ///
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="filePath"/> parameter cannot be null, empty or consist of only whitespace characters.
    /// </exception>
    public T LoadResource<T>(string filePath)
        where T : IResource
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));

        if (!this.typeToLoaderMap.TryGetValue(typeof(T), out var loader))
        {
            throw new ResourceLoaderNotRegisteredException($"The specified {nameof(T)} parameter does not have an associated registered loader.");
        }

        if (!this.pathToResourceDataMap.TryGetValue(filePath, out var resourceData))
        {
            resourceData = new ResourceData(filePath, loader.LoadResource(filePath));
            this.pathToResourceDataMap.Add(filePath, resourceData);
        }

        resourceData.IncrementReferenceCount();

        return (T)resourceData.Reference;
    }

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
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="ResourceManager"/> has been disposed.
    /// </exception>
    ///
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="loader"/> parameter cannot be null.
    /// </exception>
    public void RegisterLoader<T>(ResourceLoaderBase<T> loader)
        where T : IResource
    {
        this.RegisterLoader(typeof(T), loader);
    }

    public void RegisterLoader(Type type, IResourceLoader loader)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(type, nameof(type));
        ArgumentNullException.ThrowIfNull(loader, nameof(loader));

        if (this.typeToLoaderMap.ContainsKey(type))
        {
            throw new ArgumentException($"The specified {nameof(type)} parameter has already been registered to a resource loader.", nameof(type));
        }

        this.typeToLoaderMap.Add(type, loader);
    }

    /// <summary>
    /// Unloads the specified  <paramref name="resource"/> from this <see cref="IResourceManager"/> (calling it's dispose method if <see cref="IDisposable"/> is implemented and there are no references).
    /// </summary>
    ///
    /// <param name="resource">
    /// The resource to unload.
    /// </param>
    ///
    /// <exception cref="ObjectDisposedException">
    /// The <see cref="ResourceManager"/> has been disposed.
    /// </exception>
    ///
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="resource"/> parameter cannot be null.
    /// </exception>
    public void UnloadResource(IResource resource)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(resource, nameof(resource));

        for (int i = this.pathToResourceDataMap.Count - 1; i >= 0; i--)
        {
            var kvp = this.pathToResourceDataMap.ElementAt(i);
            var resourceData = kvp.Value;

            if (ReferenceEquals(resourceData.Reference, resource))
            {
                resourceData.DecrementReferenceCount();

                if (resourceData.ReferenceCount == 0)
                {
                    if (resourceData.Reference is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    this.pathToResourceDataMap.Remove(resourceData.FilePath);
                }
            }
        }
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.pathToResourceDataMap != null)
            {
                for (int i = this.pathToResourceDataMap.Count - 1; i >= 0; i--)
                {
                    var kvp = this.pathToResourceDataMap.ElementAt(i);
                    var resourceData = kvp.Value;

                    if (resourceData.Reference is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    this.pathToResourceDataMap.Remove(resourceData.FilePath);
                }
            }

            this.typeToLoaderMap.Clear();
        }

        this.isDisposed = true;
    }

    private sealed class ResourceData
    {
        public ResourceData(string filePath, IResource reference)
        {
            this.FilePath = filePath;
            this.Reference = reference;
        }

        public string FilePath { get; }

        public IResource Reference { get; }

        public int ReferenceCount { get; private set; }

        public void DecrementReferenceCount()
        {
            this.ReferenceCount--;
        }

        public void IncrementReferenceCount()
        {
            this.ReferenceCount++;
        }
    }
}
