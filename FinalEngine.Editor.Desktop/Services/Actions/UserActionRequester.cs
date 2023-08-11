// <copyright file="UserActionRequester.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Services.Actions;

using System;
using System.Windows;
using FinalEngine.Editor.ViewModels.Services.Actions;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IUserActionRequester"/>.
/// </summary>
/// <seealso cref="IUserActionRequester" />
public sealed class UserActionRequester : IUserActionRequester
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<UserActionRequester> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserActionRequester"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="logger"/> parameter cannot be null.
    /// </exception>
    public UserActionRequester(ILogger<UserActionRequester> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public void RequestOk(string caption, string message)
    {
        this.logger.LogDebug($"Requesting OK response from user for request: '{message}'.");
        MessageBox.Show(message, caption);
    }

    /// <inheritdoc/>
    public bool RequestYesNo(string caption, string message)
    {
        this.logger.LogDebug($"Requesting YES/NO response from user for request: '{message}'.");
        return MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
    }
}
