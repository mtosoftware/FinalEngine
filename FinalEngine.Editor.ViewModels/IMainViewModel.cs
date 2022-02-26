// <copyright file="IMainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System.Windows.Input;

    public interface IMainViewModel
    {
        ICommand ExitCommand { get; }

        ICommand NewProjectCommand { get; }
    }
}