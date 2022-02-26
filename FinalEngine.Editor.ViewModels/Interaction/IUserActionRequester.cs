// <copyright file="IUserActionRequester.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    public interface IUserActionRequester
    {
        string? RequestDirectoryLocation();

        string? RequestFileLocation(string title, string fitler);

        void RequestOk(string caption, string message);
    }
}