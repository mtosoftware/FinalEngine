// <copyright file="ISaveWindowLayoutViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using CommunityToolkit.Mvvm.Input;

/// <summary>
/// Defines an interface that represents a model of the save window layout view.
/// </summary>
public interface ISaveWindowLayoutViewModel
{
    /// <summary>
    /// Gets or sets the name of the layout to be saved.
    /// </summary>
    /// <value>
    /// The name of the layout to be saved.
    /// </value>
    string LayoutName { get; set; }

    /// <summary>
    /// Gets the save command.
    /// </summary>
    /// <value>
    /// The save command.
    /// </value>
    /// <remarks>
    /// The <see cref="SaveCommand"/> is used to save a window layout based on the defined <see cref="LayoutName"/>.
    /// </remarks>
    IRelayCommand SaveCommand { get; }

    /// <summary>
    /// Gets the title of the view.
    /// </summary>
    /// <value>
    /// The title of the view.
    /// </value>
    string Title { get; }
}
