// <copyright file="IMouseDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;
using System.Drawing;

/// <summary>
/// Represents an interface that defines methods and events that provides access to common mouse device operations.
/// </summary>
///
/// <remarks>
/// The <see cref="IMouseDevice"/> interface provides events that allow developers to respond to mouse events.
/// </remarks>
public interface IMouseDevice
{
    /// <summary>
    /// Occurs when a mouse button is pressed.
    /// </summary>
    event EventHandler<MouseButtonEventArgs>? ButtonDown;

    /// <summary>
    /// Occurs when a mouse button is released.
    /// </summary>
    event EventHandler<MouseButtonEventArgs>? ButtonUp;

    /// <summary>
    /// Occurs when the location of the mouse has changed.
    /// </summary>
    event EventHandler<MouseMoveEventArgs>? Move;

    /// <summary>
    /// Occurs when the position of the scroll wheel has changed.
    /// </summary>
    event EventHandler<MouseScrollEventArgs>? Scroll;

    /// <summary>
    /// Gets the change in position of the cursor since the last frame.
    /// </summary>
    ///
    /// <value>
    /// The change in position of the cursor since the last frame.
    /// </value>
    public PointF LocationDelta { get; }

    /// <summary>
    /// Sets the cursor location (in window pixel coordinates).
    /// </summary>
    ///
    /// <param name="location">
    /// Specifies a <see cref="PointF"/> that represents the new location.
    /// </param>
    void SetCursorLocation(PointF location);
}
