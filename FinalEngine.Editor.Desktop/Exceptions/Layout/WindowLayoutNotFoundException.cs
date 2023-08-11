// <copyright file="WindowLayoutNotFoundException.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Exceptions.Layout;

using System;
using System.Runtime.Serialization;

/// <summary>
/// Provides an exception that is thrown when a window layout could not be found.
/// </summary>
/// <seealso cref="Exception" />
public sealed class WindowLayoutNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WindowLayoutNotFoundException"/> class.
    /// </summary>
    public WindowLayoutNotFoundException()
        : base("Failed to locate a window layout.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowLayoutNotFoundException"/> class.
    /// </summary>
    /// <param name="layoutName">The name of the layout that could not be found and caused the exception to be thrown.
    /// </param>
    public WindowLayoutNotFoundException(string layoutName)
        : base($"Failed to locate a window layout that matches: '{layoutName}'.")
    {
        this.LayoutName = layoutName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowLayoutNotFoundException"/> class.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
    /// </param>
    public WindowLayoutNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowLayoutNotFoundException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
    /// </param>
    public WindowLayoutNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Gets the name of the layout that could not be found.
    /// </summary>
    /// <value>
    /// The name of the layout that could not be found and caused the exception to be thrown; otherwise, <c>null</c>.
    /// </value>
    public string? LayoutName { get; }
}
