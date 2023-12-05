// <copyright file="PropertiesToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using FinalEngine.Editor.ViewModels.Services.Entities;
using Microsoft.Extensions.Logging;

public sealed class PropertiesToolViewModel : ToolViewModelBase, IPropertiesToolViewModel
{
    private readonly ILogger<PropertiesToolViewModel> logger;

    private readonly IMessenger messenger;

    private readonly IEntityComponentTypeResolver typeResolver;

    private ObservableObject? currentViewModel;

    public PropertiesToolViewModel(
        ILogger<PropertiesToolViewModel> logger,
        IEntityComponentTypeResolver typeResolver,
        IMessenger messenger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

        this.Title = "Properties";
        this.ContentID = "Properties";

        this.logger.LogInformation($"Initializing {this.Title}...");

        this.messenger.Register<EntitySelectedMessage>(this, this.HandleEntitySelected);
        this.messenger.Register<EntityDeletedMessage>(this, this.HandleEntityDeleted);
    }

    public ObservableObject? CurrentViewModel
    {
        get { return this.currentViewModel; }
        private set { this.SetProperty(ref this.currentViewModel, value); }
    }

    private void HandleEntityDeleted(object recipient, EntityDeletedMessage message)
    {
        this.ResetCurrentViewModel();
    }

    private void HandleEntitySelected(object recipient, EntitySelectedMessage message)
    {
        this.logger.LogInformation($"Changing properties view to: '{nameof(EntityInspectorViewModel)}'.");

        this.Title = "Entity Inspector";
        this.CurrentViewModel = new EntityInspectorViewModel(this.messenger, this.typeResolver, message.Entity);
    }

    private void ResetCurrentViewModel()
    {
        this.logger.LogInformation($"Reseting the properties view to: `{nameof(PropertiesToolViewModel)}`.");

        this.Title = "Properties";
        this.CurrentViewModel = null;
    }
}
