// <copyright file="IMainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking;

public interface IMainViewModel
{
    ICommand CreateEntityCommand { get; }

    IDockViewModel DockViewModel { get; }

    ICommand ExitCommand { get; }

    ICommand ManageWindowLayoutsCommand { get; }

    ICommand ResetWindowLayoutCommand { get; }

    ICommand SaveWindowLayoutCommand { get; }

    string Title { get; }

    ICommand ToggleToolWindowCommand { get; }
}
