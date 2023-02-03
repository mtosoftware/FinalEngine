// <copyright file="NativeWindowInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation;

using System.Diagnostics.CodeAnalysis;
using global::OpenTK.Windowing.Desktop;

/// <summary>
///   Provides an implementation of an <see cref="INativeWindowInvoker"/>.
/// </summary>
/// <seealso cref="INativeWindowInvoker"/>
[ExcludeFromCodeCoverage]
public sealed class NativeWindowInvoker : NativeWindow, INativeWindowInvoker
{
    /// <inheritdoc cref="NativeWindow(NativeWindowSettings)"/>
    public NativeWindowInvoker(NativeWindowSettings settings)
        : base(settings)
    {
    }

    /// <summary>
    ///   Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    /// </value>
    public bool IsDisposed { get; private set; }

    /// <inheritdoc/>
    IMouseStateInvoker INativeWindowInvoker.MouseState
    {
        get { return new MouseStateInvoker(this.MouseState); }
    }

    /// <inheritdoc/>
    public new void ProcessWindowEvents(bool waitForEvents)
    {
        NativeWindow.ProcessWindowEvents(waitForEvents);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        this.IsDisposed = true;
    }
}
