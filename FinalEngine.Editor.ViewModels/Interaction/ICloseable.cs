// <copyright file="ICloseable.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction;

/// <summary>
/// Defines an interface that provides a method for closing a view.
/// </summary>
public interface ICloseable
{
    /// <summary>
    /// Closes the view.
    /// </summary>
    void Close();
}
