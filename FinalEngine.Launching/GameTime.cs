// <copyright file="GameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;
    using System.Diagnostics;

    public class GameTime : IGameTime
    {
        private const double Second = 1000.0d;

        private readonly double waitTime;

        private readonly Stopwatch watch;

        private double lastTime;

        public GameTime(double frameCap)
        {
            if (frameCap <= 0.0d)
            {
                throw new DivideByZeroException($"The specified {nameof(frameCap)} parameter must be greater than zero.");
            }

            this.waitTime = Second / frameCap;
            this.watch = new Stopwatch();
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