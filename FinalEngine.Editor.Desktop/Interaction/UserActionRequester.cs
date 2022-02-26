// <copyright file="UserActionRequester.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interaction
{
    using System;
    using System.Windows;
    using FinalEngine.Editor.ViewModels.Interaction;
    using Microsoft.Extensions.Logging;
    using Ookii.Dialogs.Wpf;

    public class UserActionRequester : IUserActionRequester
    {
        private readonly ILogger<UserActionRequester> logger;

        public UserActionRequester(ILogger<UserActionRequester> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string? RequestDirectoryLocation()
        {
            var dialog = new VistaFolderBrowserDialog()
            {
                Description = "Please select a directory.",
                Multiselect = false,
                RootFolder = Environment.SpecialFolder.MyDocuments,
                ShowNewFolderButton = true,
                UseDescriptionForTitle = true,
            };

            this.logger.LogInformation("Requesting a directory location from the user...");

            return dialog.ShowDialog() == true ? dialog.SelectedPath : null;
        }

        public string? RequestFileLocation(string title, string fitler)
        {
            var dialog = new VistaOpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".feproj",
                Filter = fitler,
                FilterIndex = 0,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
                Title = title,
            };

            this.logger.LogInformation("Requesting a file location from the user...");

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public void RequestOk(string caption, string message)
        {
            this.logger.LogInformation("Requesting an OK response from the user...");
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}