// <copyright file="UserActionRequester.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Services.Actions;

using System;
using System.Windows;
using FinalEngine.Editor.ViewModels.Services.Actions;
using Microsoft.Extensions.Logging;

public sealed class UserActionRequester : IUserActionRequester
{
    private readonly ILogger<UserActionRequester> logger;

    public UserActionRequester(ILogger<UserActionRequester> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void RequestOk(string caption, string message)
    {
        this.logger.LogInformation($"Requesting OK response from user for request: '{message}'.");
        MessageBox.Show(message, caption);
    }

    public bool RequestYesNo(string caption, string message)
    {
        this.logger.LogInformation($"Requesting YES/NO response from user for request: '{message}'.");
        return MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
    }
}
