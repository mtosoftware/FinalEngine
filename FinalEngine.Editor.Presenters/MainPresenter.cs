// <copyright file="MainPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Presenters
{
    using System;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.Views;
    using FinalEngine.Editor.Views.Events;
    using FinalEngine.Editor.Views.Interactions;

    public class MainPresenter : IDisposable
    {
        private readonly IApplicationContext applicationContext;

        private readonly IMainView mainView;

        private readonly IResourceLoaderRegistrar resourceLoaderRegistrar;

        public MainPresenter(
            IMainView mainView,
            IApplicationContext applicationContext,
            IResourceLoaderRegistrar resourceLoaderRegistrar)
        {
            this.mainView = mainView ?? throw new System.ArgumentNullException(nameof(mainView));
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.resourceLoaderRegistrar = resourceLoaderRegistrar ?? throw new ArgumentNullException(nameof(resourceLoaderRegistrar));

            this.mainView.OnLoaded += this.MainView_OnLoaded;
            this.mainView.OnExiting += this.MainView_OnExiting;
            this.mainView.OnContentToggled += this.MainView_OnContentToggled;
        }

        ~MainPresenter()
        {
            this.Dispose(false);
        }

        protected bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.mainView.OnLoaded -= this.MainView_OnLoaded;
            this.mainView.OnExiting -= this.MainView_OnExiting;

            this.IsDisposed = true;
        }

        private void MainView_OnContentToggled(object? sender, ContentToggledEventArgs e)
        {
            e.Togglable.Toggle();
        }

        private void MainView_OnExiting(object? sender, EventArgs e)
        {
            this.applicationContext.ExitApplication();
        }

        private void MainView_OnLoaded(object? sender, EventArgs e)
        {
            this.resourceLoaderRegistrar.RegisterAll();
            this.mainView.StatusText = "Ready";
        }
    }
}
