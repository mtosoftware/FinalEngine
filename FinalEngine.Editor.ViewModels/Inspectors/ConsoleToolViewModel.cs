// <copyright file="ConsoleToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using Microsoft.Extensions.Logging;

public sealed class ConsoleToolViewModel : ToolViewModelBase, IConsoleToolViewModel
{
    public ConsoleToolViewModel(ILogger<ConsoleToolViewModel> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.Title = "Console";
        this.ContentID = "Console";

        logger.LogInformation($"Initializing {this.Title}...");
    }
}
