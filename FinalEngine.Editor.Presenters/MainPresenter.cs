// <copyright file="MainPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Presenters
{
    using System;
    using FinalEngine.Editor.Presenters.Interactions;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.Views;

    /// <summary>
    ///   Represents a presenter for an <see cref="IMainView"/>.
    /// </summary>
    public class MainPresenter
    {
        /// <summary>
        ///   The application context.
        /// </summary>
        private readonly IApplicationContext applicationContext;

        /// <summary>
        ///   The main view.
        /// </summary>
        private readonly IMainView mainView;

        /// <summary>
        ///   The resource loader registrar.
        /// </summary>
        private readonly IResourceLoaderRegistrar resourceLoaderRegistrar;

        /// <summary>
        ///   Initializes a new instance of the <see cref="MainPresenter"/> class.
        /// </summary>
        /// <param name="mainView">
        ///   The main view.
        /// </param>
        /// <param name="applicationContext">
        ///   The application context.
        /// </param>
        /// <param name="resourceLoaderRegistrar">
        ///   The resource loader registrar.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="mainView"/>, <paramref name="applicationContext"/> or <paramref name="resourceLoaderRegistrar"/> parameter cannot be null.
        /// </exception>
        public MainPresenter(
            IMainView mainView,
            IApplicationContext applicationContext,
            IResourceLoaderRegistrar resourceLoaderRegistrar)
        {
            this.mainView = mainView ?? throw new System.ArgumentNullException(nameof(mainView));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.resourceLoaderRegistrar = resourceLoaderRegistrar ?? throw new ArgumentNullException(nameof(resourceLoaderRegistrar));

            this.mainView.OnLoad = this.Load;
            this.mainView.OnExit = this.Exit;
        }

        /// <summary>
        ///   Exits the application.
        /// </summary>
        private void Exit()
        {
            this.applicationContext.ExitApplication();
        }

        /// <summary>
        ///   Initializes the application to be ready for use.
        /// </summary>
        private void Load()
        {
            this.resourceLoaderRegistrar.RegisterAll();
        }
    }
}
