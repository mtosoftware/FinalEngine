// <copyright file="ToolPaneNotFoundException.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Exceptions.Layout;

using System;
using System.Runtime.Serialization;

/// <summary>
/// Provides an exception when a tool pane cannot be located by it's content identifier.
/// </summary>
/// <seealso cref="Exception" />
[Serializable]
public class ToolPaneNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ToolPaneNotFoundException"/> class.
    /// </summary>
    public ToolPaneNotFoundException()
        : base("The content identifier could not be matched to a tool pane.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolPaneNotFoundException"/> class.
    /// </summary>
    /// <param name="contentID">The content identifier of the tool pane that could not be located and caused the exception to be thrown.
    /// </param>
    public ToolPaneNotFoundException(string? contentID)
        : base($"The content identifier '{contentID}' could not be matched to a tool pane.")
    {
        this.ContentID = contentID;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolPaneNotFoundException"/> class.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
    /// </param>
    public ToolPaneNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToolPaneNotFoundException"/> class.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
    /// </param>
    protected ToolPaneNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Gets the content identifier of the tool pane that could not be located.
    /// </summary>
    /// <value>
    /// The content identifier of the tool pane that could not be located and caused the exception to be thrown; otherwise, <c>null</c>.
    /// </value>
    public string? ContentID { get; }
}
