// <copyright file="IEnvironmentContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Environment;

using System;

/// <summary>
/// Defines an interface that provides information about the current environment and platform.
/// </summary>
public interface IEnvironmentContext
{
    /// <inheritdoc cref="Environment.GetFolderPath(Environment.SpecialFolder)"/>/>
    string GetFolderPath(Environment.SpecialFolder folder);
}
