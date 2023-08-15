// <copyright file="ILayoutManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Layout;

using System.Collections.Generic;

/// <summary>
/// Defines an interface that provides methods to handle the current tool and pane window layout of the application.
/// </summary>
public interface ILayoutManager
{
    /// <summary>
    /// Determines whether a layout that matches the <paramref name="layoutName"/> exists.
    /// </summary>
    /// <param name="layoutName">
    /// The name of the layout.
    /// </param>
    /// <returns>
    ///   <c>true</c> if a layout that matches the specified <paramref name="layoutName"/> exists; otherwise, <c>false</c>.
    /// </returns>
    bool ContainsLayout(string layoutName);

    /// <summary>
    /// Deletes a layout that matches the specified <paramref name="layoutName"/>.
    /// </summary>
    /// <param name="layoutName">
    /// THe name of the layout to delete.
    /// </param>
    void DeleteLayout(string layoutName);

    /// <summary>
    /// Loads a layout that matches the specified <paramref name="layoutName"/>.
    /// </summary>
    /// <param name="layoutName">
    /// The name of the layout.
    /// </param>
    void LoadLayout(string layoutName);

    /// <summary>
    /// Loads all the layouts name that have been saved into a collection to be enumerated.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of type <see cref="string"/> that contains all the layout names.
    /// </returns>
    IEnumerable<string> LoadLayoutNames();

    /// <summary>
    /// Resets the current window layout to the default window layout.
    /// </summary>
    void ResetLayout();

    /// <summary>
    /// Saves a layout by matching it to the specified <paramref name="layoutName"/>.
    /// </summary>
    /// <param name="layoutName">
    /// The name of the layout to save.
    /// </param>
    void SaveLayout(string layoutName);

    /// <summary>
    /// Toggles the visiblity of a tool view that matches the specified <paramref name="contentID"/>.
    /// </summary>
    /// <param name="contentID">
    /// The content identifier that refers to the tool to toggle.
    /// </param>
    void ToggleToolWindow(string contentID);
}
