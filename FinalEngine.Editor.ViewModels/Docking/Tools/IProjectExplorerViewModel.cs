// <copyright file="IProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public interface IProjectExplorerViewModel : IToolViewModel
    {
        bool CanViewToolBar { get; set; }

        ICommand CollapseAllCommand { get; }

        ICommand ExpandAllCommand { get; }

        ObservableCollection<FileItemViewModel> FileNodes { get; }
    }
}