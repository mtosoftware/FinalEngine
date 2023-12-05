// <copyright file="ISceneViewPaneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking.Panes;

public interface ISceneViewPaneViewModel : IPaneViewModel
{
    ICommand RenderCommand { get; }
}
