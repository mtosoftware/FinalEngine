// <copyright file="MouseScrollEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses
{
    using System;

    /// <summary>
    ///   Provides event data for the <see cref="IMouseDevice.Scroll"/> event.
    /// </summary>
    /// <seealso cref="EventArgs"/>
    public class MouseScrollEventArgs : EventArgs
    {
        /// <summary>
        ///   Gets the Y-offset of the scroll wheel.
        /// </summary>
        /// <value>
        ///   The Y-offset of the scroll wheel.
        /// </value>
        public double Offset { get; init; }
    }
}
