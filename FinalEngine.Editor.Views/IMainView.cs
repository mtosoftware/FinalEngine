// <copyright file="IMainView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views
{
    using System;
    using FinalEngine.Editor.Views.Events;

    public interface IMainView
    {
        event EventHandler<ContentToggledEventArgs>? OnContentToggled;

        event EventHandler<EventArgs>? OnExiting;

        event EventHandler<EventArgs>? OnLoaded;

        string StatusText { get; set; }
    }
}
