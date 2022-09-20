// <copyright file="SceneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes
{
    public class SceneViewModel : PaneViewModelBase, ISceneViewModel
    {
        public SceneViewModel()
        {
            this.Title = "Scene View";
        }
    }
}