// <copyright file="Mouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;
using System.Collections.Generic;
using System.Drawing;

internal sealed class Mouse : IMouse, IDisposable
{
    private readonly List<MouseButton> buttonsDown;

    private readonly IMouseDevice device;

    private List<MouseButton> buttonsDownLast;

    private bool isDisposed;

    private PointF location;

    public Mouse(IMouseDevice device)
    {
        this.device = device ?? throw new ArgumentNullException(nameof(device));

        this.buttonsDown = [];
        this.buttonsDownLast = [];

        this.device.ButtonDown += this.Device_ButtonDown;
        this.device.ButtonUp += this.Device_ButtonUp;
        this.device.Move += this.Device_Move;
        this.device.Scroll += this.Device_Scroll;
    }

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

    public double WheelOffset { get; private set; }

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

    public bool IsButtonDown(MouseButton button)
    {
        return this.buttonsDown.Contains(button);
    }

    public bool IsButtonPressed(MouseButton button)
    {
        return this.buttonsDown.Contains(button) && !this.buttonsDownLast.Contains(button);
    }

    public bool IsButtonReleased(MouseButton button)
    {
        return !this.buttonsDown.Contains(button) && this.buttonsDownLast.Contains(button);
    }

    public void Update()
    {
        this.buttonsDownLast = new List<MouseButton>(this.buttonsDown);
    }

    private void Device_ButtonDown(object? sender, MouseButtonEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e, nameof(e));
        this.buttonsDown.Add(e.Button);
    }

    private void Device_ButtonUp(object? sender, MouseButtonEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e, nameof(e));

        while (this.buttonsDown.Contains(e.Button))
        {
            this.buttonsDown.Remove(e.Button);
        }
    }

    private void Device_Move(object? sender, MouseMoveEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e, nameof(e));
        this.location = e.Location;
    }

    private void Device_Scroll(object? sender, MouseScrollEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e, nameof(e));
        this.WheelOffset = e.Offset;
    }
}
