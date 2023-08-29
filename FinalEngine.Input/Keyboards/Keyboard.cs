// <copyright file="Keyboard.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;
using System.Collections.Generic;

/// <summary>
/// Provides a standard implementation of an <see cref="IKeyboard"/> that allows the user to handle real time keyboard events.
/// </summary>
///
/// <example>
/// Below you'll find an example showing you how to instantiate an instance of the <see cref="Keyboard"/> implementation. This example assumes that the following criteria has been met:
///
/// <list type="bullet">
///     <item>
///         An implementation of an <see cref="IKeyboardDevice"/> has been instantiated; likely through use of an IoC container - or - manually instantiated if the user chose to roll their own implementation.
///     </item>
/// </list>
///
/// <code>
/// // One implementation that is provided by default in a separate assembly can be instantiated here for simplicity.
/// // The standard implementation of IKeyboardDevice relies on the OpenTK platform back-end.
/// // This code would be place somewhere in your games initialization stage.
/// var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
/// var keyboard = new Keyboard(keyboardDevice);
///
/// // Later on, in the game loop, we can poll for input.
/// if (keyboard.IsKeyDown(Key.Escape)
/// {
///     Console.WriteLine("The escape key was pressed.");
/// }
///
/// // Don't forget, if you're not using the engines main loop you'll need to invoke the Update method AFTER polling for input state changes.
/// // Odd and inconsistent behaviour will occur if you poll for input after invoking this method.
/// keyboard.Update();
/// </code>
/// </example>
/// <seealso cref="IKeyboard" />
/// <seealso cref="IDisposable" />
public sealed class Keyboard : IKeyboard, IDisposable
{
    /// <summary>
    /// The physical keyboard device.
    /// </summary>
    private readonly IKeyboardDevice device;

    /// <summary>
    /// The keys down during the current frame.
    /// </summary>
    private readonly IList<Key> keysDown;

    /// <summary>
    /// Indicates whether this instance is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The keys down during the previous frame.
    /// </summary>
    private IList<Key> keysDownLast;

    /// <summary>
    /// Initializes a new instance of the <see cref="Keyboard"/> class.
    /// </summary>
    ///
    /// <param name="device">
    /// The physical keyboard device to handle in real time.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="device"/> parameter cannot be null.
    /// </exception>
    public Keyboard(IKeyboardDevice device)
    {
        this.device = device ?? throw new ArgumentNullException(nameof(device));

        this.keysDown = new List<Key>();
        this.keysDownLast = new List<Key>();

        this.device.KeyDown += this.Device_KeyDown;
        this.device.KeyUp += this.Device_KeyUp;
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> key is down during the current iteration.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> key is down during the current iteration; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isDown = keyboard.IsAltDown;
    ///
    /// while (isDown)
    /// {
    ///     Console.WriteLine("The left or right ALT key is currently held down.");
    /// }
    /// </code>
    /// </example>
    public bool IsAltDown
    {
        get { return this.keysDown.Contains(Key.LeftAlt) || this.keysDown.Contains(Key.RightAlt); }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.CapsLock"/> key is currently locked.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.CapsLock"/> key is currently locked; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the CAPS LOCK key is currently in a locked state. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isLocked = keyboard.IsCapsLocked;
    ///
    /// if (isLocked)
    /// {
    ///     Console.WriteLine("The CAPS LOCK key is currently in a locked state.");
    /// }
    /// </code>
    /// </example>
    public bool IsCapsLocked { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> key is down during the current iteration.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> key is down during the current iteration; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isDown = keyboard.IsControlDown;
    ///
    /// while (isDown)
    /// {
    ///     Console.WriteLine("The left or right CONTROL key is currently held down.");
    /// }
    /// </code>
    /// </example>
    public bool IsControlDown
    {
        get { return this.keysDown.Contains(Key.LeftControl) || this.keysDown.Contains(Key.RightControl); }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.NumLock"/> key is currently locked.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.NumLock"/> key is currently locked; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the NUM LOCK key is currently in a locked state. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isLocked = keyboard.IsNumLocked;
    ///
    /// if (isLocked)
    /// {
    ///     Console.WriteLine("The NUM LOCK key is currently in a locked state.");
    /// }
    /// </code>
    /// </example>
    public bool IsNumLocked { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> key is down during the current iteration.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> key is down during the current iteration; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isDown = keyboard.IsShiftDown;
    ///
    /// while (isDown)
    /// {
    ///     Console.WriteLine("The left or right SHIFT key is currently held down.");
    /// }
    /// </code>
    /// </example>
    public bool IsShiftDown
    {
        get { return this.keysDown.Contains(Key.LeftShift) || this.keysDown.Contains(Key.RightShift); }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        if (this.isDisposed)
        {
            return;
        }

        if (this.device != null)
        {
            this.device.KeyDown -= this.Device_KeyDown;
            this.device.KeyUp -= this.Device_KeyUp;
        }

        this.isDisposed = true;
    }

    /// <summary>
    /// Determines whether the specified <paramref name="key"/> is down during the current iteration.
    /// </summary>
    ///
    /// <param name="key">
    /// Specifies a <see cref="Key"/> that represents the key to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// if (keyboard.IsKeyDown(Key.F))
    /// {
    ///     Console.WriteLine("The F key is currently held down.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="key"/> is down during the current iteration; otherwise, <c>false</c>.
    /// </returns>
    public bool IsKeyDown(Key key)
    {
        return this.keysDown.Contains(key);
    }

    /// <summary>
    /// Determines whether the specified <paramref name="key"/> has been pressed this iteration.
    /// </summary>
    ///
    /// <param name="key">
    /// Specifies a <see cref="Key"/> that represents the key to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key has been pressed during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// if (keyboard.IsKeyPressed(Key.F))
    /// {
    ///     Console.WriteLine("The F key has been pressed.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="key"/> has been pressed during this iteration; otherwise, <c>false</c>.
    /// </returns>
    public bool IsKeyPressed(Key key)
    {
        return this.keysDown.Contains(key) && !this.keysDownLast.Contains(key);
    }

    /// <summary>
    /// Determines whether the specified <paramref name="key"/> has been released since the previous iteration.
    /// </summary>
    ///
    /// <param name="key">
    /// Specifies a <see cref="Key"/> that represents the key to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key has been released during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// if (keyboard.IsKeyReleased(Key.F))
    /// {
    ///     Console.WriteLine("The F key has been released.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="key"/> has been released since the previous iteration; otherwise, <c>false</c>.
    /// </returns>
    public bool IsKeyReleased(Key key)
    {
        return !this.keysDown.Contains(key) && this.keysDownLast.Contains(key);
    }

    /// <summary>
    /// Updates this <see cref="IKeyboard"/>.
    /// </summary>
    ///
    /// <remarks>
    /// This should only be called after the user has checked for input state changes.
    /// </remarks>
    public void Update()
    {
        this.keysDownLast = new List<Key>(this.keysDown);
    }

    /// <summary>
    /// Handles the <see cref="IKeyboardDevice.KeyDown"/> event.
    /// </summary>
    ///
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    ///
    /// <param name="e">
    /// Specifies a <see cref="KeyEventArgs"/> containing the event data.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="e"/> parameter cannot be null.
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
    /// Handles the <see cref="IKeyboardDevice.KeyUp"/> event.
    /// </summary>
    ///
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    ///
    /// <param name="e">
    /// Specifies a <see cref="KeyEventArgs"/> containing the event data.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="e"/> parameter cannot be null.
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
