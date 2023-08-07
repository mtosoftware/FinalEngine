// <copyright file="ConsoleToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;

using CommunityToolkit.Mvvm.Messaging;

public sealed class ConsoleToolViewModel : ToolViewModelBase, IConsoleToolViewModel
{
    public ConsoleToolViewModel(IMessenger messenger)
        : base(messenger)
    {
        this.Title = "Console";
        this.ContentID = "Console";
        this.PaneLocation = PaneLocation.Bottom;
    }
}
