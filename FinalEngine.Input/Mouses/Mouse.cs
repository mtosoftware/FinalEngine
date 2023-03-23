// <copyright file="Mouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

/// <summary>
/// Provides a standard implementation of an <see cref="IMouse" />, that interfaces with an <see cref="IMouseDevice" />.
/// </summary>
/// <remarks>
/// In almost all cases, you should never need to instantiate a <see cref="Mouse" />. Generally speaking, you should use the <see cref="IMouse" /> interface provided to you via the runtime factory. The only instance where you should instantiate a <see cref="Mouse" /> is if you choose to roll your own game class and not use the one provided by the runtime library; an example of this might be if you were to implement your own runtime for the engine.
/// </remarks>
/// <example>
/// Below you'll find an example of how to handle input state changes with the <see cref="Mouse"/> class. Please note that you should generally use the <see cref="IMouse"/> interface via dependency injection and this example is just to showcase the bare minimum required to use this implementation.
/// <code title="InputExample.cs">
/// namespace MyInputExample;
///
/// using System;
/// using FinalEngine.Input.Mouses;
///
/// public class InputExample
/// {
///     private readonly Mouse mouse;
///
///     public InputExample(Mouse mouse)
///     {
///         // Generally speaking you should inject the IMouse interface.
///         this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
///     }
///
///     public void HandleInput()
///     {
///         // Check if a button is being held down.
///         if (this.mouse.IsButtonDown(MouseButton.Left))
///         {
///             Console.WriteLine("The left mouse button is being held down.");
///         }
///
///         // Check if a mouse button has been pressed.
///         if (this.mouse.IsButtonPressed(MouseButton.Right))
///         {
///             Console.WriteLine("The right mouse button was pressed.");
///         }
///
///         // Check if a mouse button has been released.
///         if (this.mouse.IsButtonReleased(MouseButton.Right))
///         {
///             Console.WriteLine("The right mouse button was released.");
///         }
///
///         // Get the change in position of the mouse since the previous frame, good for implementing FPS cameras.
///         var delta = this.mouse.Delta;
///
///         // Get the current position of the mouse.
///         var position = this.mouse.Location;
///
///         // Lastly, you can get the scroll wheel offset, basically how much it's moved in the Y-direction.
///         double offset = this.mouse.WheelOffset;
///
///         // If we moved the mouse wheel and the X positon of the mouse is greater than 100 window pixels AND we haven't moved the mouse this frame.
///         if (offset &gt; 1 &amp;&amp; position.X &gt; 100 &amp;&amp; delta.IsEmpty)
///         {
///             Console.WriteLine("Wow, you really moved the scroll wheel without moving the mouse? Steady hands!");
///         }
///
///         // If you're using a GameContainerBase implementation you won't need to invoke this method.
///         // But, if you aren't you will need to invoke the method after handling input state changes.
///         this.mouse.Update();
///     }
/// }</code>
/// </example>
/// <seealso cref="IMouse" />
public class Mouse : IMouse, IDisposable
{
    /// <summary>
    /// The buttons down during the current frame.
    /// </summary>
    private readonly IList<MouseButton> buttonsDown;

    /// <summary>
    /// The physical mouse device.
    /// </summary>
    private readonly IMouseDevice? device;

    /// <summary>
    /// The buttons down during the previous frame.
    /// </summary>
    private IList<MouseButton> buttonsDownLast;

    /// <summary>
    /// The cursor location as of the last time <see cref="IMouseDevice.Move"/> was raised.
    /// </summary>
    private PointF location;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mouse"/> class.
    /// </summary>
    /// <param name="device">
    /// Specifies an <see cref="IMouseDevice"/> that represents the mouse device to listen to.
    /// </param>
    /// <remarks>
    /// The <paramref name="device"/> parameter is nullable, when set to <c>null</c> the events are not hooked and therefore the object will not listen out for mouse events. This can be useful in situations where the end-user might not have a mouse or require a mouse on the underlying platform (for example, a mobile device).
    /// </remarks>
    public Mouse(IMouseDevice? device)
    {
        this.device = device;

        this.buttonsDown = new List<MouseButton>();
        this.buttonsDownLast = new List<MouseButton>();

        if (this.device != null)
        {
            this.device.ButtonDown += this.Device_ButtonDown;
            this.device.ButtonUp += this.Device_ButtonUp;
            this.device.Move += this.Device_Move;
            this.device.Scroll += this.Device_Scroll;
        }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="Mouse"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    ~Mouse()
    {
        this.Dispose(false);
    }

    /// <inheritdoc/>
    public PointF Delta
    {
        get { return this.device == null ? PointF.Empty : this.device.LocationDelta; }
    }

    /// <inheritdoc/>
    public PointF Location
    {
        get
        {
            return this.location;
        }

        set
        {
            if (this.device != null)
            {
                if (this.Location.Equals(value))
                {
                    return;
                }

                this.device.SetCursorLocation(value);
            }
        }
    }

    /// <inheritdoc/>
    public double WheelOffset { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
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
    public bool IsButtonDown(MouseButton button)
    {
        return this.buttonsDown.Contains(button);
    }

    /// <inheritdoc/>
    public bool IsButtonPressed(MouseButton button)
    {
        return this.buttonsDown.Contains(button) && !this.buttonsDownLast.Contains(button);
    }

    /// <inheritdoc/>
    public bool IsButtonReleased(MouseButton button)
    {
        return !this.buttonsDown.Contains(button) && this.buttonsDownLast.Contains(button);
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Please note that you should not need to invoke this function if you're using a game container as the base implementation takes care of it for you.
    /// </remarks>
    public void Update()
    {
        this.buttonsDownLast = new List<MouseButton>(this.buttonsDown);
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
            this.device.ButtonDown -= this.Device_ButtonDown;
            this.device.ButtonUp -= this.Device_ButtonUp;
            this.device.Move -= this.Device_Move;
            this.device.Scroll -= this.Device_Scroll;
        }

        this.IsDisposed = true;
    }

    /// <summary>
    /// Handles the <see cref="IMouseDevice.ButtonDown"/> event.
    /// </summary>
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    /// <param name="e">
    /// Specifies a <see cref="MouseButtonEventArgs"/> containing the event data.
    /// </param>
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
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    /// <param name="e">
    /// Specifies a <see cref="MouseButtonEventArgs"/> containing the event data.
    /// </param>
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
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    /// <param name="e">
    /// Specifies a <see cref="MouseMoveEventArgs"/> containing the event data.
    /// </param>
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
    /// <param name="sender">
    /// Specifies an <see cref="object"/> that represents the instance that raised the event.
    /// </param>
    /// <param name="e">
    /// Specifies a <see cref="MouseScrollEventArgs"/> containing the event data.
    /// </param>
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
