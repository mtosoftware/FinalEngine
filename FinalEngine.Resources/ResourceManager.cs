// <copyright file="ResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;
using System.Collections.Generic;
using System.Linq;
using FinalEngine.Resources.Exceptions;

public class ResourceManager : IResourceManager
{
    private static IResourceManager? instance;

    private readonly Dictionary<string, ResourceData> pathToResourceDataMap;

    private readonly Dictionary<Type, IResourceLoaderInternal> typeToLoaderMap;

    public ResourceManager()
    {
        this.typeToLoaderMap = [];
        this.pathToResourceDataMap = [];
    }

    ~ResourceManager()
    {
        this.Dispose(false);
    }

    public static IResourceManager Instance
    {
        get
        {
            return instance ??= new ResourceManager();
        }
    }

    protected bool IsDisposed { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public T LoadResource<T>(string filePath)
        where T : IResource
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
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

    public void RegisterLoader<T>(ResourceLoaderBase<T> loader)
        where T : IResource
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        ArgumentNullException.ThrowIfNull(loader, nameof(loader));

        if (this.typeToLoaderMap.ContainsKey(typeof(T)))
        {
            throw new ArgumentException($"The specified {nameof(T)} parameter has already been registered to a resource loader.", nameof(T));
        }

        this.typeToLoaderMap.Add(typeof(T), loader);
    }

    public void UnloadResource(IResource? resource)
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
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

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
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

        this.IsDisposed = true;
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
