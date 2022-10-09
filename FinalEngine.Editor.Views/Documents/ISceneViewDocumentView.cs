// <copyright file="ISceneViewDocumentView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views.Documents
{
    using System;
    using FinalEngine.Editor.Views.Events;

    public interface ISceneViewDocumentView
    {
        event EventHandler<EventArgs>? OnLoaded;

        event EventHandler<EventArgs>? OnRender;

        event EventHandler<SizeChangedEventArgs>? OnResized;
    }
}
