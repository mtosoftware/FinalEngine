// <copyright file="PropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing;

using System;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;

public class PropertyViewModel<T> : ObservableValidator, IPropertyViewModel<T>
{
    private readonly object component;

    private readonly Func<T?> getValue;

    private readonly Action<T?> setValue;

    public PropertyViewModel(object component, PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property, nameof(property));

        this.component = component ?? throw new ArgumentNullException(nameof(component));

        this.getValue = new Func<T?>(() =>
        {
            return (T?)property.GetValue(this.component);
        });

        this.setValue = new Action<T?>(x =>
        {
            property.SetValue(this.component, x);
        });

        this.Name = property.Name;
        this.Value = this.getValue();
    }

    public string Name { get; }

    public virtual T? Value
    {
        get
        {
            return this.getValue();
        }

        set
        {
            this.OnPropertyChanging(nameof(this.Value));
            this.setValue(value);
            this.OnPropertyChanged(nameof(this.Value));
            this.ValidateProperty(this.Value, nameof(this.Value));
        }
    }
}
