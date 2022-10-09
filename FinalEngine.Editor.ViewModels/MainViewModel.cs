// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.Views;
    using FinalEngine.Editor.Views.Interactions;

    public class MainViewModel : ViewModelBase
    {
        private readonly IApplicationContext application;

        private readonly IResourceLoaderRegistrar registrar;

        private readonly IMainView view;

        private string? status;

        public MainViewModel(
            IMainView view,
            IApplicationContext application,
            IResourceLoaderRegistrar registrar)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.application = application ?? throw new ArgumentNullException(nameof(application));
            this.registrar = registrar ?? throw new ArgumentNullException(nameof(registrar));

            this.view.OnLoaded += this.View_OnLoaded;
            this.view.OnExiting += this.View_OnExiting;
        }

        public string Status
        {
            get { return this.status ?? string.Empty; }
            set { this.SetProperty(ref this.status, value); }
        }

        private void View_OnExiting(object? sender, EventArgs e)
        {
            this.application.ExitApplication();
        }

        private void View_OnLoaded(object? sender, EventArgs e)
        {
            this.registrar.RegisterAll();
            this.Status = "Ready!";
        }
    }
}
