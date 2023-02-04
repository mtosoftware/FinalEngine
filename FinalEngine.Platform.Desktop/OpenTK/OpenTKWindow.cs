// <copyright file="OpenTKWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK;

using System;
using System.Drawing;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using global::OpenTK.Mathematics;
using global::OpenTK.Windowing.Common;

/// <summary>
///   Provides an OpenTK implementation of an <see cref="IWindow"/> and <see cref="IEventsProcessor"/>.
/// </summary>
/// <seealso cref="IWindow"/>
/// <seealso cref="IEventsProcessor"/>
public class OpenTKWindow : IWindow, IEventsProcessor
{
    /// <summary>
    ///   The native window.
    /// </summary>
    private readonly INativeWindowInvoker nativeWindow;

    /// <summary>
    ///   Initializes a new instance of the <see cref="OpenTKWindow"/> class.
    /// </summary>
    /// <param name="nativeWindow">
    ///   Specifies a <see cref="INativeWindowInvoker"/> that represents the underlying native window to be used.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="nativeWindow"/> parameter is null.
    /// </exception>
    public OpenTKWindow(INativeWindowInvoker nativeWindow)
    {
        this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow));

        this.nativeWindow.VSync = VSyncMode.Off;
    }

    /// <summary>
    ///   Finalizes an instance of the <see cref="OpenTKWindow"/> class.
    /// </summary>
    ~OpenTKWindow()
    {
        this.Dispose(false);
    }

    /// <inheritdoc/>
    public Size ClientSize
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            return new Size(this.nativeWindow.ClientSize.X, this.nativeWindow.ClientSize.Y);
        }
    }

    /// <inheritdoc/>
    public bool IsExiting
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            return this.nativeWindow.IsExiting;
        }
    }

    /// <inheritdoc/>
    public bool IsFocused
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            return this.nativeWindow.IsFocused;
        }
    }

    /// <inheritdoc/>
    public Size Size
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            return new Size(this.nativeWindow.Size.X, this.nativeWindow.Size.Y);
        }

        set
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            this.nativeWindow.Size = new Vector2i(value.Width, value.Height);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    ///   The underlying native window is disposed.
    /// </exception>
    public string Title
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            return this.nativeWindow.Title;
        }

        set
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            this.nativeWindow.Title = value;
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    ///   The underlying native window is disposed.
    /// </exception>
    public bool Visible
    {
        get
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            return this.nativeWindow.IsVisible;
        }

        set
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(OpenTKWindow));
            }

            this.nativeWindow.IsVisible = value;
        }
    }

    /// <summary>
    ///   Gets a value indicating whether this <see cref="OpenTKWindow"/> is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="OpenTKWindow"/> is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    ///   The underlying native window is disposed.
    /// </exception>
    public void Close()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenTKWindow));
        }

        this.nativeWindow.Close();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    /// <remarks>
    ///   This method is not thread safe and should only be executed on the main thread.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">
    ///   The underlying native window is disposed.
    /// </exception>
    public void ProcessEvents()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenTKWindow));
        }

        this.nativeWindow.ProcessWindowEvents(false);
        this.nativeWindow.ProcessInputEvents();
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing && !this.nativeWindow.IsDisposed)
        {
            this.nativeWindow.Dispose();
        }

        this.IsDisposed = true;
    }
}
