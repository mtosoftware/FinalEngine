// <copyright file="ConsoleToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;

/// <summary>
/// Provides a standard implementation of an <see cref="IConsoleToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="IConsoleToolViewModel" />
public sealed class ConsoleToolViewModel : ToolViewModelBase, IConsoleToolViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleToolViewModel"/> class.
    /// </summary>
    public ConsoleToolViewModel()
    {
        this.Title = "Console";
        this.ContentID = "Console";
    }
}
