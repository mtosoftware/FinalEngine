// <copyright file="IManageWindowLayoutsViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System.Collections.Generic;
using System.Windows.Input;

public interface IManageWindowLayoutsViewModel
{
    ICommand DeleteCommand { get; }

    IEnumerable<string> LayoutNames { get; }

    string? SelectedItem { get; }

    string Title { get; }
}
