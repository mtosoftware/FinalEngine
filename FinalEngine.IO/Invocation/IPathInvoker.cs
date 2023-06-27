// <copyright file="IPathInvoker.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation;

using System.IO;

/// <summary>
/// Defines an interface that provides methods for invocation of the <see cref="Path"/> class.
/// </summary>
public interface IPathInvoker
{
    /// <inheritdoc cref="Path.GetExtension(string?)"/>
    string? GetExtension(string? path);
}
