// <copyright file="EnvironmentContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Environment;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides a standard implementation of an <see cref="IEnvironmentContext"/>.
/// </summary>
/// <seealso cref="IEnvironmentContext" />
[ExcludeFromCodeCoverage(Justification = "Invocation")]
public sealed class EnvironmentContext : IEnvironmentContext
{
    /// <inheritdoc/>
    public string GetFolderPath(Environment.SpecialFolder folder)
    {
        return Environment.GetFolderPath(folder);
    }
}
