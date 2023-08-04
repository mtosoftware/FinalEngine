// <copyright file="ConsoleToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;

public sealed class ConsoleToolViewModel : ToolViewModelBase, IConsoleToolViewModel
{
    public ConsoleToolViewModel()
    {
        this.Title = "Console";
        this.ContentID = "Console";
    }
}
