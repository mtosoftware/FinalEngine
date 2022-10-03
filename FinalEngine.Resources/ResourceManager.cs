// <copyright file="ResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using FinalEngine.Resources.Exceptions;

    /// <summary>
    ///   Provides a standard resource manager with reference counting.
    /// </summary>
    /// <seealso cref="IResourceManager"/>
    public class ResourceManager : IResourceManager
    {
        /// <summary>
        ///   The path to resource data map.
        /// </summary>
        private readonly IDictionary<string, ResourceData> pathToResourceDataMap;

        /// <summary>
        ///   The generic type to resource loader map.
        /// </summary>
        private readonly IDictionary<Type, IResourceLoaderInternal> typeToLoaderMap;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ResourceManager"/> class.
        /// </summary>
        public ResourceManager()
        {
            this.typeToLoaderMap = new Dictionary<Type, IResourceLoaderInternal>();
            this.pathToResourceDataMap = new Dictionary<string, ResourceData>();
        }

        /// <summary>
        ///   Finalizes an instance of the <see cref="ResourceManager"/> class.
        /// </summary>
        [ExcludeFromCodeCoverage(Justification = "Exception Class")]
        ~ResourceManager()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="filePath"/> parameter cannot be null, empty or consist of only whitespace characters.
        /// </exception>
        /// <exception cref="Exception">
        ///   The specified <typeparamref name="T"/> parameter does not have an associated registered loader, call <see cref="RegisterLoader{T}(ResourceLoaderBase{T})"/> to register the appropriate resource loader.
        /// </exception>
        public T LoadResource<T>(string filePath)
            where T : IResource
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), $"The specified {nameof(filePath)} parameter cannot be null.");
            }

            if (!this.typeToLoaderMap.TryGetValue(typeof(T), out IResourceLoaderInternal? loader))
            {
                throw new ResourceLoaderNotRegisteredException($"The specified {nameof(T)} parameter does not have an associated registered loader.");
            }

            if (!this.pathToResourceDataMap.TryGetValue(filePath, out ResourceData? resourceData))
            {
                resourceData = new ResourceData(filePath, loader.LoadResource(filePath));
                this.pathToResourceDataMap.Add(filePath, resourceData);
            }

            resourceData.IncrementReferenceCount();

            return (T)resourceData.Reference;
        }

        /// <summary>
        ///   Registers the specified <paramref name="loader"/> to this <see cref="IResourceManager"/>.
        /// </summary>
        /// <typeparam name="T">
        ///   The type of resource the loader can load.
        /// </typeparam>
        /// <param name="loader">
        ///   The loader to register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="loader"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <typeparamref name="T"/> parameter has already been registered to a resource loader.
        /// </exception>
        public void RegisterLoader<T>(ResourceLoaderBase<T> loader)
            where T : IResource
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader), $"The specified {nameof(loader)} parameter cannot be null.");
            }

            if (this.typeToLoaderMap.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"The specified {nameof(T)} parameter has already been registered to a resource loader.", nameof(T));
            }

            this.typeToLoaderMap.Add(typeof(T), loader);
        }

        /// <summary>
        ///   Unloads the specified <paramref name="resource"/> from this <see cref="IResourceManager"/>.
        /// </summary>
        /// <param name="resource">
        ///   The resource to unload.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="resource"/> parameter cannot be null.
        /// </exception>
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

                resourceData.DecrementReferenceCount();

                if (resourceData.ReferenceCount == 0)
                {
                    resourceData.Reference.Dispose();
                    this.pathToResourceDataMap.Remove(resourceData.FilePath);
                }
            }
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
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
                }

                this.typeToLoaderMap.Clear();
            }

            this.IsDisposed = true;
        }

        /// <summary>
        ///   Represents a reference to a resource and it's associated data.
        /// </summary>
        private class ResourceData
        {
            /// <summary>
            ///   Initializes a new instance of the <see cref="ResourceData"/> class.
            /// </summary>
            /// <param name="filePath">
            ///   The file path of the resource.
            /// </param>
            /// <param name="reference">
            ///   The reference of the resource.
            /// </param>
            /// <exception cref="ArgumentNullException">
            ///   The specified <paramref name="filePath"/> or <paramref name="reference"/> parameter cannot be null.
            /// </exception>
            public ResourceData(string filePath, IResource reference)
            {
                this.FilePath = filePath;
                this.Reference = reference;
            }

            /// <summary>
            ///   Gets the file path of the resource.
            /// </summary>
            /// <value>
            ///   The file path of the resource.
            /// </value>
            public string FilePath { get; }

            /// <summary>
            ///   Gets the reference of the resource.
            /// </summary>
            /// <value>
            ///   The reference of the resource.
            /// </value>
            public IResource Reference { get; }

            /// <summary>
            ///   Gets the reference count.
            /// </summary>
            /// <value>
            ///   The reference count.
            /// </value>
            public int ReferenceCount { get; private set; }

            /// <summary>
            ///   Decrements the reference count.
            /// </summary>
            public void DecrementReferenceCount()
            {
                this.ReferenceCount--;
            }

            /// <summary>
            ///   Increments the reference count.
            /// </summary>
            public void IncrementReferenceCount()
            {
                this.ReferenceCount++;
            }
        }
    }
}