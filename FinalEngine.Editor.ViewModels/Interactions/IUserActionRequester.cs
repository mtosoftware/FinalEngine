// <copyright file="IUserActionRequester.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interactions;

public interface IUserActionRequester
{
    void RequestOk(string caption, string message);

    bool RequestYesNo(string caption, string message);
}
