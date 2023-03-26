// <copyright file="CodeViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

public sealed class CodeViewModel : PaneViewModelBase, ICodeViewModel
{
    public CodeViewModel()
    {
        //// TODO: Replace this with the file name to be edited.
        this.Title = "Code Editor";
    }
}
