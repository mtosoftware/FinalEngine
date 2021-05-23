// <copyright file="GameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Launching.Invocation;

    public class GameTime : IGameTime
    {
        private const double Second = 1000.0d;

        private readonly double waitTime;

        private readonly IStopwatchInvoker watch;

        private double lastTime;

        public GameTime(IStopwatchInvoker watch, double frameCap)
        {
            this.watch = watch ?? throw new ArgumentNullException(nameof(watch), $"The specified {nameof(watch)} parameter cannot be null.");

            if (frameCap <= 0.0d)
            {
                throw new DivideByZeroException($"The specified {nameof(frameCap)} parameter must be greater than zero.");
            }

            this.waitTime = Second / frameCap;
        }

        [ExcludeFromCodeCoverage]
        public GameTime(double frameCap)
            : this(new StopwatchInvoker(new Stopwatch()), frameCap)
        {
        }

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
                double frameRate = Math.Round(Second / delta);

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