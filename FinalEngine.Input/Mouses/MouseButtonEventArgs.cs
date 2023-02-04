// <copyright file="MouseButtonEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;

/// <summary>
/// Provides event data for the <see cref="IMouseDevice.ButtonUp"/> and <see cref="IMouseDevice.ButtonDown"/> events.
/// </summary>
/// <seealso cref="EventArgs"/>
public class MouseButtonEventArgs : EventArgs
{
    /// <summary>
    /// Gets the button that raised this event.
    /// </summary>
    /// <value>
    /// The button that raised this event.
    /// </value>
    public MouseButton Button { get; init; }
}
