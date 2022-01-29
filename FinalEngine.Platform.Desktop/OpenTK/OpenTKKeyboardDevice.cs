// <copyright file="OpenTKKeyboardDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK
{
    using System;
    using FinalEngine.Input;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using global::OpenTK.Windowing.Common;

    /// <summary>
    ///   Provides an OpenTK implementation of an <see cref="IKeyboardDevice"/>.
    /// </summary>
    /// <seealso cref="IKeyboardDevice"/>
    public class OpenTKKeyboardDevice : IKeyboardDevice
    {
        /// <summary>
        ///   The native window.
        /// </summary>
        private readonly INativeWindowInvoker nativeWindow;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OpenTKKeyboardDevice"/> class.
        /// </summary>
        /// <param name="nativeWindow">
        ///   Specifies a <see cref="INativeWindowInvoker"/> that represents the underlying native window used to hook onto keyboard events.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="nativeWindow"/> parameter is null.
        /// </exception>
        public OpenTKKeyboardDevice(INativeWindowInvoker nativeWindow)
        {
            this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow), $"The specified {nameof(nativeWindow)} parameter cannot be null.");

            this.nativeWindow.KeyUp += this.NativeWindow_KeyUp;
            this.nativeWindow.KeyDown += this.NativeWindow_KeyDown;
        }

        /// <inheritdoc/>
        public event EventHandler<KeyEventArgs>? KeyDown;

        /// <inheritdoc/>
        public event EventHandler<KeyEventArgs>? KeyUp;

        /// <summary>
        ///   Handles the <see cref="INativeWindowInvoker.KeyDown"/> event.
        /// </summary>
        /// <param name="args">
        ///   The <see cref="KeyboardKeyEventArgs"/> instance containing the event data.
        /// </param>
        private void NativeWindow_KeyDown(KeyboardKeyEventArgs args)
        {
            this.KeyDown?.Invoke(this, new KeyEventArgs()
            {
                Key = (Key)args.Key,
                Modifiers = (KeyModifiers)args.Modifiers,
            });
        }

        /// <summary>
        ///   Handles the <see cref="INativeWindowInvoker.KeyUp"/> event.
        /// </summary>
        /// <param name="args">
        ///   The <see cref="KeyboardKeyEventArgs"/> instance containing the event data.
        /// </param>
        private void NativeWindow_KeyUp(KeyboardKeyEventArgs args)
        {
            this.KeyUp?.Invoke(this, new KeyEventArgs()
            {
                Key = (Key)args.Key,
                Modifiers = (KeyModifiers)args.Modifiers,
            });
        }
    }
}