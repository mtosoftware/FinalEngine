// <copyright file="ResourceLoaderNotRegisteredException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Exceptions;

using System;

/// <summary>
/// Represents an exception that is thrown when a <see cref="ResourceLoaderBase{TResource}"/> has not been registered to an <see cref="IResourceManager"/>.
/// </summary>
///
/// <remarks>
/// To register a <see cref="ResourceLoaderBase{TResource}"/> to an <see cref="IResourceManager"/> you should invoke the <see cref="IResourceManager.RegisterLoader{T}(ResourceLoaderBase{T})"/> function.
/// </remarks>
///
/// <seealso cref="Exception" />
[Serializable]
public class ResourceLoaderNotRegisteredException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceLoaderNotRegisteredException"/> class.
    /// </summary>
    public ResourceLoaderNotRegisteredException()
        : base("Resource Loader not registered.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceLoaderNotRegisteredException"/> class.
    /// </summary>
    ///
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public ResourceLoaderNotRegisteredException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceLoaderNotRegisteredException"/> class.
    /// </summary>
    ///
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    ///
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a <c>null</c> reference if no inner exception is specified.
    /// </param>
    public ResourceLoaderNotRegisteredException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
