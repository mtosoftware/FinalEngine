// <copyright file="ViewPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interaction
{
    using System;
    using FinalEngine.Editor.Desktop.Views;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Interaction;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IViewPresenter"/>.
    /// </summary>
    /// <seealso cref="IViewPresenter"/>
    public class ViewPresenter : IViewPresenter
    {
        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger<ViewPresenter> logger;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ViewPresenter"/> class.
        /// </summary>
        /// <param name="logger">
        ///   The logger.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="logger"/> parameter cannot be null.
        /// </exception>
        public ViewPresenter(ILogger<ViewPresenter> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///   Shows the new project view.
        /// </summary>
        /// <param name="newProjectViewModel">
        ///   The new project view model.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="newProjectViewModel"/> parameter cannot be null.
        /// </exception>
        public void ShowNewProjectView(INewProjectViewModel newProjectViewModel)
        {
            if (newProjectViewModel == null)
            {
                throw new ArgumentNullException(nameof(newProjectViewModel));
            }

            var view = new NewProjectView()
            {
                DataContext = newProjectViewModel,
            };

            this.logger.LogInformation("Display new project view...");

            view.ShowDialog();
        }
    }
}