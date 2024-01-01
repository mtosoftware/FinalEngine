// <copyright file="ToolPaneNotFoundException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Exceptions.Layout;

using System;

[Serializable]
public class ToolPaneNotFoundException : Exception
{
    public ToolPaneNotFoundException()
        : base("The content identifier could not be matched to a tool pane.")
    {
    }

    public ToolPaneNotFoundException(string? contentID)
        : base($"The content identifier '{contentID}' could not be matched to a tool pane.")
    {
        this.ContentID = contentID;
    }

    public ToolPaneNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public string? ContentID { get; }
}
