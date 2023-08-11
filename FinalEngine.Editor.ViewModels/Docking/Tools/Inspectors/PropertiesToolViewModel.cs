// <copyright file="PropertiesToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools.Inspectors;

using System;
using Microsoft.Extensions.Logging;

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
    /// <param name="logger">
    /// The logger.
    /// </param>
    public PropertiesToolViewModel(ILogger<PropertiesToolViewModel> logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.Title = "Properties";
        this.ContentID = "Properties";

        logger.LogDebug($"Initializing {this.Title}...");
    }
}
