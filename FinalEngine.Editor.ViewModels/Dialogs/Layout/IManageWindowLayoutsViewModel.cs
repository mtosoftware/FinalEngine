// <copyright file="IManageWindowLayoutsViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

/// <summary>
/// Defines an interface that represents a model of the manage window layouts view.
/// </summary>
public interface IManageWindowLayoutsViewModel
{
    /// <summary>
    /// Gets the apply command.
    /// </summary>
    /// <value>
    /// The apply command.
    /// </value>
    /// <remarks>
    /// The <see cref="ApplyCommand"/> is used to apply the current <see cref="SelectedItem"/> as the new window layout.
    /// </remarks>
    IRelayCommand ApplyCommand { get; }

    /// <summary>
    /// Gets the delete command.
    /// </summary>
    /// <value>
    /// The delete command.
    /// </value>
    /// <remarks>
    /// The <see cref="DeleteCommand"/> is used to delete the current <see cref="SelectedItem"/> from the view and delete it from the applications roaming data.
    /// </remarks>
    IRelayCommand DeleteCommand { get; }

    /// <summary>
    /// Gets the layout names.
    /// </summary>
    /// <value>
    /// The layout names.
    /// </value>
    /// <remarks>
    /// The <see cref="LayoutNames"/> property refers to a list of names for each window layout located in the applications roaming data.
    /// </remarks>
    IEnumerable<string> LayoutNames { get; }

    /// <summary>
    /// Gets the selected item.
    /// </summary>
    /// <value>
    /// The selected item.
    /// </value>
    /// <remarks>
    /// The <see cref="SelectedItem"/> refers to the currently selected layout name from the collection of <see cref="LayoutNames"/>.
    /// </remarks>
    string? SelectedItem { get; }

    /// <summary>
    /// Gets the title of the view.
    /// </summary>
    /// <value>
    /// The title of the view.
    /// </value>
    string Title { get; }
}
