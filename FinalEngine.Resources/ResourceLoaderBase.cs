// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;

/// <summary>
/// Provides an abstract class used to load resources of the specified <typeparamref name="TResource"/> type.
/// </summary>
///
/// <typeparam name="TResource">
/// The type of the resource to load.
/// </typeparam>
///
/// <remarks>
/// You should implement this interface if there is a resource type you wish to load via an <see cref="IResourceManager"/>. Please note that the implementation should (usually) be able to function without a resource manager.
/// </remarks>
///
/// <seealso cref="IResourceLoaderInternal" />
public abstract class ResourceLoaderBase<TResource> : IResourceLoaderInternal
    where TResource : IResource
{
    /// <summary>
    /// Loads a resource from the specified <paramref name="filePath"/>.
    /// </summary>
    ///
    /// <param name="filePath">
    /// The file path of the resource to load.
    /// </param>
    ///
    /// <returns>
    /// The loaded resource, of type <typeparamref name="TResource"/>.
    /// </returns>
    public abstract TResource LoadResource(string filePath);

    IResource IResourceLoaderInternal.LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));
        return this.LoadResource(filePath);
    }
}
