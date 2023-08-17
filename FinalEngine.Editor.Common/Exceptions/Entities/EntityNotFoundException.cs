// <copyright file="EntityNotFoundException.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Exceptions.Entities;

using System;
using System.Runtime.Serialization;
using FinalEngine.ECS;

/// <summary>
/// Provides an exception that is thrown when an entity couldn't be found.
/// </summary>
/// <seealso cref="Exception" />
public sealed class EntityNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    public EntityNotFoundException()
        : base($"An {nameof(Entity)} was not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="uniqueIdentifier">
    /// The unique identifier of the <see cref="Entity"/> that couldn't be found.
    /// </param>
    public EntityNotFoundException(Guid uniqueIdentifier)
        : base($"An {nameof(Entity)} that matches ID: '{uniqueIdentifier}' was not found.")
    {
        this.UniqueIdentifier = uniqueIdentifier;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
    /// </param>
    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
    /// </param>
    public EntityNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Gets the unique identifier of the <see cref="Entity"/> that couldn't be found.
    /// </summary>
    /// <value>
    /// The unique identifier of the <see cref="Entity"/> that couldn't be found.
    /// </value>
    public Guid UniqueIdentifier { get; }
}
