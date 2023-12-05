// <copyright file="OpenTKKeyboardDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using global::OpenTK.Windowing.Common;

public class OpenTKKeyboardDevice : IKeyboardDevice
{
    public OpenTKKeyboardDevice(INativeWindowInvoker nativeWindow)
    {
        ArgumentNullException.ThrowIfNull(nativeWindow, nameof(nativeWindow));

        nativeWindow.KeyUp += this.NativeWindow_KeyUp;
        nativeWindow.KeyDown += this.NativeWindow_KeyDown;
    }

    public event EventHandler<KeyEventArgs>? KeyDown;

    public event EventHandler<KeyEventArgs>? KeyUp;

    private void NativeWindow_KeyDown(KeyboardKeyEventArgs args)
    {
        this.KeyDown?.Invoke(this, new KeyEventArgs()
        {
            Key = (Key)args.Key,
            Modifiers = (KeyModifiers)args.Modifiers,
        });
    }

    private void NativeWindow_KeyUp(KeyboardKeyEventArgs args)
    {
        this.KeyUp?.Invoke(this, new KeyEventArgs()
        {
            Key = (Key)args.Key,
            Modifiers = (KeyModifiers)args.Modifiers,
        });
    }
}
