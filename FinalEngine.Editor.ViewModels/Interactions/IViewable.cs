// <copyright file="IViewable.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interactions;

/// <summary>
/// Defines an interface that represents a relationship between a view and it's view model.
/// </summary>
/// <typeparam name="TViewModel">
/// The type of the view model.
/// </typeparam>
public interface IViewable<TViewModel>
{
    /// <summary>
    /// Gets or sets the data context.
    /// </summary>
    /// <value>
    /// The data context.
    /// </value>
    /// <remarks>
    /// The <see cref="DataContext"/> property should usually refer to the <typeparamref name="TViewModel"/> type.
    /// </remarks>
    object DataContext { get; set; }

    /// <summary>
    /// Shows the view as a dialog window and only returns once the view is closed.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the activity of the view was accepted; otherwise <c>false</c>.
    /// </returns>
    bool? ShowDialog();
}
