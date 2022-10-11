// <copyright file="ICreateEntityView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Views.Dialogs
{
    using System;

    public interface ICreateEntityView
    {
        event EventHandler? OnAddComponent;

        event EventHandler? OnOk;

        event EventHandler? OnRemoveComponent;
    }
}
