// <copyright file="UserActionRequester.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Services.Actions;

using System.Windows;
using FinalEngine.Editor.ViewModels.Services.Actions;

public sealed class UserActionRequester : IUserActionRequester
{
    public void RequestOk(string caption, string message)
    {
        MessageBox.Show(message, caption);
    }

    public bool RequestYesNo(string caption, string message)
    {
        return MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
    }
}
