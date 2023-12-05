// <copyright file="NativeWindowInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation;

using System.Diagnostics.CodeAnalysis;
using global::OpenTK.Windowing.Desktop;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
public sealed class NativeWindowInvoker : NativeWindow, INativeWindowInvoker
{
    public NativeWindowInvoker(NativeWindowSettings settings)
        : base(settings)
    {
    }

    public bool IsDisposed { get; private set; }

    IMouseStateInvoker INativeWindowInvoker.MouseState
    {
        get { return new MouseStateInvoker(this.MouseState); }
    }

    public new void ProcessWindowEvents(bool waitForEvents)
    {
        NativeWindow.ProcessWindowEvents(waitForEvents);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        this.IsDisposed = true;
    }
}
