// <copyright file="IPropertyStringViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Components;

/// <summary>
/// Defines an interface that represents a model of a property string view.
/// </summary>
public interface IPropertyStringViewModel
{
    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    /// <value>
    /// The name of the property.
    /// </value>
    string Name { get; }

    /// <summary>
    /// Gets or sets the value of the property.
    /// </summary>
    /// <value>
    /// The value of the property.
    /// </value>
    string? Value { get; set; }
}
