// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Windows.Input;
    using FinalEngine.Editor.ViewModels.Interaction;
    using Microsoft.Toolkit.Mvvm.Input;

    public class MainViewModel : IMainViewModel
    {
        private readonly IViewModelFactory viewModelFactory;

        private readonly IViewPresenter viewPresenter;

        private ICommand? exitCommand;

        private ICommand? newProjectCommand;

        public MainViewModel(IViewModelFactory viewModelFactory, IViewPresenter viewPresenter)
        {
            this.viewModelFactory = viewModelFactory ?? throw new ArgumentNullException(nameof(viewModelFactory));
            this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
        }

        public ICommand ExitCommand
        {
            get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Exit); }
        }

        public ICommand NewProjectCommand
        {
            get { return this.newProjectCommand ??= new RelayCommand(this.ShowNewProjectView); }
        }

        private void Exit(ICloseable? closeable)
        {
            if (closeable == null)
            {
                throw new ArgumentNullException(nameof(closeable));
            }

            closeable.Close();
        }

        private void ShowNewProjectView()
        {
            this.viewPresenter.ShowNewProjectView(this.viewModelFactory.CreateNewProjectViewModel());
        }
    }
}