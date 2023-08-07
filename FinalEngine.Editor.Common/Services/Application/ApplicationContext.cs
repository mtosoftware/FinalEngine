// <copyright file="ApplicationContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;
using System.Reflection;

/// <summary>
/// Provides a standard implementation of an <see cref="IApplicationContext"/>.
/// </summary>
/// <seealso cref="IApplicationContext" />
public sealed class ApplicationContext : IApplicationContext
{
    /// <inheritdoc/>
    public string Title
    {
        get { return $"Final Engine - {this.Version}"; }
    }

    /// <inheritdoc/>
    public Version Version
    {
        get { return Assembly.GetExecutingAssembly().GetName().Version!; }
    }
}
