// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;

public abstract class ResourceLoaderBase<TResource> : IResourceLoaderInternal
    where TResource : IResource
{
    public abstract TResource LoadResource(string filePath);

    IResource IResourceLoaderInternal.LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));
        return this.LoadResource(filePath);
    }
}
