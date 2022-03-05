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

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IUserActionRequester"/>.
    /// </summary>
    /// <seealso cref="IUserActionRequester"/>
    public class UserActionRequester : IUserActionRequester
    {
        /// <summary>
        ///   The logger.
        /// </summary>
        private readonly ILogger<UserActionRequester> logger;

        /// <summary>
        ///   Initializes a new instance of the <see cref="UserActionRequester"/> class.
        /// </summary>
        /// <param name="logger">
        ///   The logger.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="logger"/> parameter cannot be null.
        /// </exception>
        public UserActionRequester(ILogger<UserActionRequester> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///   Requests a directory location from the user.
        /// </summary>
        /// <returns>
        ///   The directory location or <c>null</c> if one was not specified.
        /// </returns>
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

        /// <summary>
        ///   Requests a file location from the user.
        /// </summary>
        /// <param name="title">
        ///   The title of the request.
        /// </param>
        /// <param name="fitler">
        ///   The request fitler.
        /// </param>
        /// <returns>
        ///   The file location or <c>null</c> if one was not specified.
        /// </returns>
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

        /// <summary>
        ///   Requests an OK response from the user.
        /// </summary>
        /// <param name="caption">
        ///   The caption of the request.
        /// </param>
        /// <param name="message">
        ///   The message of the request.
        /// </param>
        public void RequestOk(string caption, string message)
        {
            this.logger.LogInformation("Requesting an OK response from the user...");
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}