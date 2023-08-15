// <copyright file="ICloseable.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interactions;

/// <summary>
/// Defines an interface that represents an interaction to close a view.
/// </summary>
public interface ICloseable
{
    /// <summary>
    /// Closes the view.
    /// </summary>
    void Close();
}
