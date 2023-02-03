// <copyright file="UniformNotLocatedException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

/// <summary>
///   Provides an exception that is thrown when a uniform location couldn't be located.
/// </summary>
/// <seealso cref="Exception"/>
[Serializable]
[ExcludeFromCodeCoverage(Justification = "Exception Class")]
public class UniformNotLocatedException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="UniformNotLocatedException"/> class.
    /// </summary>
    public UniformNotLocatedException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="UniformNotLocatedException"/> class.
    /// </summary>
    /// <param name="message">
    ///   The message that describes the error, or a null reference if no message can be specified.
    /// </param>
    public UniformNotLocatedException(string? message)
        : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="UniformNotLocatedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">
    ///   The error message that explains the reason for the exception, or a null reference if no message can be specified.
    /// </param>
    /// <param name="inner">
    ///   The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
    /// </param>
    public UniformNotLocatedException(string? message, Exception? inner)
        : base(message, inner)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="UniformNotLocatedException"/> class.
    /// </summary>
    /// <param name="message">
    ///   The error message that explains the reason for the exception, or a null reference if no message can be specified.
    /// </param>
    /// <param name="uniformName">
    ///   Specifies a <see cref="string"/> that represents the uniform name that couldn't be located.
    /// </param>
    public UniformNotLocatedException(string? message, string uniformName)
        : base(message)
    {
        this.UniformName = uniformName;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="UniformNotLocatedException"/> class with serialized data.
    /// </summary>
    /// <param name="info">
    ///   The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    ///   The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
    /// </param>
    protected UniformNotLocatedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    ///   Gets the name of the uniform that wasn't located.
    /// </summary>
    /// <value>
    ///   The name of the uniform that wasn't located.
    /// </value>
    public string? UniformName { get; }
}
