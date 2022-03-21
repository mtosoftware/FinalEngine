// <copyright file="IViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    using FinalEngine.Editor.ViewModels.Docking;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Defines an interface that provides methods for creating view models.
    /// </summary>
    public interface IViewModelFactory
    {
        IDockViewModel CreateDockViewModel();

        /// <summary>
        ///   Creates the new project view model.
        /// </summary>
        /// <returns>
        ///   The newly created <see cref="INewProjectViewModel"/>.
        /// </returns>
        INewProjectViewModel CreateNewProjectViewModel();

        IProjectExplorerViewModel CreateProjectExplorerViewModel();
    }
}