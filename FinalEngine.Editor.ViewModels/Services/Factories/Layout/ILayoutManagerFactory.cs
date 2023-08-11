// <copyright file="ILayoutManagerFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Factories.Layout;

using FinalEngine.Editor.ViewModels.Services.Layout;

/// <summary>
/// Defines an interface that provides a method to instantiate an instance of <see cref="ILayoutManager"/>.
/// </summary>
public interface ILayoutManagerFactory
{
    /// <summary>
    /// Creates the <see cref="ILayoutManager"/>.
    /// </summary>
    /// <returns>
    /// The newly created <see cref="ILayoutManager"/>.
    /// </returns>
    ILayoutManager CreateManager();
}
