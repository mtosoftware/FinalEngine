// <copyright file="ISceneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using System.Windows.Input;

public interface ISceneViewModel
{
    int ProjectionHeight { get; set; }

    int ProjectionWidth { get; set; }

    ICommand RenderCommand { get; }
}
