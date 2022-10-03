// <copyright file="DepthStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;

    /// <summary>
    ///   Represents the depth state used in a call to <see cref="IOutputMerger.SetDepthState(DepthStateDescription)"/>.
    /// </summary>
    /// <seealso cref="IEquatable{DepthStateDescription}"/>
    public struct DepthStateDescription : IEquatable<DepthStateDescription>
    {
        /// <summary>
        ///   The depth comparison mode which determines how new values are compared with existing values in a depth operation.
        /// </summary>
        private ComparisonMode? comparisonMode;

        /// <summary>
        ///   Indicates whether writing to the depth buffer is enabled.
        /// </summary>
        private bool? writeEnabled;

        /// <summary>
        ///   Gets or sets a <see cref="comparisonMode"/> that represents the mode that determines how new values are compared with existing values in a depth operation.
        /// </summary>
        /// <value>
        ///   The mode that determines how new values are compared with existing values in a depth operation.
        /// </value>
        public ComparisonMode ComparisonMode
        {
            get { return this.comparisonMode ?? ComparisonMode.Less; }
            set { this.comparisonMode = value; }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether reading from the depth buffer is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reading from the depth buffer is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool ReadEnabled { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether writing to the depth buffer is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if writing to the depth buffer is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool WriteEnabled
        {
            get { return this.writeEnabled ?? true; }
            set { this.writeEnabled = value; }
        }

        /// <summary>
        ///   Implements the operator !=.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="DepthStateDescription"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="DepthStateDescription"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator !=(DepthStateDescription left, DepthStateDescription right)
        {
            return !(left == right);
        }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="DepthStateDescription"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="DepthStateDescription"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator ==(DepthStateDescription left, DepthStateDescription right)
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
        public bool Equals(DepthStateDescription other)
        {
            return this.ComparisonMode == other.ComparisonMode &&
                   this.ReadEnabled == other.ReadEnabled &&
                   this.WriteEnabled == other.WriteEnabled;
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
            return obj is DepthStateDescription description && this.Equals(description);
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

            return (this.ComparisonMode.GetHashCode() * Accumulator) +
                   (this.ReadEnabled.GetHashCode() * Accumulator) +
                   (this.WriteEnabled.GetHashCode() * Accumulator);
        }
    }
}