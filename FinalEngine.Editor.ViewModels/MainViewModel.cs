// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using FinalEngine.Editor.Services.Instructions;
    using FinalEngine.Editor.Services.Workflows.Entities.DeleteEntity;
    using FinalEngine.Editor.Services.Workflows.Instructions.Redo;
    using FinalEngine.Editor.Services.Workflows.Instructions.Undo;
    using FinalEngine.Editor.ViewModels.Documents;
    using FinalEngine.Editor.ViewModels.Tools;
    using FinalEngine.Editor.ViewModels.Views;
    using MediatR;

    public class MainViewModel : ViewModelBase
    {
        private readonly IInstructionsManager instructionsManager;

        private readonly IMediator mediator;

        private readonly IMainView view;

        private bool canPerformEditCommands;

        private string? status;

        private string? title;

        public MainViewModel(
            IMainView view,
            IMediator mediator,
            IInstructionsManager instructionsManager)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));

            view.OnLoaded += this.View_OnLoaded;
            view.OnMenuEditDelete += this.View_OnMenuEditDelete;
            view.OnEditMenuOpening += this.View_OnEditMenuOpening;
            view.OnEditUndo += this.View_OnEditUndo;
            view.OnEditRedo += this.View_OnEditRedo;

            instructionsManager.InstructionsModified += this.InstructionsManager_InstructionsModified;
        }

        public bool CanPerformEditCommands
        {
            get { return this.canPerformEditCommands; }
            private set { this.SetProperty(ref this.canPerformEditCommands, value); }
        }

        public bool CanRedo
        {
            get { return this.instructionsManager.CanRedo; }
        }

        public bool CanUndo
        {
            get { return this.instructionsManager.CanUndo; }
        }

        public string Status
        {
            get { return this.status ?? string.Empty; }
            private set { this.SetProperty(ref this.status, value); }
        }

        public string Title
        {
            get { return this.title ?? string.Empty; }
            private set { this.SetProperty(ref this.title, value); }
        }

        private ConsoleViewModel Console
        {
            get { return this.view.Console; }
        }

        private EntityInspectorViewModel EntityInspector
        {
            get { return this.view.EntityInspector; }
        }

        private EntitySystemsViewModel EntitySystems
        {
            get { return this.view.EntitySystems; }
        }

        private SceneViewModel Scene
        {
            get { return this.view.Scene; }
        }

        private SceneHierarchyViewModel SceneHierarchy
        {
            get { return this.view.SceneHierarchy; }
        }

        private void InstructionsManager_InstructionsModified(object? sender, EventArgs e)
        {
            this.NotifyPropertyChanged(nameof(this.CanUndo));
            this.NotifyPropertyChanged(nameof(this.CanRedo));
        }

        private void View_OnEditMenuOpening(object? sender, EventArgs e)
        {
            this.CanPerformEditCommands = this.SceneHierarchy.SelectedEntity != null;
        }

        private void View_OnEditRedo(object? sender, EventArgs e)
        {
            this.mediator.Send(new RedoCommand());
        }

        private void View_OnEditUndo(object? sender, EventArgs e)
        {
            this.mediator.Send(new UndoCommand());
        }

        private void View_OnLoaded(object? sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var info = FileVersionInfo.GetVersionInfo(assembly.Location);

            string? productName = info.ProductName;
            string? productVersion = info.ProductVersion;

            this.Title = $"{productName} - {productVersion}";
            this.Status = "Ready 😎";
        }

        private async void View_OnMenuEditDelete(object? sender, EventArgs e)
        {
            if (this.SceneHierarchy.SelectedEntity == null)
            {
                return;
            }

            var command = new DeleteEntityCommand()
            {
                Entity = this.SceneHierarchy.SelectedEntity,
            };

            await this.mediator.Send(command).ConfigureAwait(true);
        }
    }
}
