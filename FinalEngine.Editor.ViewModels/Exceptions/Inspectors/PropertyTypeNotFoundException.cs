// <copyright file="PropertyTypeNotFoundException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Exceptions.Inspectors;

using System;

[Serializable]
public class PropertyTypeNotFoundException : Exception
{
    public PropertyTypeNotFoundException()
        : base("A property tpye was not found.")
    {
    }

    public PropertyTypeNotFoundException(string? typeName)
        : base($"A property type of name: '{typeName}' was not found.")
    {
        this.TypeName = typeName;
    }

    public PropertyTypeNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public string? TypeName { get; }
}
