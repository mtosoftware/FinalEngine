// <copyright file="IApplicationContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;

/// <summary>
/// Defines an interface that represents contextual information related to the current application.
/// </summary>
public interface IApplicationContext
{
    /// <summary>
    /// Gets the title of the application.
    /// </summary>
    /// <value>
    /// The title of the application.
    /// </value>
    string Title { get; }

    /// <summary>
    /// Gets the version of the application.
    /// </summary>
    /// <value>
    /// The version of the application.
    /// </value>
    Version Version { get; }
}
