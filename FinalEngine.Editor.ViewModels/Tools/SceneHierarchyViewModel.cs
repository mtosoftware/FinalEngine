// <copyright file="SceneHierarchyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Tools
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Services.Events.Entities;
    using FinalEngine.Editor.Services.Workflows.Entities.DeleteEntity;
    using FinalEngine.Editor.ViewModels.Views.Tools;
    using MediatR;
    using Micky5991.EventAggregator.Interfaces;

    public class SceneHierarchyViewModel : ViewModelBase
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IMediator mediator;

        private readonly ISceneHierarchyView view;

        private bool canPerofrmEditCommands;

        private Entity? selectedEntity;

        private int selectedIndex;

        public SceneHierarchyViewModel(ISceneHierarchyView view, IMediator mediator, IEventAggregator eventAggregator)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            this.view.OnContextOpening += this.View_OnContextOpening;
            this.view.OnContextDelete += this.View_OnContextDelete;

            this.eventAggregator.Subscribe<EntityAddedEvent>(this.OnEntityCreated);
            this.eventAggregator.Subscribe<EntityRemovedEvent>(this.OnEntityDeleted);

            this.Entities = new BindingList<Entity>();
        }

        public bool CanPerformEditCommands
        {
            get { return this.canPerofrmEditCommands; }
            private set { this.SetProperty(ref this.canPerofrmEditCommands, value); }
        }

        public BindingList<Entity> Entities { get; }

        public Entity? SelectedEntity
        {
            get { return this.selectedEntity; }
            set { this.SetProperty(ref this.selectedEntity, value); }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            private set { this.SetProperty(ref this.selectedIndex, value); }
        }

        private void OnEntityCreated(EntityAddedEvent eventData)
        {
            var entity = eventData.Entity;
            int index = 0;

            if (this.SelectedEntity != null)
            {
                index = this.Entities.IndexOf(this.SelectedEntity);
            }

            if (index == -1)
            {
                index = 0;
            }

            this.Entities.Insert(index, entity);
            this.SelectedEntity = entity;
        }

        private void OnEntityDeleted(EntityRemovedEvent eventData)
        {
            var entityToDelete = this.Entities.FirstOrDefault(x =>
            {
                return x.Identifier == eventData.Identifier;
            });

            if (entityToDelete == null)
            {
                return;
            }

            this.Entities.Remove(entityToDelete);
            this.SelectedIndex = this.Entities.Count > 0 ? 0 : -1;
        }

        private async void View_OnContextDelete(object? sender, EventArgs e)
        {
            if (this.SelectedEntity == null)
            {
                return;
            }

            var command = new DeleteEntityCommand()
            {
                Entity = this.SelectedEntity,
            };

            await this.mediator.Send(command).ConfigureAwait(true);
        }

        private void View_OnContextOpening(object? sender, EventArgs e)
        {
            this.CanPerformEditCommands = this.SelectedEntity != null;
        }
    }
}
