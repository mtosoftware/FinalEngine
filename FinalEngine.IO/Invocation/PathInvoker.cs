// <copyright file="PathInvoker.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.IO;

/// <summary>
/// Provides an implementation of an <see cref="IPathInvoker"/>.
/// </summary>
/// <seealso cref="IPathInvoker" />
[ExcludeFromCodeCoverage]
public sealed class PathInvoker : IPathInvoker
{
    /// <inheritdoc/>
    public string? GetExtension(string? path)
    {
        return Path.GetExtension(path);
    }
}
