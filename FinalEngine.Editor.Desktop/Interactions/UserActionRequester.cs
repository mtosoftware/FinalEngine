// <copyright file="UserActionRequester.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interactions;

using System;
using System.Windows.Forms;
using FinalEngine.Editor.ViewModels.Interaction;
using Microsoft.Extensions.Logging;

//// TODO: Determine how dialogs can know what parent view created it.
public sealed class UserActionRequester : IUserActionRequester
{
    private readonly ILogger<UserActionRequester> logger;

    public UserActionRequester(ILogger<UserActionRequester> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public string? RequestDirectoryLocation()
    {
        var dialog = new FolderBrowserDialog()
        {
            Description = "Please select a directory.",
            RootFolder = Environment.SpecialFolder.MyDocuments,
            ShowNewFolderButton = true,
            UseDescriptionForTitle = true,
        };

        this.logger.LogInformation("Requesting a directory location from the user...");

        return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
    }
}
