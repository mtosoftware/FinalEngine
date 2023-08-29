// <copyright file="IKeyboardDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;

/// <summary>
/// Represents an interface that defines methods to provide access to common keyboard device operations.
/// </summary>
///
/// <remarks>
///   The <see cref="IKeyboardDevice"/> interface provides events that allow developers to respond to key presses and releases.
/// </remarks>
public interface IKeyboardDevice
{
    /// <summary>
    /// Occurs when a keyboard key is pressed down.
    /// </summary>
    event EventHandler<KeyEventArgs>? KeyDown;

    /// <summary>
    /// Occurs when a keyboard key is released.
    /// </summary>
    event EventHandler<KeyEventArgs>? KeyUp;
}
