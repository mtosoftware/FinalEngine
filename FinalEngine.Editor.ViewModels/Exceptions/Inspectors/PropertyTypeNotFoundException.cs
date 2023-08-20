// <copyright file="PropertyTypeNotFoundException.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Exceptions.Inspectors;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

/// <summary>
/// Provides an exception that is thrown when a property type cannot be found.
/// </summary>
/// <seealso cref="Exception" />
[Serializable]
[ExcludeFromCodeCoverage(Justification = "Exception")]
public class PropertyTypeNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyTypeNotFoundException"/> class.
    /// </summary>
    public PropertyTypeNotFoundException()
        : base("A property tpye was not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyTypeNotFoundException"/> class.
    /// </summary>
    /// <param name="typeName">
    /// The name of the property type.
    /// </param>
    public PropertyTypeNotFoundException(string? typeName)
        : base($"A property type of name: '{typeName}' was not found.")
    {
        this.TypeName = typeName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyTypeNotFoundException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference in Visual Basic) if no inner exception is specified.
    /// </param>
    public PropertyTypeNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyTypeNotFoundException"/> class.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
    /// </param>
    protected PropertyTypeNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Gets the name of the property type that could not be found.
    /// </summary>
    /// <value>
    /// The name of the property type that could not be found and caused the exception to be thrown.
    /// </value>
    public string? TypeName { get; }
}
