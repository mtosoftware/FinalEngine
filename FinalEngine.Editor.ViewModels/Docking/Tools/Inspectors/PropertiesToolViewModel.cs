// <copyright file="PropertiesToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;

/// <summary>
/// Provides a standard implementation of an <see cref="IPropertiesToolViewModel"/>.
/// </summary>
/// <seealso cref="ToolViewModelBase" />
/// <seealso cref="IPropertiesToolViewModel" />
public sealed class PropertiesToolViewModel : ToolViewModelBase, IPropertiesToolViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertiesToolViewModel"/> class.
    /// </summary>
    public PropertiesToolViewModel()
    {
        this.Title = "Properties";
        this.ContentID = "Properties";
    }
}
