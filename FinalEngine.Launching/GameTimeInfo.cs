// <copyright file="GameTimeInfo.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;

    public struct GameTimeInfo : IEquatable<GameTimeInfo>
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

        public override bool Equals(object? obj)
        {
            return obj is GameTimeInfo info && this.Equals(info);
        }

        public bool Equals(GameTimeInfo other)
        {
            return this.Delta == other.Delta &&
                   this.FrameRate == other.FrameRate &&
                   this.DeltaF == other.DeltaF &&
                   this.FrameRateF == other.FrameRateF;
        }

        public override int GetHashCode()
        {
            const int Accumulator = 17;

            return (this.Delta.GetHashCode() * Accumulator) +
                   (this.FrameRate.GetHashCode() * Accumulator) +
                   (this.DeltaF.GetHashCode() * Accumulator) +
                   (this.FrameRateF.GetHashCode() * Accumulator);
        }

        public override string ToString()
        {
            return $"({this.Delta} : {this.FrameRate})";
        }
    }
}