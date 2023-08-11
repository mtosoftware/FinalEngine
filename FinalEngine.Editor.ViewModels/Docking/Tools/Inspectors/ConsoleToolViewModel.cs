// <copyright file="ConsoleToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;

using System;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IConsoleToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="IConsoleToolViewModel" />
public sealed class ConsoleToolViewModel : ToolViewModelBase, IConsoleToolViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleToolViewModel" /> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    public ConsoleToolViewModel(ILogger<ConsoleToolViewModel> logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.Title = "Console";
        this.ContentID = "Console";

        logger.LogInformation($"Initializing {this.Title}...");
    }
}
