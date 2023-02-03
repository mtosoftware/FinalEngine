// <copyright file="MouseStateInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation;

using System;
using System.Diagnostics.CodeAnalysis;
using global::OpenTK.Mathematics;
using global::OpenTK.Windowing.GraphicsLibraryFramework;

/// <summary>
///   Provides an implementation of an <see cref="IMouseStateInvoker"/>.
/// </summary>
/// <seealso cref="IMouseStateInvoker"/>
[ExcludeFromCodeCoverage(Justification = "Invocation Class")]
public class MouseStateInvoker : IMouseStateInvoker
{
    /// <summary>
    ///   The mouse state.
    /// </summary>
    private readonly MouseState state;

    /// <summary>
    ///   Initializes a new instance of the <see cref="MouseStateInvoker"/> class.
    /// </summary>
    /// <param name="state">
    ///   The state to invoke.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="state"/> parameter cannot be null.
    /// </exception>
    public MouseStateInvoker(MouseState state)
    {
        this.state = state ?? throw new ArgumentNullException(nameof(state));
    }

    /// <inheritdoc/>
    public Vector2 Delta
    {
        get { return this.state.Delta; }
    }
}
