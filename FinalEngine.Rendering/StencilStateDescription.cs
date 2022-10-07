// <copyright file="StencilStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;

    /// <summary>
    ///   Enumerates the available stencil operations that identify an action taken on samples that pass or fail a stencil test.
    /// </summary>
    public enum StencilOperation
    {
        /// <summary>
        ///   Specifies the value is kept.
        /// </summary>
        Keep,

        /// <summary>
        ///   Specifies the value is set to zero.
        /// </summary>
        Zero,

        /// <summary>
        ///   Specifies the existing value is replaced with <see cref="StencilStateDescription.ReferenceValue"/>.
        /// </summary>
        Replace,

        /// <summary>
        ///   Specifies the existing value is incremented and clamped to the maximum representable unsigned value.
        /// </summary>
        Increment,

        /// <summary>
        ///   Specifies the existing value is incremented wrapped to zero when it exceeds the maximum representable unsigned value.
        /// </summary>
        IncrementWrap,

        /// <summary>
        ///   Specifies the existing value is decremented and clamped to zero.
        /// </summary>
        Decrement,

        /// <summary>
        ///   Specifies the existing value is decremented and wrapped to the maximum representable unsigned value if it would be reduced below zero.
        /// </summary>
        DecrementWrap,

        /// <summary>
        ///   Specifies bitwise-inversion of the existing value.
        /// </summary>
        Invert,
    }

    /// <summary>
    ///   Represents the stencil state used in a call to <see cref="IOutputMerger.SetStencilState(StencilStateDescription)"/>.
    /// </summary>
    /// <seealso cref="IEquatable{StencilStateDescription}"/>
    public struct StencilStateDescription : IEquatable<StencilStateDescription>
    {
        /// <summary>
        ///   The stencil comparison mode which determines how new values are compared with existing values in a stencil operation.
        /// </summary>
        private ComparisonMode? comparisonMode;

        /// <summary>
        ///   The operation performed on samples that pass the stencil test but fail the depth test.
        /// </summary>
        private StencilOperation? depthFail;

        /// <summary>
        ///   The operation performed on samples that pass the stencil test.
        /// </summary>
        private StencilOperation? pass;

        /// <summary>
        ///   Controls the portion of the stencil buffer used for reading.
        /// </summary>
        private int? readMask;

        /// <summary>
        ///   The operation performed on samples that fail the stencil test.
        /// </summary>
        private StencilOperation? stencilFail;

        /// <summary>
        ///   Controls the portion of the stencil buffer used for writing.
        /// </summary>
        private int? writeMask;

        /// <summary>
        ///   Gets or sets the stencil comparison mode which determines how new values are compared with existing values in a stencil operation.
        /// </summary>
        /// <value>
        ///   The stencil comparison mode which determines how new values are compared with existing values in a stencil operation.
        /// </value>
        public ComparisonMode ComparisonMode
        {
            get { return this.comparisonMode ?? ComparisonMode.Always; }
            set { this.comparisonMode = value; }
        }

        /// <summary>
        ///   Gets or sets the operation performed on samples that pass the stencil test but fail the depth test.
        /// </summary>
        /// <value>
        ///   The operation performed on samples that pass the stencil test but fail the depth test.
        /// </value>
        public StencilOperation DepthFail
        {
            get { return this.depthFail ?? StencilOperation.Keep; }
            set { this.depthFail = value; }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether stencil testing is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        ///   Gets or sets the operation performed on samples that pass the stencil test.
        /// </summary>
        /// <value>
        ///   The operation performed on samples that pass the stencil test.
        /// </value>
        public StencilOperation Pass
        {
            get { return this.pass ?? StencilOperation.Keep; }
            set { this.pass = value; }
        }

        /// <summary>
        ///   Gets or sets the mask that controls the portion of the stencil buffer used for reading.
        /// </summary>
        /// <value>
        ///   The mask that controls the portion of the stencil buffer used for reading.
        /// </value>
        public int ReadMask
        {
            get { return this.readMask ?? 0; }
            set { this.readMask = value; }
        }

        /// <summary>
        ///   Gets or sets the reference value to use when doing a stencil test.
        /// </summary>
        /// <value>
        ///   The reference value used when doing a stencil test.
        /// </value>
        public int ReferenceValue { get; set; }

        /// <summary>
        ///   Gets or sets the operation performed on samples that fail the stencil test.
        /// </summary>
        /// <value>
        ///   The operation performed on samples that fail the stencil test.
        /// </value>
        public StencilOperation StencilFail
        {
            get { return this.stencilFail ?? StencilOperation.Keep; }
            set { this.stencilFail = value; }
        }

        /// <summary>
        ///   Gets or sets the mask that controls the portion of the stencil buffer used for writing.
        /// </summary>
        /// <value>
        ///   The mask that controls the portion of the stencil buffer used for writing.
        /// </value>
        public int WriteMask
        {
            get { return this.writeMask ?? -1; }
            set { this.writeMask = value; }
        }

        /// <summary>
        ///   Implements the operator !=.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="StencilStateDescription"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="StencilStateDescription"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator !=(StencilStateDescription left, StencilStateDescription right)
        {
            return !(left == right);
        }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="StencilStateDescription"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="StencilStateDescription"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator ==(StencilStateDescription left, StencilStateDescription right)
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
        public bool Equals(StencilStateDescription other)
        {
            return this.ComparisonMode == other.comparisonMode &&
                   this.DepthFail == other.DepthFail &&
                   this.Enabled == other.Enabled &&
                   this.Pass == other.pass &&
                   this.ReadMask == other.ReadMask &&
                   this.ReferenceValue == other.ReferenceValue &&
                   this.StencilFail == other.StencilFail &&
                   this.WriteMask == other.WriteMask;
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
            return obj is StencilStateDescription description && this.Equals(description);
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

            return (this.ComparisonMode.GetHashCode() * accumulator) +
                   (this.DepthFail.GetHashCode() * accumulator) +
                   (this.Enabled.GetHashCode() * accumulator) +
                   (this.Pass.GetHashCode() * accumulator) +
                   (this.ReadMask.GetHashCode() * accumulator) +
                   (this.ReferenceValue.GetHashCode() * accumulator) +
                   (this.StencilFail.GetHashCode() * accumulator) +
                   (this.WriteMask.GetHashCode() * accumulator);
        }
    }
}
