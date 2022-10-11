// <copyright file="ViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using FinalEngine.Editor.Services.Instructions;
    using FinalEngine.Editor.ViewModels.Dialogs.World;
    using FinalEngine.Editor.ViewModels.Documents;
    using FinalEngine.Editor.ViewModels.Tools;
    using FinalEngine.Editor.ViewModels.Views;
    using FinalEngine.Editor.ViewModels.Views.Dialogs;
    using FinalEngine.Editor.ViewModels.Views.Documents;
    using FinalEngine.Editor.ViewModels.Views.Tools;
    using MediatR;
    using Micky5991.EventAggregator.Interfaces;

    public class ViewModelFactory
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IInstructionsManager instructionsManaegr;

        private readonly IMediator mediator;

        public ViewModelFactory(IEventAggregator eventAggregator, IInstructionsManager instructionsManaegr, IMediator mediator)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.instructionsManaegr = instructionsManaegr ?? throw new ArgumentNullException(nameof(instructionsManaegr));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public EntityInspectorViewModel Create(IEntityInspectorView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new EntityInspectorViewModel();
        }

        public SceneViewModel Create(ISceneView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new SceneViewModel();
        }

        public EntitySystemsViewModel Create(IEntitySystemsView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new EntitySystemsViewModel();
        }

        public ConsoleViewModel Create(IConsoleView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new ConsoleViewModel();
        }

        public SceneHierarchyViewModel Create(ISceneHierarchyView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new SceneHierarchyViewModel(view, this.mediator, this.eventAggregator);
        }

        public CreateEntityViewModel Create(ICreateEntityView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new CreateEntityViewModel(view, this.mediator);
        }

        public MainViewModel Create(IMainView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new MainViewModel(view, this.mediator, this.instructionsManaegr);
        }
    }
}
