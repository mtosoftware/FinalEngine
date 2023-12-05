// <copyright file="IDockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System.Collections.Generic;
using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;

public interface IDockViewModel
{
    ICommand LoadCommand { get; }

    IEnumerable<IPaneViewModel> Panes { get; }

    IEnumerable<IToolViewModel> Tools { get; }

    ICommand UnloadCommand { get; }
}
