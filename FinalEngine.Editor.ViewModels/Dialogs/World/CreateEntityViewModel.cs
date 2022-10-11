// <copyright file="CreateEntityViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.World
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using FinalEngine.Editor.Services.Workflows.Entities.CreateEntity;
    using FinalEngine.Editor.ViewModels.Views.Dialogs;
    using MediatR;

    public class CreateEntityViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        private readonly ICreateEntityView view;

        private string? entityTag;

        private Guid identifier;

        public CreateEntityViewModel(ICreateEntityView view, IMediator mediator)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            this.view.OnOk += this.View_OnOk;
            this.view.OnAddComponent += this.View_OnAddComponent;
            this.view.OnRemoveComponent += this.View_OnRemoveComponent;

            this.EntityTag = "New Entity";
            this.Identifier = Guid.NewGuid();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "You must provide an entity tag.")]
        public string EntityTag
        {
            get { return this.entityTag ?? string.Empty; }
            set { this.SetProperty(ref this.entityTag, value); }
        }

        [TypeConverter(typeof(GuidConverter))]
        public Guid Identifier
        {
            get { return this.identifier; }
            set { this.SetProperty(ref this.identifier, value); }
        }

        private void View_OnAddComponent(object? sender, EventArgs e)
        {
        }

        private async void View_OnOk(object? sender, EventArgs e)
        {
            var command = new CreateEntityCommand()
            {
                Tag = this.EntityTag,
                Identifier = this.Identifier,
            };

            await this.mediator.Send(command).ConfigureAwait(true);
        }

        private void View_OnRemoveComponent(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
