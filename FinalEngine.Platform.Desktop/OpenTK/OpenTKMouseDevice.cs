// <copyright file="OpenTKMouseDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK;

using System;
using System.Drawing;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using global::OpenTK.Mathematics;
using TKMouseButtonEventArgs = global::OpenTK.Windowing.Common.MouseButtonEventArgs;
using TKMouseMoveEventArgs = global::OpenTK.Windowing.Common.MouseMoveEventArgs;
using TKMouseWheelEventArgs = global::OpenTK.Windowing.Common.MouseWheelEventArgs;

public class OpenTKMouseDevice : IMouseDevice
{
    private readonly INativeWindowInvoker nativeWindow;

    public OpenTKMouseDevice(INativeWindowInvoker nativeWindow)
    {
        this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow));

        this.nativeWindow.MouseUp += this.NativeWindow_MouseUp;
        this.nativeWindow.MouseDown += this.NativeWindow_MouseDown;
        this.nativeWindow.MouseMove += this.NativeWindow_MouseMove;
        this.nativeWindow.MouseWheel += this.NativeWindow_MouseWheel;
    }

    public event EventHandler<MouseButtonEventArgs>? ButtonDown;

    public event EventHandler<MouseButtonEventArgs>? ButtonUp;

    public event EventHandler<MouseMoveEventArgs>? Move;

    public event EventHandler<MouseScrollEventArgs>? Scroll;

    public PointF LocationDelta
    {
        get { return new PointF(this.nativeWindow.MouseState.Delta.X, this.nativeWindow.MouseState.Delta.Y); }
    }

    public void SetCursorLocation(PointF location)
    {
        this.nativeWindow.MousePosition = new Vector2(location.X, location.Y);
    }

    private void NativeWindow_MouseDown(TKMouseButtonEventArgs args)
    {
        this.ButtonDown?.Invoke(this, new MouseButtonEventArgs()
        {
            Button = (MouseButton)args.Button,
        });
    }

    private void NativeWindow_MouseMove(TKMouseMoveEventArgs args)
    {
        this.Move?.Invoke(this, new MouseMoveEventArgs()
        {
            Location = new PointF(args.X, args.Y),
        });
    }

    private void NativeWindow_MouseUp(TKMouseButtonEventArgs args)
    {
        this.ButtonUp?.Invoke(this, new MouseButtonEventArgs()
        {
            Button = (MouseButton)args.Button,
        });
    }

    private void NativeWindow_MouseWheel(TKMouseWheelEventArgs args)
    {
        this.Scroll?.Invoke(this, new MouseScrollEventArgs()
        {
            Offset = args.OffsetY,
        });
    }
}
