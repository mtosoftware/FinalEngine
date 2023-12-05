// <copyright file="WindowLayoutNotFoundException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Exceptions.Layout;

using System;

[Serializable]
public class WindowLayoutNotFoundException : Exception
{
    public WindowLayoutNotFoundException()
        : base("Failed to locate a window layout.")
    {
    }

    public WindowLayoutNotFoundException(string layoutName)
        : base($"Failed to locate a window layout that matches: '{layoutName}'.")
    {
        this.LayoutName = layoutName;
    }

    public WindowLayoutNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public string? LayoutName { get; }
}
