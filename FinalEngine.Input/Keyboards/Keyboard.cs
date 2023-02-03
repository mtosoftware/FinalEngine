// <copyright file="Keyboard.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///   Provides a standard implementation of an <see cref="IKeyboard"/>, that interfaces with an <see cref="IKeyboardDevice"/>.
/// </summary>
/// <seealso cref="IKeyboard"/>
public class Keyboard : IKeyboard, IDisposable
{
    /// <summary>
    ///   The initial size capacity of the <see cref="keysDown"/> and <see cref="keysDownLast"/> collections.
    /// </summary>
    private const int InitialSizeCapacity = 20;

    /// <summary>
    ///   The physical keyboard device.
    /// </summary>
    private readonly IKeyboardDevice? device;

    /// <summary>
    ///   The keys down during the current frame.
    /// </summary>
    private readonly IList<Key> keysDown;

    /// <summary>
    ///   The keys down during the previous frame.
    /// </summary>
    private IList<Key> keysDownLast;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Keyboard"/> class.
    /// </summary>
    /// <param name="device">
    ///   Specifies a <see cref="IKeyboardDevice"/> that represents the keyboard device to listen to.
    /// </param>
    /// <remarks>
    ///   The <paramref name="device"/> parameter is nullable, when set to null the events are not hooked and therefore the object will not listen out for keyboard events. This can be useful in situations where the end-user might not have a keyboard or require a keyboard on the underlying platform (for example, a mobile device).
    /// </remarks>
    public Keyboard(IKeyboardDevice? device)
    {
        this.device = device;

        this.keysDown = new List<Key>(InitialSizeCapacity);
        this.keysDownLast = new List<Key>(InitialSizeCapacity);

        if (this.device != null)
        {
            this.device.KeyDown += this.Device_KeyDown;
            this.device.KeyUp += this.Device_KeyUp;
        }
    }

    [ExcludeFromCodeCoverage]
    ~Keyboard()
    {
        this.Dispose(false);
    }

    /// <inheritdoc/>
    public bool IsAltDown
    {
        get { return this.keysDown.Contains(Key.LeftAlt) || this.keysDown.Contains(Key.RightAlt); }
    }

    /// <inheritdoc/>
    public bool IsCapsLocked { get; private set; }

    /// <inheritdoc/>
    public bool IsControlDown
    {
        get { return this.keysDown.Contains(Key.LeftControl) || this.keysDown.Contains(Key.RightControl); }
    }

    /// <inheritdoc/>
    public bool IsNumLocked { get; private set; }

    /// <inheritdoc/>
    public bool IsShiftDown
    {
        get { return this.keysDown.Contains(Key.LeftShift) || this.keysDown.Contains(Key.RightShift); }
    }

    protected bool IsDisposed { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public bool IsKeyDown(Key key)
    {
        return this.keysDown.Contains(key);
    }

    /// <inheritdoc/>
    public bool IsKeyPressed(Key key)
    {
        return this.keysDown.Contains(key) && !this.keysDownLast.Contains(key);
    }

    /// <inheritdoc/>
    public bool IsKeyReleased(Key key)
    {
        return !this.keysDown.Contains(key) && this.keysDownLast.Contains(key);
    }

    /// <inheritdoc/>
    /// <remarks>
    ///   This method should only be called after the user has checked for input state changes.
    /// </remarks>
    public void Update()
    {
        this.keysDownLast = new List<Key>(this.keysDown);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (this.device != null)
        {
            this.device.KeyDown -= this.Device_KeyDown;
            this.device.KeyUp -= this.Device_KeyUp;
        }

        this.IsDisposed = true;
    }

    /// <summary>
    ///   Handles the <see cref="IKeyboardDevice.KeyDown"/> event.
    /// </summary>
    /// <param name="sender">
    ///   The sender.
    /// </param>
    /// <param name="e">
    ///   The <see cref="KeyEventArgs"/> instance containing the event data.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="e"/> parameter is null.
    /// </exception>
    private void Device_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        this.IsCapsLocked = e.CapsLock;
        this.IsNumLocked = e.NumLock;

        this.keysDown.Add(e.Key);
    }

    /// <summary>
    ///   Handles the <see cref="IKeyboardDevice.KeyUp"/> event.
    /// </summary>
    /// <param name="sender">
    ///   The sender.
    /// </param>
    /// <param name="e">
    ///   The <see cref="KeyEventArgs"/> instance containing the event data.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="e"/> parameter is null.
    /// </exception>
    private void Device_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        while (this.keysDown.Contains(e.Key))
        {
            this.keysDown.Remove(e.Key);
        }
    }
}
