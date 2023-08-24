// <copyright file="KeyEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;

/// <summary>
/// Provides event data for the <see cref="IKeyboardDevice.KeyUp"/> and <see cref="IKeyboardDevice.KeyDown"/> events.
/// </summary>
/// <seealso cref="EventArgs"/>
public class KeyEventArgs : EventArgs
{
    /// <summary>
    /// Gets a value indicating whether the (left or right) alt key was held down when this event was raised.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the (left or right) alt key was held down; otherwise, <c>false</c>.
    /// </value>
    public bool Alt
    {
        get { return (this.Modifiers & KeyModifiers.Alt) != 0; }
    }

    /// <summary>
    /// Gets a value indicating whether the caps-lock key was locked when this event was raised.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the caps-lock key is locked; otherwise, <c>false</c>.
    /// </value>
    public bool CapsLock
    {
        get { return (this.Modifiers & KeyModifiers.CapsLock) != 0; }
    }

    /// <summary>
    /// Gets a value indicating whether the (left or right) control key was held down when this event was raised.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the (left or right) control key was held down; otherwise, <c>false</c>.
    /// </value>
    public bool Control
    {
        get { return (this.Modifiers & KeyModifiers.Control) != 0; }
    }

    /// <summary>
    /// Gets the <see cref="FinalEngine.Input.Keyboards.Key"/> that raised this event.
    /// </summary>
    ///
    /// <value>
    /// The <see cref="FinalEngine.Input.Keyboards.Key"/> that raised this event.
    /// </value>
    public Key Key { get; init; }

    /// <summary>
    /// Gets the <see cref="KeyModifiers"/> that were pressed (or locked) when this event was raised.
    /// </summary>
    ///
    /// <value>
    /// The modifiers that were pressed (or locked).
    /// </value>
    public KeyModifiers Modifiers { get; init; }

    /// <summary>
    /// Gets a value indicating whether the num-lock key was locked when this event was raised.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the num-lock key is locked; otherwise, <c>false</c>.
    /// </value>
    public bool NumLock
    {
        get { return (this.Modifiers & KeyModifiers.NumLock) != 0; }
    }

    /// <summary>
    /// Gets a value indicating whether the (left or right) shift key was held down when this event was raised.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the (left or right) shift key was held down; otherwise, <c>false</c>.
    /// </value>
    public bool Shift
    {
        get { return (this.Modifiers & KeyModifiers.Shift) != 0; }
    }

    /// <summary>
    /// Gets a value indicating whether any super keys were held down when this event was raised.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if any of the super keys were held down; otherwise, <c>false</c>.
    /// </value>
    public bool Super
    {
        get { return (this.Modifiers & KeyModifiers.Super) != 0; }
    }
}
