// <copyright file="ContentToggledEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views.Events
{
    using System;
    using FinalEngine.Editor.Views.Interactions;

    public class ContentToggledEventArgs : EventArgs
    {
        public ContentToggledEventArgs(ITogglable togglable)
        {
            this.Togglable = togglable ?? throw new ArgumentNullException(nameof(togglable));
        }

        public ITogglable Togglable { get; }
    }
}
