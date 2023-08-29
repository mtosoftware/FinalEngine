// <copyright file="Mouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Provides a standard implementation of an <see cref="IMouse"/> that allows the user handle real time mouse events.
/// </summary>
///
/// <example>
/// Below you'll find an example showing you how to instantiate an instance of the <see cref="Mouse"/> implementation. This example assumes that the following criteria has been met:
///
/// <list type="bullet">
///     <item>
///         An implementation of an <see cref="IMouseDevice"/> has been instantiated; likely through use of an IoC container - or - manually instantiated if the user chose to roll their own implementation.
///     </item>
/// </list>
///
/// <code>
/// // One implementation that is provided by default in a separate assembly can be instantiated here for simplicity.
/// // The standard implementation of IMouseDevice relies on the OpenTK platform back-end.
/// // This code would be place somewhere in your games initialization stage.
/// var mouseDevice = new OpenTKMouseDevice(nativeWindow);
/// var mouse = new Mouse(mouseDevice);
///
/// // Later on, in the game loop, we can poll for input.
/// if (mouse.IsButtonDown(MouseButton.Left))
/// {
///     Console.WriteLine("The left mouse button is held down.");
/// }
///
/// // Don't forget, if you're not using the engines main loop you'll need to invoke the Update method AFTER polling for input state changes.
/// // Odd and inconsistent behaviour will occur if you poll for input after invoking this method.
/// mouse.Update();
/// </code>
/// </example>
/// <seealso cref="IMouse" />
/// <seealso cref="IDisposable" />
public sealed class Mouse : IMouse, IDisposable
{
    /// <summary>
    /// The buttons down during the current frame.
    /// </summary>
    private readonly IList<MouseButton> buttonsDown;

    /// <summary>
    /// The physical mouse device.
    /// </summary>
    private readonly IMouseDevice device;

    /// <summary>
    /// The buttons down during the previous frame.
    /// </summary>
    private IList<MouseButton> buttonsDownLast;

    /// <summary>
    /// Indicates whether this instance is disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The location of the cursor in window pixel coordinates.
    /// </summary>
    private PointF location;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mouse"/> class.
    /// </summary>
    ///
    /// <param name="device">
    /// The physical mouse device to handle in real time.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="device"/> parameter cannot be null.
    /// </exception>
    public Mouse(IMouseDevice device)
    {
        this.device = device ?? throw new ArgumentNullException(nameof(device));

        this.buttonsDown = new List<MouseButton>();
        this.buttonsDownLast = new List<MouseButton>();

        this.device.ButtonDown += this.Device_ButtonDown;
        this.device.ButtonUp += this.Device_ButtonUp;
        this.device.Move += this.Device_Move;
        this.device.Scroll += this.Device_Scroll;
    }

    /// <summary>
    /// Gets the change in position of the cursor during the previous frame.
    /// </summary>
    ///
    /// <value>
    /// The change in position of cursor during the previous frame.
    /// </value>
    public PointF Delta
    {
        get { return this.device.LocationDelta; }
    }

    /// <summary>
    /// Gets or sets the location of the cursor in window pixel coordinates.
    /// </summary>
    ///
    /// <value>
    /// The location of the cursor in window pixel coordinates.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to modify the location of the cursor. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isReleased = mouse.IsButtonReleased(MouseButton.Left);
    ///
    /// if (isReleased)
    /// {
    ///     // Modify the location of the cursor in window pixel coordinates.
    ///     mouse.Location = new PointF(100.0f, 100.0f);
    /// }
    /// </code>
    /// </example>
    public PointF Location
    {
        get
        {
            return this.location;
        }

        set
        {
            if (this.Location.Equals(value))
            {
                return;
            }

            this.device.SetCursorLocation(value);
        }
    }

