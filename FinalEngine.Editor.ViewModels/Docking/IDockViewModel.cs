// <copyright file="IDockViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System.Collections.Generic;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;

public interface IDockViewModel
{
    IEnumerable<IPaneViewModel> Panes { get; }

    IEnumerable<IToolViewModel> Tools { get; }
}
