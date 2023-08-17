// <copyright file="IPropertyViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing;

/// <summary>
/// Defines an interface that represents a model of a component property view.
/// </summary>
/// <typeparam name="T">
/// The type of the property to model.
/// </typeparam>
public interface IPropertyViewModel<T>
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
    T Value { get; set; }
}
