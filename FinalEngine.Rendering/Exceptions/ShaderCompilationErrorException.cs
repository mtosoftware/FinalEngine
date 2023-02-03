// <copyright file="ShaderCompilationErrorException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

/// <summary>
///   Provides an exception that is thrown when a shader is compiled and generates an error.
/// </summary>
/// <seealso cref="Exception"/>
[Serializable]
[ExcludeFromCodeCoverage(Justification = "Exception Class")]
public class ShaderCompilationErrorException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="ShaderCompilationErrorException"/> class.
    /// </summary>
    public ShaderCompilationErrorException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ShaderCompilationErrorException"/> class.
    /// </summary>
    /// <param name="message">
    ///   The message that describes the error, or a null reference if no message can be specified.
    /// </param>
    public ShaderCompilationErrorException(string? message)
        : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ShaderCompilationErrorException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">
    ///   The error message that explains the reason for the exception, or a null reference if no message can be specified.
    /// </param>
    /// <param name="inner">
    ///   The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
    /// </param>
    public ShaderCompilationErrorException(string? message, Exception? inner)
        : base(message, inner)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ShaderCompilationErrorException"/> class.
    /// </summary>
    /// <param name="message">
    ///   The error message that explains the reason for the exception, or a null reference if no message can be specified.
    /// </param>
    /// <param name="errorLog">
    ///   The error log that was provided by the shader compiler.
    /// </param>
    public ShaderCompilationErrorException(string? message, string errorLog)
        : base(message)
    {
        this.ErrorLog = errorLog;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ShaderCompilationErrorException"/> class with serialized data.
    /// </summary>
    /// <param name="info">
    ///   The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    ///   The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
    /// </param>
    protected ShaderCompilationErrorException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    ///   Gets the error log.
    /// </summary>
    /// <value>
    ///   The error log.
    /// </value>
    public string? ErrorLog { get; }
}
