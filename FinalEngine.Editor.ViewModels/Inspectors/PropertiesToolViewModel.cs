// <copyright file="PropertiesToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IPropertiesToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="IPropertiesToolViewModel" />
public sealed class PropertiesToolViewModel : ToolViewModelBase, IPropertiesToolViewModel
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<PropertiesToolViewModel> logger;

    /// <summary>
    /// The messenger.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    /// The current view model to be shown in the properties view.
    /// </summary>
    private ObservableObject? currentViewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertiesToolViewModel"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="messenger">
    /// The messenger.
    /// </param>
    public PropertiesToolViewModel(
        ILogger<PropertiesToolViewModel> logger,
        IMessenger messenger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

        this.Title = "Properties";
        this.ContentID = "Properties";

        this.logger.LogInformation($"Initializing {this.Title}...");

        this.messenger.Register<EntitySelectedMessage>(this, this.HandleEntitySelected);
        this.messenger.Register<EntityDeletedMessage>(this, this.HandleEntityDeleted);
    }

    /// <inheritdoc/>
    public ObservableObject? CurrentViewModel
    {
        get { return this.currentViewModel; }
        private set { this.SetProperty(ref this.currentViewModel, value); }
    }

    /// <summary>
    /// Handles the <see cref="EntityDeletedMessage"/> and resets the view.
    /// </summary>
    /// <param name="recipient">
    /// The recipient.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    private void HandleEntityDeleted(object recipient, EntityDeletedMessage message)
    {
        this.ResetCurrentViewModel();
    }

    /// <summary>
    /// Handles the <see cref="EntitySelectedMessage"/> and updates the view.
    /// </summary>
    /// <param name="recipient">
    /// The recipient.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="message"/> parameter cannot be null.
    /// </exception>
    private void HandleEntitySelected(object recipient, EntitySelectedMessage message)
    {
        this.logger.LogInformation($"Changing properties view to: '{nameof(EntityInspectorViewModel)}'.");

        this.Title = "Entity Inspector";
        this.CurrentViewModel = new EntityInspectorViewModel(message.Entity);
    }

    /// <summary>
    /// Resets the current view model.
    /// </summary>
    private void ResetCurrentViewModel()
    {
        this.logger.LogInformation($"Reseting the properties view to: `{nameof(PropertiesToolViewModel)}`.");

        this.Title = "Properties";
        this.CurrentViewModel = null;
    }
}
