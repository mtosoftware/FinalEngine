// <copyright file="IMainViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System.Windows.Input;

public interface IMainViewModel
{
    ICommand AboutFinalEngineCommand { get; }

    ICommand CopyCommand { get; }

    ICommand CreateEntityCommand { get; }

    ICommand CutCommand { get; }

    ICommand DeleteCommand { get; }

    ICommand DuplicateCommand { get; }

    ICommand ExitCommand { get; }

    ICommand GithubPagesCommand { get; }

    ICommand ImportResourceCommand { get; }

    ICommand NewProjectCommand { get; }

    ICommand NewSceneCommand { get; }

    ICommand OpenProjectCommand { get; }

    ICommand OpenSceneCommand { get; }

    ICommand PasteCommand { get; }

    ICommand RedoCommand { get; }

    ICommand SaveCommand { get; }

    ICommand SceneSettingsCommand { get; }

    ICommand SelectAllCommand { get; }

    ICommand SettingsCommand { get; }

    string Title { get; }

    ICommand TogglePaneCommand { get; }

    ICommand ToggleToolbarCommand { get; }

    ICommand UndoCommand { get; }
}
