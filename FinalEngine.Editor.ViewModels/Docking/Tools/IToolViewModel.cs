// <copyright file="IToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools;

using FinalEngine.Editor.ViewModels.Docking.Panes;

public interface IToolViewModel : IPaneViewModel
{
    bool IsVisible { get; set; }
}
