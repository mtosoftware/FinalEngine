// <copyright file="ViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    using System;
    using FinalEngine.Editor.Common.Services;
    using FinalEngine.Editor.ViewModels.Docking;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IViewModelFactory"/>.
    /// </summary>
    /// <seealso cref="IViewModelFactory"/>
    public class ViewModelFactory : IViewModelFactory
    {
        /// <summary>
        ///   The project file handler.
        /// </summary>
        private readonly IProjectFileHandler projectFileHandler;

        /// <summary>
        ///   The user action requester.
        /// </summary>
        private readonly IUserActionRequester userActionRequester;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ViewModelFactory"/> class.
        /// </summary>
        /// <param name="userActionRequester">
        ///   The user action requester.
        /// </param>
        /// <param name="projectFileHandler">
        ///   The project file handler.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="userActionRequester"/> or <paramref name="projectFileHandler"/> parameter cannot be null.
        /// </exception>
        public ViewModelFactory(IUserActionRequester userActionRequester, IProjectFileHandler projectFileHandler)
        {
            this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
            this.projectFileHandler = projectFileHandler ?? throw new ArgumentNullException(nameof(projectFileHandler));
        }

        public IDockViewModel CreateDockViewModel()
        {
            return new DockViewModel(this);
        }

        /// <summary>
        ///   Creates the new project view model.
        /// </summary>
        /// <returns>
        ///   The newly created <see cref="INewProjectViewModel"/>.
        /// </returns>
        public INewProjectViewModel CreateNewProjectViewModel()
        {
            return new NewProjectViewModel(this.userActionRequester, this.projectFileHandler);
        }

        public IProjectExplorerViewModel CreateProjectExplorerViewModel()
        {
            return new ProjectExplorerViewModel(this.projectFileHandler);
        }
    }
}