// <copyright file="IApplicationContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;

/// <summary>
/// Defines an interface that represents contextual information related to the current application and it's associated data.
/// </summary>
public interface IApplicationContext
{
    /// <summary>
    /// Get the directory that serves as a common repository for Final Engine application-specific data for the current roaming user.
    /// </summary>
    /// <value>
    /// The directory that serves as a common repository for application-specific data for the current roaming user.
    /// </value>
    string DataDirectory { get; }

    /// <summary>
    /// Gets the title of the application.
    /// </summary>
    /// <value>
    /// The title of the application, suffixed by the <see cref="Version"/>.
    /// </value>
    string Title { get; }

    /// <summary>
    /// Gets the version of the application.
    /// </summary>
    /// <value>
    /// The assembly version of the application.
    /// </value>
    Version Version { get; }
}
