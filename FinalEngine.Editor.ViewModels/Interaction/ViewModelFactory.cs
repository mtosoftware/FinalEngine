// <copyright file="ViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    using System;
    using FinalEngine.Editor.Common.Services.Factories;
    using FinalEngine.Editor.Common.Services.Projects;
    using FinalEngine.Editor.Common.Services.Scenes;
    using FinalEngine.Editor.ViewModels.Docking;
    using FinalEngine.Editor.ViewModels.Docking.Panes;
    using FinalEngine.Editor.ViewModels.Docking.Tools;
    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IViewModelFactory"/>.
    /// </summary>
    /// <seealso cref="IViewModelFactory"/>
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IGameTimeFactory gameTimeFactory;

        /// <summary>
        ///   The messanger.
        /// </summary>
        private readonly IMessenger messenger;

        /// <summary>
        ///   The project file handler.
        /// </summary>
        private readonly IProjectFileHandler projectFileHandler;

        private readonly ISceneRenderer sceneRenderer;

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
        /// <param name="messenger">
        ///   The messanger.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="userActionRequester"/> or <paramref name="projectFileHandler"/> parameter cannot be null.
        /// </exception>
        public ViewModelFactory(
            IUserActionRequester userActionRequester,
            IProjectFileHandler projectFileHandler,
            IMessenger messenger,
            ISceneRenderer sceneRenderer,
            IGameTimeFactory gameTimeFactory)
        {
            this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
            this.projectFileHandler = projectFileHandler ?? throw new ArgumentNullException(nameof(projectFileHandler));
            this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));
            this.gameTimeFactory = gameTimeFactory ?? throw new ArgumentNullException(nameof(gameTimeFactory));
        }

        /// <summary>
        ///   Creates the dock view model.
        /// </summary>
        /// <returns>
        ///   The newly created <see cref="IDockViewModel"/>.
        /// </returns>
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
            return new NewProjectViewModel(this.userActionRequester, this.projectFileHandler, this.messenger);
        }

        /// <summary>
        ///   Creates the project explorer view model.
        /// </summary>
        /// <returns>
        ///   The newly created <see cref="IProjectExplorerViewModel"/>.
        /// </returns>
        public IProjectExplorerViewModel CreateProjectExplorerViewModel()
        {
            return new ProjectExplorerViewModel(this.messenger);
        }

        public ISceneViewModel CreateSceneViewModel()
        {
            return new SceneViewModel(this.sceneRenderer, this.gameTimeFactory);
        }
    }
}