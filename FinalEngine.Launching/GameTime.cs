// <copyright file="GameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Launching.Invocation;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IGameTime"/> using an <see cref="IStopwatchInvoker"/> to keep track of time.
    /// </summary>
    /// <seealso cref="FinalEngine.Launching.IGameTime"/>
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
            this.watch = watch ?? throw new ArgumentNullException(nameof(watch), $"The specified {nameof(watch)} parameter cannot be null.");

            if (frameCap <= 0.0d)
            {
                throw new DivideByZeroException($"The specified {nameof(frameCap)} parameter must be greater than zero.");
            }

            this.waitTime = OneSecondAsMilliSeconds / frameCap;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="GameTime"/> class.
        /// </summary>
        /// <param name="frameCap">
        ///   The frame cap (frame rate).
        /// </param>
        [ExcludeFromCodeCoverage]
        public GameTime(double frameCap)
            : this(new StopwatchInvoker(new Stopwatch()), frameCap)
        {
        }

        /// <summary>
        ///   Determines whether the next frame can be processed and rendered.
        /// </summary>
        /// <param name="info">
        ///   The information for the previous frame.
        /// </param>
        /// <returns>
        ///   <c>true</c> if this instance can process the next frame; otherwise, <c>false</c>.
        /// </returns>
        public bool CanProcessNextFrame(out GameTimeInfo info)
        {
            if (!this.watch.IsRunning)
            {
                this.watch.Restart();
            }

            double currentTime = this.watch.Elapsed.TotalMilliseconds;

            if (currentTime >= this.lastTime + this.waitTime)
            {
                double delta = currentTime - this.lastTime;
                double frameRate = Math.Round(OneSecondAsMilliSeconds / delta);

                info = new GameTimeInfo()
                {
                    Delta = delta,
                    FrameRate = frameRate,
                };

                this.lastTime = currentTime;

                return true;
            }

            info = default;
            return false;
        }
    }
}