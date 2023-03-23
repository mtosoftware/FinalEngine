// <copyright file="ResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FinalEngine.Resources.Exceptions;

/// <summary>
/// Provides a standard resource manager with reference counting.
/// </summary>
/// <remarks>
/// The <see cref="ResourceManager"/> is used to manage resources throughout the life-cycle of your game or application. It utilizes reference counting so it's important to remember to unload resources once you are done with them. It internally manages the life-cycle of the dependency and once all references have been unloaded their dispose method is invoked. It's also super important to provide the correct extension suffix for the resource you're attempting to load as the manager does not attempt to resolve this for you.
/// </remarks>
/// <seealso cref="IResourceManager" />
public class ResourceManager : IResourceManager
{
    /// <summary>
    ///   The instance.
    /// </summary>
    private static IResourceManager? instance;

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
    [ExcludeFromCodeCoverage]
    ~ResourceManager()
    {
        this.Dispose(false);
    }

    /// <summary>
    ///   Gets the instance.
    /// </summary>
    /// <value>
    ///   The instance.
    /// </value>
    public static IResourceManager Instance
    {
        get
        {
            return instance ??= new ResourceManager();
        }
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
    /// <example>
    /// Below you'll see an example of how to load and unload texture resources using the resource manager.
    /// <code title="ResourceManagerExample.cs">
    /// namespace TextureResourceLoadingExample;
    ///
    /// using FinalEngine.Extensions.Resources.Loaders.Textures;
    /// using FinalEngine.Rendering.Textures;
    ///
    /// public class Game : GameBase
    /// {
    ///     protected override void LoadResources()
    ///     {
    ///         // Register the resource loader(s) if you haven't already.
    ///         // Here we are using the standard implementation through
    ///         // an interface that refers to ResourceManager.Instance.
    ///         this.ResourceManager.RegisterLoader(new Texture2DResourceLoader(this.FileSystem, this.RenderDevice.Factory));
    ///
    ///         // Finally, we can load a resource of type ITexture2D.
    ///         // We now don't have to worry about disposing of the resource
    ///         // as it will be handled by the resource manager once all
    ///         // references have been removed.
    ///         var texture = this.ResourceManager.LoadResource&lt;ITexture2D&gt;("Resources\\Textures\\texture.png"); // First reference.
    ///
    ///         // Now let's load the same texture into a different variable.
    ///         // This will mean the reference count will be two. As such,
    ///         // once the resource has been unloaded it will still be loaded
    ///         // in memory.
    ///         var texture_two = this.ResourceManager.LoadResource&lt;ITexture2D&gt;("Resources\\Textures\\texture.png"); // Second reference.
    ///
    ///         // Now, let's unload a reference.
    ///         this.ResourceManager.UnloadResource(texture_two);
    ///
    ///         // Note that here we can still refer to texture and texture_two
    ///         // as they still both refer to the same reference object.
    ///         // so we could pass in texture_two here if we wanted to.
    ///         this.ResourceManager.UnloadResource(texture);
    ///
    ///         base.LoadResources();
    ///     }
    /// }
    /// </code>
    /// </example>
    public T LoadResource<T>(string filePath)
        where T : IResource
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(ResourceManager));
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
        }

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
    /// Registers the specified <paramref name="loader" /> to this <see cref="IResourceManager" />.
    /// </summary>
    /// <typeparam name="T">
    /// The type of resource the loader can load.
    /// </typeparam>
    /// <param name="loader">
    /// The loader to register.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="loader" /> parameter cannot be null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <span class="typeparameter">T</span> parameter has already been registered to a resource loader.
    /// </exception>
    /// <example>
    /// Below you'll find an example of how to register a sound resource loader.
    /// <code title="RegisterLoaderExample.cs">
    /// public class Game : GameBase
    /// {
    ///     protected override void LoadResources()
    ///     {
    ///         // Use the interface provided instead of the implementation
    ///         // Or use ResourceManager.Instance if you like.
    ///         this.ResourceManager.RegisterLoader(new SoundResourceLoader(this.FileSystem));
    ///
    ///         // Now we can load sound resources.
    ///         var sound = ResourceManager.LoadResource&lt;ISound&gt;("sound.mp3");
    ///
    ///         // And then use the resource.
    ///         sound.Start();
    ///     }
    /// }
    /// </code>
    /// </example>
    public void RegisterLoader<T>(ResourceLoaderBase<T> loader)
        where T : IResource
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(ResourceManager));
        }

        if (loader == null)
        {
            throw new ArgumentNullException(nameof(loader));
        }

        if (this.typeToLoaderMap.ContainsKey(typeof(T)))
        {
            throw new ArgumentException($"The specified {nameof(T)} parameter has already been registered to a resource loader.", nameof(T));
        }

        this.typeToLoaderMap.Add(typeof(T), loader);
    }

    /// <summary>
    /// Unloads the specified <paramref name="resource" /> from this <see cref="IResourceManager" />.
    /// </summary>
    /// <param name="resource">
    /// The resource to unload.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="resource" /> parameter cannot be null.
    /// </exception>
    /// <example>
    /// Unloading resources is pretty straightforward. Here is an example of unloading a loaded resource:
    /// <code title="UnloadResourceExample.cs">
    /// public class Game : GameBase
    /// {
    ///     private IResource resource;
    ///
    ///     protected override void LoadResources()
    ///     {
    ///         resource = this.ResourceManager.LoadResource&lt;ITexture2D&gt;("Resources\\Textures\\mac.jpg");
    ///
    /// #if WINDOWS
    ///         // If we're running on windows, unload the resource.
    ///         // Dunno why you'd do this, but the use case of this
    ///         // function is pretty straightforward.
    ///         this.ResourceManager.UnloadResource(resource);
    /// #endif
    ///     }
    /// }
    /// </code>
    /// </example>
    public void UnloadResource(IResource resource)
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(ResourceManager));
        }

        if (resource == null)
        {
            throw new ArgumentNullException(nameof(resource));
        }

        for (int i = this.pathToResourceDataMap.Count - 1; i >= 0; i--)
        {
            var kvp = this.pathToResourceDataMap.ElementAt(i);
            var resourceData = kvp.Value;

            if (ReferenceEquals(resourceData.Reference, resource))
            {
                resourceData.DecrementReferenceCount();

                if (resourceData.ReferenceCount == 0)
                {
                    resourceData.Reference.Dispose();
                    this.pathToResourceDataMap.Remove(resourceData.FilePath);
                }
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
                    var kvp = this.pathToResourceDataMap.ElementAt(i);
                    var resourceData = kvp.Value;

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
    private sealed class ResourceData
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
