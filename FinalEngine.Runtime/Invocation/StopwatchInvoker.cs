// <copyright file="StopwatchInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Invocation;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///   Provides a standard implementation of an <see cref="IStopwatchInvoker"/>.
/// </summary>
/// <seealso cref="IStopwatchInvoker"/>
[ExcludeFromCodeCoverage]
public class StopwatchInvoker : IStopwatchInvoker
{
    /// <summary>
    ///   The stopwatch.
    /// </summary>
    private readonly Stopwatch watch;

    /// <summary>
    /// Initializes a new instance of the <see cref="StopwatchInvoker"/> class.
    /// </summary>
    public StopwatchInvoker()
        : this(new Stopwatch())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="StopwatchInvoker"/> class.
    /// </summary>
    /// <param name="watch">
    ///   The stopwatch to invoke.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="watch"/> parameter cannot be null.
    /// </exception>
    public StopwatchInvoker(Stopwatch watch)
    {
        this.watch = watch ?? throw new ArgumentNullException(nameof(watch));
    }

    /// <inheritdoc/>
    public TimeSpan Elapsed
    {
        get { return this.watch.Elapsed; }
    }

    /// <inheritdoc/>
    public bool IsRunning
    {
        get { return this.watch.IsRunning; }
    }

    /// <inheritdoc/>
    public void Restart()
    {
        this.watch.Restart();
    }
}
