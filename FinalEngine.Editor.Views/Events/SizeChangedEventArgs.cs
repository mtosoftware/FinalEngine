// <copyright file="SizeChangedEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views.Events
{
    using System;
    using System.Drawing;

    public sealed class SizeChangedEventArgs : EventArgs
    {
        public SizeChangedEventArgs(Rectangle clientRectangle)
        {
            this.ClientRectangle = clientRectangle;
        }

        public Rectangle ClientRectangle { get; }
    }
}
