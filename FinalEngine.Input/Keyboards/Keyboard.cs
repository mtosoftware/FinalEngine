// <copyright file="Keyboard.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides a standard implementation of an <see cref="IKeyboard" />, that interfaces with an <see cref="IKeyboardDevice" />.
/// </summary>
/// <remarks>
/// In almost all cases, you should never need to instantiate a <see cref="Keyboard"/>. Generally speaking, you should use the <see cref="IKeyboard"/> interface provided to you via the runtime factory. The only instance where you should instantiate a <see cref="Keyboard"/> is if you choose to roll your own game class and not use the one provided by the runtime library; an example of this might be if you were to implement your own runtime for the engine.
/// </remarks>
/// <example>
/// Below you'll find an example of how to handle input state changes with the Keyboard class. Please note that you should generally use the IKeyboard interface via dependency injection and this example is just to showcase the bare minimum required to use this implementation.
/// <code title="InputExample.cs">
/// namespace MyInputExample;
///
/// using System;
/// using FinalEngine.Input.Keyboards;
///
/// public class InputExample
/// {
///     private readonly Keyboard keyboard;
///
///     public InputExample(Keyboard keyboard)
///     {
///         // Generally speaking you should inject the IKeyboard interface.
///         this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
///     }
///
///     public void HandleInput()
///     {
///         // Check if a key is being held down.
///         if (this.keyboard.IsKeyDown(Key.A))
///         {
///             Console.WriteLine("The A key is being held down.");
///         }
///
///         // Check if a key has been pressed.
///         if (this.keyboard.IsKeyPressed(Key.B))
///         {
///             Console.WriteLine("The B key has been pressed.");
///         }
///
///         // Check if a key has been released.
///         if (this.keyboard.IsKeyReleased(Key.Space))
///         {
///             Console.WriteLine("The space bar key has been released.");
///         }
///
///         // Also check for lockable keys.
///         if (this.keyboard.IsCapsLocked &amp;&amp; this.keyboard.IsKeyReleased(Key.F))
///         {
///             Console.WriteLine("Wow, the caps lock key is locked AND you pressed the F key!");
///         }
///
///         // Lastly, there is also modifier keys that can be checked via properties as well.
///         if (this.keyboard.IsAltDown &amp;&amp; this.keyboard.IsKeyPressed(Key.F4))
///         {
///             Environment.Exit(0);
///         }
///
///         // If you're using a GameContainerBase implementation you won't need to invoke this method.
///         // But, if you aren't you will need to invoke the method after handling input state changes.
///         this.keyboard.Update();
///     }
/// }</code>
/// </example>
/// <seealso cref="IKeyboard" />
public class Keyboard : IKeyboard, IDisposable
{
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
    ///   Specifies an <see cref="IKeyboardDevice"/> that represents the keyboard device to listen to.
    /// </param>
    /// <remarks>
    ///   The <paramref name="device"/> parameter is nullable, when set to null the events are not hooked and therefore the object will not listen out for keyboard events. This can be useful in situations where the end-user might not have a keyboard or require a keyboard on the underlying platform (for example, a mobile device).
    /// </remarks>
    public Keyboard(IKeyboardDevice? device)
    {
        this.device = device;

        this.keysDown = new List<Key>();
        this.keysDownLast = new List<Key>();

        if (this.device != null)
        {
            this.device.KeyDown += this.Device_KeyDown;
            this.device.KeyUp += this.Device_KeyUp;
        }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Keyboard"/> class.
    /// </summary>
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

    /// <summary>
    /// Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
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
    public void Update()
    {
        this.keysDownLast = new List<Key>(this.keysDown);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
    ///   Specifies a <see cref="KeyEventArgs"/> containing the event data.
    /// </param>
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
    ///   Handles the <see cref="IKeyboardDevice.KeyUp"/> event.
    /// </summary>
    /// <param name="sender">
    ///   Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    /// <param name="e">
    ///   Specifies a <see cref="KeyEventArgs"/> containing the event data.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="e"/> parameter cannot be null.
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