    /// <summary>
    /// Gets the scroll wheel offset.
    /// </summary>
    ///
    /// <value>
    /// The scroll wheel offset.
    /// </value>
    public double WheelOffset { get; private set; }

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
            this.device.ButtonDown -= this.Device_ButtonDown;
            this.device.ButtonUp -= this.Device_ButtonUp;
            this.device.Move -= this.Device_Move;
            this.device.Scroll -= this.Device_Scroll;
        }

        this.isDisposed = true;
    }

    /// <summary>
    /// Determines whether the specified <paramref name="button"/> is down during the current iteration.
    /// </summary>
    ///
    /// <param name="button">
    /// Specifies a <see cref="MouseButton"/> that represents the button to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a button is being held down. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isDown = mouse.IsButtonDown(MouseButton.Right);
    ///
    /// if (isDown)
    /// {
    ///     Console.WriteLine("The left mouse button is held down.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="button"/> is down during the current iteration; otherwise, <c>false</c>.
    /// </returns>
    public bool IsButtonDown(MouseButton button)
    {
        return this.buttonsDown.Contains(button);
    }

    /// <summary>
    /// Determines whether the specified <paramref name="button"/> has been pressed this iteration.
    /// </summary>
    ///
    /// <param name="button">
    /// Specifies a <see cref="MouseButton"/> that represents the button to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a button has been pressed during the current iteration. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isPressed = mouse.IsButtonPressed(MouseButton.Middle);
    ///
    /// if (isPressed)
    /// {
    ///     Console.WriteLine("The middle button has been pressed.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="button"/> has been pressed this iteration; otherwise, <c>false</c>.
    /// </returns>
    public bool IsButtonPressed(MouseButton button)
    {
        return this.buttonsDown.Contains(button) && !this.buttonsDownLast.Contains(button);
    }

    /// <summary>
    /// Determines whether the specified <paramref name="button"/> has been released since the previous iteration.
    /// </summary>
    ///
    /// <param name="button">
    /// Specifies a <see cref="MouseButton"/> that represents the button to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key has been released since the previous iteration. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isReleased = mouse.IsButtonReleased(MouseButton.Middle);
    ///
    /// if (isPressed)
    /// {
    ///     Console.WriteLine("The middle button has been released.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="button"/> has been released since the previous iteration; otherwise, <c>false</c>.
    /// </returns>
    public bool IsButtonReleased(MouseButton button)
    {
        return !this.buttonsDown.Contains(button) && this.buttonsDownLast.Contains(button);
    }

    /// <summary>
    /// Updates this <see cref="IMouse"/>.
    /// </summary>
    ///
    /// <remarks>
    /// This should only be called after the user has checked for input state changes.
    /// </remarks>
    public void Update()
    {
        this.buttonsDownLast = new List<MouseButton>(this.buttonsDown);
    }

    /// <summary>
    /// Handles the <see cref="IMouseDevice.ButtonDown"/> event.
    /// </summary>
    ///
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    ///
    /// <param name="e">
    /// Specifies a <see cref="MouseButtonEventArgs"/> instance containing the event data.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="e"/> parameter cannot be null.
    /// </exception>
    private void Device_ButtonDown(object? sender, MouseButtonEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        this.buttonsDown.Add(e.Button);
    }

    /// <summary>
    /// Handles the <see cref="IMouseDevice.ButtonUp"/> event.
    /// </summary>
    ///
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    ///
    /// <param name="e">
    /// Specifies a <see cref="MouseButtonEventArgs"/> instance containing the event data.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="e"/> parameter cannot be null.
    /// </exception>
    private void Device_ButtonUp(object? sender, MouseButtonEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        while (this.buttonsDown.Contains(e.Button))
        {
            this.buttonsDown.Remove(e.Button);
        }
    }

    /// <summary>
    /// Handles the <see cref="IMouseDevice.Move"/> event.
    /// </summary>
    ///
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    ///
    /// <param name="e">
    /// Specifies a <see cref="MouseMoveEventArgs"/> instance containing the event data.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="e"/> parameter cannot be null.
    /// </exception>
    private void Device_Move(object? sender, MouseMoveEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        this.location = e.Location;
    }

    /// <summary>
    /// Handles the <see cref="IMouseDevice.Scroll"/> event.
    /// </summary>
    ///
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    ///
    /// <param name="e">
    /// Specifies a <see cref="MouseScrollEventArgs"/> instance containing the event data.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="e"/> parameter cannot be null.
    /// </exception>
    private void Device_Scroll(object? sender, MouseScrollEventArgs e)
    {
        if (e == null)
        {
            throw new ArgumentNullException(nameof(e));
        }

        this.WheelOffset = e.Offset;
    }
}
