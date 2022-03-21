﻿// <copyright file="IProjectExplorerViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools
{
    using System.Collections.ObjectModel;

    public interface IProjectExplorerViewModel : IToolViewModel
    {
        ObservableCollection<FileItemViewModel> FileNodes { get; }
    }
}