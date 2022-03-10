// <copyright file="IUserActionRequester.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    /// <summary>
    ///   Defines an interface that provides methods for interacting with the user.
    /// </summary>
    public interface IUserActionRequester
    {
        /// <summary>
        ///   Requests a directory location from the user.
        /// </summary>
        /// <returns>
        ///   The directory location or <c>null</c> if one was not specified.
        /// </returns>
        string? RequestDirectoryLocation();

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
        string? RequestFileLocation(string title, string fitler);

        /// <summary>
        ///   Requests an OK response from the user.
        /// </summary>
        /// <param name="caption">
        ///   The caption of the request.
        /// </param>
        /// <param name="message">
        ///   The message of the request.
        /// </param>
        void RequestOk(string caption, string message);
    }
}