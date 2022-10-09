// <copyright file="PresenterFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Presenters
{
    using System;
    using FinalEngine.Editor.Presenters.Interactions;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.Views;
    using FinalEngine.Rendering;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IPresenterFactory"/>.
    /// </summary>
    /// <seealso cref="IPresenterFactory"/>
    public class PresenterFactory : IPresenterFactory
    {
        /// <summary>
        ///   The application context.
        /// </summary>
        private readonly IApplicationContext applicationContext;

        /// <summary>
        ///   The render device.
        /// </summary>
        private readonly IRenderDevice renderDevice;

        /// <summary>
        ///   The resource loader registrar.
        /// </summary>
        private readonly IResourceLoaderRegistrar resourceLoaderRegistrar;

        /// <summary>
        ///   Initializes a new instance of the <see cref="PresenterFactory"/> class.
        /// </summary>
        /// <param name="applicationContext">
        ///   The application context.
        /// </param>
        /// <param name="renderDevice">
        ///   The render device.
        /// </param>
        /// <param name="resourceLoaderRegistrar">
        ///   The resource loader registrar.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="applicationContext"/>, <paramref name="renderDevice"/> or <paramref name="resourceLoaderRegistrar"/> parameter cannot be null.
        /// </exception>
        public PresenterFactory(IApplicationContext applicationContext, IRenderDevice renderDevice, IResourceLoaderRegistrar resourceLoaderRegistrar)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.resourceLoaderRegistrar = resourceLoaderRegistrar ?? throw new ArgumentNullException(nameof(resourceLoaderRegistrar));
        }

        /// <summary>
        ///   Creates a <see cref="MainPresenter"/> from the specified <paramref name="view"/>.
        /// </summary>
        /// <param name="view">
        ///   The view used to create the presetner.
        /// </param>
        /// <returns>
        ///   The newly created <see cref="MainPresenter"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="view"/> parameter cannot be null.
        /// </exception>
        public MainPresenter CreateMainPresenter(IMainView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new MainPresenter(view, this.applicationContext, this.resourceLoaderRegistrar);
        }
    }
}
