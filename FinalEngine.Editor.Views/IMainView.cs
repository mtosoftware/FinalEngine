// <copyright file="IMainView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views
{
    using System;

    public interface IMainView
    {
        event EventHandler<EventArgs>? OnExiting;

        event EventHandler<EventArgs>? OnLoaded;
    }
}
