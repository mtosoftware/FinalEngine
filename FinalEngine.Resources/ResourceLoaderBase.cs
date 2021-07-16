// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    using System;

    public abstract class ResourceLoaderBase<T> : IResourceLoaderInternal
        where T : IResource
    {
        public abstract T LoadResource(string filePath);

        IResource IResourceLoaderInternal.LoadResource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), $"The specified {nameof(filePath)} parameter cannot be null.");
            }

            return this.LoadResource(filePath);
        }
    }
}