// <copyright file="ResourceLoaderNotRegisteredException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

/// <summary>
///   Provides an exception that is thrown when a call is made to <see cref="ResourceManager.LoadResource{T}(string)"/> and a resource loader for the specified type has not been registered with <see cref="ResourceManager.RegisterLoader{T}(ResourceLoaderBase{T})"/>.
/// </summary>
/// <seealso cref="Exception"/>
[Serializable]
[ExcludeFromCodeCoverage]
public class ResourceLoaderNotRegisteredException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="ResourceLoaderNotRegisteredException"/> class.
    /// </summary>
    public ResourceLoaderNotRegisteredException()
        : base("Resource Loader not registered.")
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResourceLoaderNotRegisteredException"/> class.
    /// </summary>
    /// <param name="message">
    ///   The message that describes the error.
    /// </param>
    public ResourceLoaderNotRegisteredException(string? message)
        : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResourceLoaderNotRegisteredException"/> class.
    /// </summary>
    /// <param name="message">
    ///   The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    ///   The exception that is the cause of the current exception, or a null reference ( <see langword="Nothing"/> in Visual Basic) if no inner exception is specified.
    /// </param>
    public ResourceLoaderNotRegisteredException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResourceLoaderNotRegisteredException"/> class.
    /// </summary>
    /// <param name="info">
    ///   The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    ///   The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
    /// </param>
    protected ResourceLoaderNotRegisteredException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
