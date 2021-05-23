// <copyright file="GameTimeInfo.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;

    /// <summary>
    ///   Represents a structure that contains game time information.
    /// </summary>
    /// <seealso cref="System.IEquatable{FinalEngine.Launching.GameTimeInfo}"/>
    public struct GameTimeInfo : IEquatable<GameTimeInfo>
    {
        /// <summary>
        ///   Gets the delta (time passed since the previous frame).
        /// </summary>
        /// <value>
        ///   The delta (time passed since the previous frame).
        /// </value>
        public double Delta { get; init; }

        /// <summary>
        ///   Gets the delta, as a float (time passed since the previous frame).
        /// </summary>
        /// <value>
        ///   The delta, as a float (time passed since the previous frame).
        /// </value>
        public float DeltaF
        {
            get { return (float)this.Delta; }
        }

        /// <summary>
        ///   Gets the frame rate (FPS).
        /// </summary>
        /// <value>
        ///   The frame rate (FPS).
        /// </value>
        public double FrameRate { get; init; }

        /// <summary>
        ///   Gets the frame rate (FPS), as a float.
        /// </summary>
        /// <value>
        ///   The frame rate (FPS), as a float.
        /// </value>
        public float FrameRateF
        {
            get { return (float)this.FrameRate; }
        }

        /// <summary>
        ///   Implements the operator !=.
        /// </summary>
        /// <param name="left">
        ///   The left operand.
        /// </param>
        /// <param name="right">
        ///   The right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator !=(GameTimeInfo left, GameTimeInfo right)
        {
            return !(left == right);
        }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name="left">
        ///   The left operand.
        /// </param>
        /// <param name="right">
        ///   The right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator ==(GameTimeInfo left, GameTimeInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///   Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">
        ///   The object to compare with the current instance.
        /// </param>
        /// <returns>
        ///   <see langword="true"/> if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            return obj is GameTimeInfo info && this.Equals(info);
        }

        /// <summary>
        ///   Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">
        ///   An object to compare with this object.
        /// </param>
        /// <returns>
        ///   <see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(GameTimeInfo other)
        {
            return this.Delta == other.Delta &&
                   this.FrameRate == other.FrameRate &&
                   this.DeltaF == other.DeltaF &&
                   this.FrameRateF == other.FrameRateF;
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            const int Accumulator = 17;

            return (this.Delta.GetHashCode() * Accumulator) +
                   (this.FrameRate.GetHashCode() * Accumulator) +
                   (this.DeltaF.GetHashCode() * Accumulator) +
                   (this.FrameRateF.GetHashCode() * Accumulator);
        }
    }
}