// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    using System;

    /// <summary>
    ///   Provides a base resource loader that can load a resource of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    ///   The resource to be loaded from this <see cref="ResourceLoaderBase{T}"/>.
    /// </typeparam>
    /// <seealso cref="IResourceLoaderInternal"/>
    public abstract class ResourceLoaderBase<T> : IResourceLoaderInternal
        where T : IResource
    {
        /// <summary>
        ///   Loads a resource of the specified <typeparamref name="T"/> from the specified <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">
        ///   The file path to load the resource.
        /// </param>
        /// <returns>
        ///   The newly loaded resource.
        /// </returns>
        public abstract T LoadResource(string filePath);

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
        ///   filePath - The specified <paramref name="filePath"/> parameter cannot be null, empty or consist of only whitespace characters.
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
}
