// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;

/// <summary>
/// Provides a base resource loader that can load a resource of type <typeparamref name="TResource"/>.
/// </summary>
/// <typeparam name="TResource">
/// The resource to be loaded from this <see cref="ResourceLoaderBase{T}" />.
/// </typeparam>
/// <remarks>
/// You should inherit from this base class when you intend to implement a way to load a resource of a type that is not currently supported by the API. This can be used to load any resource that implements IResource. Once implemented, you can use the ResourceManager to load that particular resource.
/// </remarks>
/// <example>
/// Below you'll find a simple example that loads a shader and determines its pipeline target based on a macro.
/// <code title="ShaderResourceLoader.cs">public class ShaderResourceLoader : ResourceLoaderBase&lt;IShader&gt;
/// {
///     private readonly IGPUResourceFactory factory;
///
///     private readonly IFileSystem fileSystem;
///
///     public ShaderResourceLoader(IFileSystem fileSystem, IGPUResourceFactory factory)
///     {
///         this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
///         this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
///     }
///
///     public override IShader LoadResource(string filePath)
///     {
///         if (string.IsNullOrWhiteSpace(filePath))
///         {
///             throw new ArgumentNullException(nameof(filePath));
///         }
///
///         using (var stream = this.fileSystem.OpenFile(filePath, FileAccessMode.Read))
///         {
///             using (var reader = new StreamReader(stream))
///             {
///                 string content = reader.ReadToEnd();
///                 var target = content.Contains("#define VERTEX") ? PipelineTarget.Vertex : PipelineTarget.Fragment;
///
///                 return this.factory.CreateShader(target, content);
///             }
///         }
///     }
/// }
/// </code>
/// </example>
/// <seealso cref="IResourceLoaderInternal" />
public abstract class ResourceLoaderBase<TResource> : IResourceLoaderInternal
    where TResource : IResource
{
    /// <summary>
    ///   Loads a resource of the specified <typeparamref name="TResource"/> from the specified <paramref name="filePath"/>.
    /// </summary>
    /// <param name="filePath">
    ///   The file path to load the resource.
    /// </param>
    /// <returns>
    ///   The newly loaded resource.
    /// </returns>
    /// <remarks>
    /// Override this function to implement the functionality required to load the resource of type <typeparamref name="TResource"/>.
    /// </remarks>
    public abstract TResource LoadResource(string filePath);

    /// <summary>
    ///   Loads a resource from the specified <paramref name="filePath"/>.
    /// </summary>
    /// <param name="filePath">
    ///   The file path to load the resource from.
    /// </param>
    /// <returns>
    ///   The newly loaded resource.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="filePath"/> parameter cannot be null, empty or consist of only whitespace characters.
    /// </exception>
    IResource IResourceLoaderInternal.LoadResource(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(filePath));
        }

        return this.LoadResource(filePath);
    }
}
