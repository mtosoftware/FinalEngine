// <copyright file="GameTimeInfo.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    public struct GameTimeInfo
    {
        public double Delta { get; init; }

        public float DeltaF
        {
            get { return (float)this.Delta; }
        }

        public double FrameRate { get; init; }

        public float FrameRateF
        {
            get { return (float)this.FrameRate; }
        }
    }
}