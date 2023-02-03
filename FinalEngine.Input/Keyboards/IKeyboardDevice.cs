// <copyright file="IKeyboardDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;

/// <summary>
///   Defines an interface that provides access to common keyboard device operations.
/// </summary>
public interface IKeyboardDevice
{
    /// <summary>
    ///   Occurs when a keyboard key is pressed.
    /// </summary>
    event EventHandler<KeyEventArgs>? KeyDown;

    /// <summary>
    ///   Occurs when a keyboard key is released.
    /// </summary>
    event EventHandler<KeyEventArgs>? KeyUp;
}
