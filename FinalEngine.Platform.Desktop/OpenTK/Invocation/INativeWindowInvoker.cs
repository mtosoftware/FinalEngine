// <copyright file="INativeWindowInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::OpenTK.Mathematics;
    using global::OpenTK.Windowing.Common;
    using global::OpenTK.Windowing.Desktop;

    /// <summary>
    ///   Defines an interface that provides methods for invocation of a <see cref="NativeWindow"/>.
    /// </summary>
    [SuppressMessage("Design", "CA1003:Use generic event handler instances", Justification = "Required by Invocation")]
    public interface INativeWindowInvoker : IDisposable
    {
        /// <inheritdoc cref="NativeWindow.KeyDown"/>
        event Action<KeyboardKeyEventArgs> KeyDown;

        /// <inheritdoc cref="NativeWindow.KeyUp"/>
        event Action<KeyboardKeyEventArgs> KeyUp;

        /// <inheritdoc cref="NativeWindow.MouseDown"/>
        event Action<MouseButtonEventArgs> MouseDown;

        /// <inheritdoc cref="NativeWindow.MouseMove"/>
        event Action<MouseMoveEventArgs> MouseMove;

        /// <inheritdoc cref="NativeWindow.MouseUp"/>
        event Action<MouseButtonEventArgs> MouseUp;

        /// <inheritdoc cref="NativeWindow.MouseWheel"/>
        event Action<MouseWheelEventArgs> MouseWheel;

        /// <inheritdoc cref="NativeWindow.ClientSize"/>
        Vector2i ClientSize { get; }

        /// <summary>
        ///   Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        bool IsDisposed { get; }

        /// <inheritdoc cref="NativeWindow.IsExiting"/>
        bool IsExiting { get; }

        /// <inheritdoc cref="NativeWindow.IsFocused"/>
        bool IsFocused { get; }

        /// <inheritdoc cref="NativeWindow.IsVisible"/>
        bool IsVisible { get; set; }

        /// <inheritdoc cref="NativeWindow.MousePosition"/>
        Vector2 MousePosition { get; set; }

        /// <inheritdoc cref="NativeWindow.MouseState"/>
        IMouseStateInvoker MouseState { get; }

        /// <inheritdoc cref="NativeWindow.Size"/>
        Vector2i Size { get; set; }

        /// <inheritdoc cref="NativeWindow.Title"/>
        string Title { get; set; }

        /// <inheritdoc cref="NativeWindow.VSync"/>
        VSyncMode VSync { get; set; }

        /// <inheritdoc cref="NativeWindow.Close"/>
        void Close();

        /// <inheritdoc cref="NativeWindow.ProcessInputEvents"/>
        void ProcessInputEvents();

        /// <inheritdoc cref="NativeWindow.ProcessWindowEvents(bool)"/>
        void ProcessWindowEvents(bool waitForEvents);
    }
}
