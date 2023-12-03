// <copyright file="EntityComponentTypeViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.ViewModels.Messages.Entities;

public sealed class EntityComponentTypeViewModel : ObservableObject, IEntityComponentTypeViewModel
{
    private readonly Entity entity;

    private readonly IMessenger messenger;

    private readonly Type type;

    private ICommand? addCommand;

    private string? name;

    public EntityComponentTypeViewModel(IMessenger messenger, Entity entity, Type type)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        this.type = type ?? throw new ArgumentNullException(nameof(type));

        this.Name = this.type.Name;
    }

    public ICommand AddCommand
    {
        get { return this.addCommand ??= new RelayCommand(this.AddComponent, this.CanAddComponent); }
    }

    public string Name
    {
        get { return this.name ?? string.Empty; }
        private set { this.SetProperty(ref this.name, value); }
    }

    private void AddComponent()
    {
        //// TODO: Throw exception for non-parameterless constructors.
        object? instance = Activator.CreateInstance(this.type);

        if (instance is not null and IEntityComponent component)
        {
            this.entity.AddComponent(component);
            this.messenger.Send(new EntityModifiedMessage(this.entity));
        }
    }

    private bool CanAddComponent()
    {
        return !this.entity.ContainsComponent(this.type);
    }
}
