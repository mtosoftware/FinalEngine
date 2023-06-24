// <copyright file="GameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using System.Diagnostics;
using FinalEngine.Runtime.Invocation;

/// <summary>
///   Provides a standard implementation of an <see cref="IGameTime"/> using an <see cref="IStopwatchInvoker"/> to keep track of time.
/// </summary>
/// <seealso cref="IGameTime"/>
public class GameTime : IGameTime
{
    /// <summary>
    ///   One second, represented in milliseconds.
    /// </summary>
    private const double OneSecondAsMilliSeconds = 1000.0d;

    /// <summary>
    ///   The amount of time required to wait before a frame can processed, in milliseconds.
    /// </summary>
    private readonly double waitTime;

    /// <summary>
    ///   The watch invoker, used to keep track of time throughout the game.
    /// </summary>
    private readonly IStopwatchInvoker watch;

    /// <summary>
    ///   The last time a frame has been processed, in milliseconds.
    /// </summary>
    private double lastTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameTime"/> class.
    /// </summary>
    /// <param name="frameCap">
    /// The frame cap.
    /// </param>
    public GameTime(double frameCap)
        : this(new StopwatchInvoker(new Stopwatch()), frameCap)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="GameTime"/> class.
    /// </summary>
    /// <param name="watch">
    ///   The watch, used to keep track of time throughout the game.
    /// </param>
    /// <param name="frameCap">
    ///   The frame cap (frame rate), in milliseconds.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="watch"/> parameter cannot be null.
    /// </exception>
    /// <exception cref="DivideByZeroException">
    ///   The specified <paramref name="frameCap"/> parameter must be greater than zero.
    /// </exception>
    public GameTime(IStopwatchInvoker watch, double frameCap)
    {
        this.watch = watch ?? throw new ArgumentNullException(nameof(watch));

        if (frameCap <= 0.0d)
        {
            throw new DivideByZeroException($"The specified {nameof(frameCap)} parameter must be greater than zero.");
        }

        this.waitTime = OneSecondAsMilliSeconds / frameCap;
    }

    /// <summary>
    ///   Gets the delta (time that's passed since the previous frame).
    /// </summary>
    /// <value>
    ///   The delta (time that's passed since the previous frame).
    /// </value>
    public static float Delta { get; private set; }

    /// <summary>
    ///   Gets the frame rate (or FPS).
    /// </summary>
    /// <value>
    ///   The frame rate (or FPS).
    /// </value>
    public static float FrameRate { get; private set; }

    /// <summary>
    ///   Determines whether the next frame can be processed and rendered.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance can process the next frame; otherwise, <c>false</c>.
    /// </returns>
    public bool CanProcessNextFrame()
    {
        if (!this.watch.IsRunning)
        {
            this.watch.Restart();
        }

        double currentTime = this.watch.Elapsed.TotalMilliseconds;

        if (currentTime >= this.lastTime + this.waitTime)
        {
            Delta = (float)(currentTime - this.lastTime);
            FrameRate = (float)Math.Round(OneSecondAsMilliSeconds / Delta);

            this.lastTime = currentTime;

            return true;
        }

        return false;
    }
}
