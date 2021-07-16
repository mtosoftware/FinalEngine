// <copyright file="ResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ResourceManager : IResourceManager
    {
        private IDictionary<string, ResourceData> pathToResourceDataMap;

        private IDictionary<Type, IResourceLoaderInternal> typeToLoaderMap;

        public ResourceManager()
        {
            this.typeToLoaderMap = new Dictionary<Type, IResourceLoaderInternal>();
            this.pathToResourceDataMap = new Dictionary<string, ResourceData>();
        }

        ~ResourceManager()
        {
            this.Dispose(false);
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
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), $"The specified {nameof(filePath)} parameter cannot be null.");
            }

            if (!this.typeToLoaderMap.TryGetValue(typeof(T), out IResourceLoaderInternal? loader))
            {
                throw new Exception($"The specified {nameof(T)} parameter does not have an associated registered loader.");
            }

            if (!this.pathToResourceDataMap.TryGetValue(filePath, out ResourceData? resourceData))
            {
#if DEBUG
                Console.WriteLine($"Loading Resource: {filePath}");
#endif
                resourceData = new ResourceData()
                {
                    FilePath = filePath,
                    Reference = loader.LoadResource(filePath),
                };

                this.pathToResourceDataMap.Add(filePath, resourceData);
            }

            resourceData.IncrementReferenceCount();

#if DEBUG
            Console.WriteLine($"Incrementing Reference Count: {filePath} : {resourceData.ReferenceCount}");
#endif

            return (T)resourceData.Reference;
        }

        public void RegisterLoader<T>(ResourceLoaderBase<T> loader)
            where T : IResource
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader), $"The specified {nameof(loader)} parameter cannot be null.");
            }

            if (this.typeToLoaderMap.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"The specifeid {nameof(T)} parameter has already been registered to a resource loader.", nameof(T));
            }

            this.typeToLoaderMap.Add(typeof(T), loader);
        }

        public void UnloadResource(IResource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource), $"The specified {nameof(resource)} parameter cannot be null.");
            }

            for (int i = this.pathToResourceDataMap.Count - 1; i >= 0; i--)
            {
                KeyValuePair<string, ResourceData> kvp = this.pathToResourceDataMap.ElementAt(i);
                ResourceData resourceData = kvp.Value;

#if DEBUG
                Console.WriteLine($"Decrementing Reference Count: {resourceData.FilePath} : {resourceData.ReferenceCount}");
#endif

                resourceData.DecrementReferenceCount();

                if (resourceData.ReferenceCount == 0)
                {
                    resourceData.Reference.Dispose();
                    this.pathToResourceDataMap.Remove(resourceData.FilePath);
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
                        KeyValuePair<string, ResourceData> kvp = this.pathToResourceDataMap.ElementAt(i);
                        ResourceData resourceData = kvp.Value;

                        resourceData.Reference.Dispose();
                        this.pathToResourceDataMap.Remove(resourceData.FilePath);
                    }

                    this.pathToResourceDataMap = null;
                }

                if (this.typeToLoaderMap != null)
                {
                    this.typeToLoaderMap.Clear();
                    this.typeToLoaderMap = null;
                }
            }

            this.IsDisposed = true;
        }

        private class ResourceData
        {
            public string FilePath { get; init; }

            public IResource Reference { get; init; }

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
}