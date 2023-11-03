// <copyright file="CreateEntityViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Commands;
using FinalEngine.Editor.ViewModels.Commands.Entities;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="ICreateEntityViewModel"/>.
/// </summary>
/// <seealso cref="ObservableValidator" />
/// <seealso cref="ICreateEntityViewModel" />
public sealed class CreateEntityViewModel : ObservableValidator, ICreateEntityViewModel
{
    private readonly IMementoCaretaker caretaker;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<CreateEntityViewModel> logger;

    /// <summary>
    /// The messenger.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    /// The scene manager, used to create a new entity and add it to the active scene.
    /// </summary>
    private readonly ISceneManager sceneManager;

    /// <summary>
    /// The create command.
    /// </summary>
    private IRelayCommand? createCommand;

    /// <summary>
    /// The name of the entity to create.
    /// </summary>
    private string? entityName;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEntityViewModel"/> class.
    /// </summary>
    /// <param name="messenger"></param>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="sceneManager">
    /// The scene manager, used to create a new entity and add it to the active scene.
    /// </param>
    /// <param name="caretaker"></param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> or <paramref name="sceneManager"/> parameter cannot be null.
    /// </exception>
    public CreateEntityViewModel(
        IMessenger messenger,
        ILogger<CreateEntityViewModel> logger,
        ISceneManager sceneManager,
        IMementoCaretaker caretaker)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.caretaker = caretaker ?? throw new ArgumentNullException(nameof(caretaker));

        this.EntityName = "Entity";
        this.EntityGuid = Guid.NewGuid();
    }

    /// <inheritdoc/>
    public IRelayCommand CreateCommand
    {
        get
        {
            return this.createCommand ??= new RelayCommand<ICloseable>(this.Create, x =>
            {
                return !this.HasErrors;
            });
        }
    }

    /// <inheritdoc/>
    public Guid EntityGuid { get; }

    /// <inheritdoc/>
    [Required(ErrorMessage = "You must provide an entity name.")]
    public string EntityName
    {
        get
        {
            return this.entityName ?? string.Empty;
        }

        set
        {
            this.SetProperty(ref this.entityName, value, true);
            this.CreateCommand.NotifyCanExecuteChanged();
        }
    }

    /// <inheritdoc/>
    public string Title
    {
        get { return "Create New Entity"; }
    }

    /// <summary>
    /// Adds a new entity to the active scene.
    /// </summary>
    /// <param name="closeable">
    /// The closeable interaction used to close the create entity view.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="closeable"/> parameter cannot be null.
    /// </exception>
    private void Create(ICloseable? closeable)
    {
        this.logger.LogInformation($"Creating new entity...");

        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        var memento = new CreateEntityMementoCommand(
            this.messenger,
            this.sceneManager,
            this.EntityName,
            this.EntityGuid);

        this.caretaker.Apply(memento);

        this.logger.LogInformation($"Entity with ID: '{this.EntityGuid}' created!");

        closeable.Close();
    }
}
