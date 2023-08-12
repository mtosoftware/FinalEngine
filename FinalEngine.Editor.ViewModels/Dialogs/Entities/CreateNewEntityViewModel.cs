// <copyright file="CreateNewEntityViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Interactions;

public sealed class CreateNewEntityViewModel : ObservableValidator, ICreateNewEntityViewModel
{
    private readonly Scene scene;

    private IRelayCommand? createCommand;

    private string? entityName;

    public CreateNewEntityViewModel(Scene scene)
    {
        this.scene = scene ?? throw new ArgumentNullException(nameof(scene));

        this.EntityName = "Entity";
    }

    public ICommand CreateCommand
    {
        get
        {
            return this.createCommand ??= new RelayCommand<ICloseable>(this.Create, x =>
            {
                return !this.HasErrors;
            });
        }
    }

    [Required(ErrorMessage = "You must provide an entity name.")]
    public string EntityName
    {
        get
        {
            return this.entityName ?? string.Empty;
        }

        set
        {
            this.SetProperty(ref this.entityName, value);
            this.createCommand?.NotifyCanExecuteChanged();
        }
    }

    public string Title
    {
        get { return "Create New Entity"; }
    }

    private void Create(ICloseable? closeable)
    {
        if (closeable == null)
        {
            throw new ArgumentNullException(nameof(closeable));
        }

        var entity = new Entity(this.EntityName);
        this.scene.AddEntity(entity);

        closeable.Close();
    }
}
