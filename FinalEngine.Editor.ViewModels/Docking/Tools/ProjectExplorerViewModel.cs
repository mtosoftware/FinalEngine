// <copyright file="ProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    public class ProjectExplorerViewModel : ToolViewModelBase, IProjectExplorerViewModel
    {
        public ProjectExplorerViewModel()
        {
            this.Title = "Project Explorer";
        }
    }
}