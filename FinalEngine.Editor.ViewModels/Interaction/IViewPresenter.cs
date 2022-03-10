// <copyright file="IViewPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    /// <summary>
    ///   Defines an interface that provides methods for presenting views to a user.
    /// </summary>
    public interface IViewPresenter
    {
        /// <summary>
        ///   Shows the new project view.
        /// </summary>
        /// <param name="newProjectViewModel">
        ///   The new project view model.
        /// </param>
        void ShowNewProjectView(INewProjectViewModel newProjectViewModel);
    }
}