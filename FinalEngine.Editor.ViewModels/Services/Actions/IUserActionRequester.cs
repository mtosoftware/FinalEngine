// <copyright file="IUserActionRequester.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Actions;

/// <summary>
/// Defines an interface that provides methods that require action to be taken by the application user.
/// </summary>
public interface IUserActionRequester
{
    /// <summary>
    /// Requests the user to accept the specified <paramref name="message"/>.
    /// </summary>
    /// <param name="caption">
    /// The caption of request.
    /// </param>
    /// <param name="message">
    /// The message of the request.
    /// </param>
    void RequestOk(string caption, string message);

    /// <summary>
    /// Requests the user to accept or decline the specified <paramref name="message"/>.
    /// </summary>
    /// <param name="caption">
    /// The caption of the request.
    /// </param>
    /// <param name="message">
    /// The message of the request.
    /// </param>
    /// <returns>
    /// <c>true</c>, if the user accepted the request; otherwise, <c>false</c>.
    /// </returns>
    bool RequestYesNo(string caption, string message);
}
