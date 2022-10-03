// <copyright file="IResource.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    using System;

    /// <summary>
    ///   Defines an interface that represents a resource that can be loaded by a <see cref="ResourceLoaderBase{T}"/>.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public interface IResource : IDisposable
    {
    }
}