// <copyright file="EntityComponentViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;
using FinalEngine.Editor.ViewModels.Exceptions.Inspectors;
using FinalEngine.Editor.ViewModels.Messages.Entities;

/// <summary>
///   Provides a standard implementation of an <see cref="IEntityComponentViewModel"/>.
/// </summary>
/// <seealso cref="ObservableObject"/>
/// <seealso cref="IEntityComponentViewModel"/>
public sealed class EntityComponentViewModel : ObservableObject, IEntityComponentViewModel
{
    /// <summary>
    ///   The component to model.
    /// </summary>
    private readonly IEntityComponent component;

    /// <summary>
    ///   The entity that contains the component to be modeled.
    /// </summary>
    private readonly Entity entity;

    /// <summary>
    ///   The messenger, used to notify other views when the entity has been modified.
    /// </summary>
    private readonly IMessenger messenger;

    /// <summary>
    ///   The property view models associated with this component model.
    /// </summary>
    private readonly ObservableCollection<ObservableObject> propertyViewModels;

    /// <summary>
    ///   Indicates whether the components properties are visible.
    /// </summary>
    private bool isVisible;

    /// <summary>
    ///   The remove command, used to remove the component from the entity.
    /// </summary>
    private ICommand? removeCommand;

    /// <summary>
    ///   The toggle command.
    /// </summary>
    private ICommand? toggleCommand;

    /// <summary>
    ///   Initializes a new instance of the <see cref="EntityComponentViewModel"/> class.
    /// </summary>
    /// <param name="messenger">
    ///   The messenger.
    /// </param>
    /// <param name="component">
    ///   The component to be modeled.
    /// </param>
    /// <param name="entity">
    ///   The entity that contains the specified <paramref name="component"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="messenger"/>, <paramref name="entity"/> or <paramref name="component"/> parameter cannot be null.
    /// </exception>
    public EntityComponentViewModel(IMessenger messenger, Entity entity, IEntityComponent component)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        this.component = component ?? throw new ArgumentNullException(nameof(component));

        this.propertyViewModels = new ObservableCollection<ObservableObject>();

        this.Name = component.GetType().Name;
        this.IsVisible = true;

        foreach (var property in component.GetType().GetProperties().OrderBy(x =>
        {
            return x.Name;
        }))
        {
            if (property.GetSetMethod() == null || property.GetGetMethod() == null)
            {
                continue;
            }

            var type = property.PropertyType;
            var browsable = property.GetCustomAttribute<BrowsableAttribute>();

            if (browsable != null && !browsable.Browsable)
            {
                continue;
            }

            switch (type.Name.ToUpperInvariant())
            {
                case "STRING":
                    this.propertyViewModels.Add(new StringPropertyViewModel(component, property));
                    break;

                case "BOOLEAN":
                    this.propertyViewModels.Add(new BoolPropertyViewModel(component, property));
                    break;

                case "INT32":
                    this.propertyViewModels.Add(new IntPropertyViewModel(component, property));
                    break;

                case "DOUBLE":
                    this.propertyViewModels.Add(new DoublePropertyViewModel(component, property));
                    break;

                case "SINGLE":
                    this.propertyViewModels.Add(new FloatPropertyViewModel(component, property));
                    break;

                case "VECTOR2":
                    this.propertyViewModels.Add(new Vector2PropertyViewModel(component, property));
                    break;

                case "VECTOR3":
                    this.propertyViewModels.Add(new Vector3PropertyViewModel(component, property));
                    break;

                case "VECTOR4":
                    this.propertyViewModels.Add(new Vector4PropertyViewModel(component, property));
                    break;

                case "QUATERNION":
                    this.propertyViewModels.Add(new QuaternionPropertyViewModel(component, property));
                    break;

                default:
                    throw new PropertyTypeNotFoundException(type.Name);
            }
        }
    }

    /// <inheritdoc/>
    public bool IsVisible
    {
        get { return this.isVisible; }
        private set { this.SetProperty(ref this.isVisible, value); }
    }

    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public ICollection<ObservableObject> PropertyViewModels
    {
        get { return this.propertyViewModels; }
    }

    /// <inheritdoc/>
    public ICommand RemoveCommand
    {
        get { return this.removeCommand ??= new RelayCommand(this.Remove, this.CanRemove); }
    }

    /// <inheritdoc/>
    public ICommand ToggleCommand
    {
        get { return this.toggleCommand ??= new RelayCommand(this.Toggle); }
    }

    /// <summary>
    ///   Determines whether the component can be removed from the entity.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the component can be removed from the entity; otherwise, <c>false</c>.
    /// </returns>
    private bool CanRemove()
    {
        return this.component.GetType() != typeof(TagComponent);
    }

    /// <summary>
    ///   Removes the component from the entity and notifies other views that the entity has been modified.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///   The <see cref="Entity"/> provided to this instance does not contain an <see cref="IEntityComponent"/> of the required type.
    /// </exception>
    private void Remove()
    {
        if (!this.entity.ContainsComponent(this.component))
        {
            throw new InvalidOperationException($"The {nameof(Entity)} provided to this instance does not contain an {nameof(IEntityComponent)} of type: '{this.component.GetType()}'");
        }

        this.entity.RemoveComponent(this.component);
        this.messenger.Send(new EntityModifiedMessage(this.entity));
    }

    /// <summary>
    ///   Toggles the visibility of the components properties.
    /// </summary>
    private void Toggle()
    {
        this.IsVisible = !this.IsVisible;
    }
}
