// <copyright file="IUserActionRequester.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction;

public interface IUserActionRequester
{
    /// <summary>
    ///   Requests a directory location from the user.
    /// </summary>
    /// <returns>
    ///   The directory location or <c>null</c> if one was not specified.
    /// </returns>
    string? RequestDirectoryLocation();
}
