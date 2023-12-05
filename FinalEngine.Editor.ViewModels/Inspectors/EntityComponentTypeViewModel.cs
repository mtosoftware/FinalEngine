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

    public EntityComponentTypeViewModel(IMessenger messenger, Entity entity, Type type)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));

        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (!typeof(IEntityComponent).IsAssignableFrom(type))
        {
            throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IEntityComponent)}.");
        }

        this.type = type;
        this.Name = this.type.Name;
    }

    public ICommand AddCommand
    {
        get { return this.addCommand ??= new RelayCommand(this.AddComponent, this.CanAddComponent); }
    }

    public string Name { get; }

    private void AddComponent()
    {
        object? instance = Activator.CreateInstance(this.type) ?? throw new InvalidOperationException($"The entity component couldn't be instantiated for the specified type: '{this.type}'. Please ensure the component contains a default empty constructor.");

        if (instance is not IEntityComponent component)
        {
            throw new InvalidCastException($"The created instance of type '{instance.GetType()}' does not implement the {nameof(IEntityComponent)} interface.");
        }

        if (this.entity.ContainsComponent(this.type))
        {
            return;
        }

        this.entity.AddComponent(component);
        this.messenger.Send(new EntityModifiedMessage(this.entity));
    }

    private bool CanAddComponent()
    {
        return !this.entity.ContainsComponent(this.type);
    }
}
