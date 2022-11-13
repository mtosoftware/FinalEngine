// <copyright file="RenderQualitySettings.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Settings
{
    using System;

    public enum AntiAliasing
    {
        None,

        TwoTimesMultiSampling,

        FourTimesMultiSampling,

        EightTimesMultiSampling,
    }

    public struct RenderQualitySettings : IEquatable<RenderQualitySettings>
    {
        private AntiAliasing? antiAliasing;

        private bool? multiSamplingEnabled;

        public AntiAliasing AntiAliasing
        {
            get { return this.antiAliasing ?? AntiAliasing.FourTimesMultiSampling; }
            set { this.antiAliasing = value; }
        }

        public int AntiAliasingSamples
        {
            get
            {
                return this.AntiAliasing switch
                {
                    AntiAliasing.None => 0,
                    AntiAliasing.TwoTimesMultiSampling => 2,
                    AntiAliasing.FourTimesMultiSampling => 4,
                    AntiAliasing.EightTimesMultiSampling => 8,
                    _ => 4,
                };
            }
        }

        public bool MultiSamplingEnabled
        {
            get { return this.multiSamplingEnabled ?? true; }
            set { this.multiSamplingEnabled = value; }
        }

        /// <summary>
        ///   Implements the operator !=.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="RenderQualitySettings"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="RenderQualitySettings"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator !=(RenderQualitySettings left, RenderQualitySettings right)
        {
            return !(left == right);
        }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="RenderQualitySettings"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="RenderQualitySettings"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator ==(RenderQualitySettings left, RenderQualitySettings right)
        {
            return left.Equals(right);
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
        public bool Equals(RenderQualitySettings other)
        {
            return this.MultiSamplingEnabled == other.MultiSamplingEnabled &&
                   this.AntiAliasing == other.AntiAliasing;
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
            return obj is RenderQualitySettings settings && this.Equals(settings);
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///   A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            const int accumulator = 17;

            return (this.MultiSamplingEnabled.GetHashCode() * accumulator) +
                   (this.AntiAliasing.GetHashCode() * accumulator);
        }
    }
}
