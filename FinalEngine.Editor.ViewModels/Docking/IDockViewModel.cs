// <copyright file="IDockViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System.Collections.Generic;
using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;

public interface IDockViewModel
{
    ICommand LoadLayoutCommand { get; }

    IEnumerable<IPaneViewModel> Panes { get; }

    ICommand SaveLayoutCommand { get; }

    ICommand ToggleToolWindowCommand { get; }

    IEnumerable<IToolViewModel> Tools { get; }
}
