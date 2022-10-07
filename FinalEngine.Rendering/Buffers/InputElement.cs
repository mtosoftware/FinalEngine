// <copyright file="InputElement.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///   Enumerates the available data types for an <see cref="InputElement"/>.
    /// </summary>
    [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Required by API")]
    public enum InputElementType
    {
        /// <summary>
        ///   The data type is a 32-bit signed integer - or <see cref="int"/>.
        /// </summary>
        Int,

        /// <summary>
        ///   The data type is a 8-bit signed integer - or <see cref="sbyte"/>.
        /// </summary>
        Byte,

        /// <summary>
        ///   The data type is a 16-bit signed integer - or <see cref="short"/>.
        /// </summary>
        Short,

        /// <summary>
        ///   The data type is a 32-bit floating point value (IEEE-754).
        /// </summary>
        Float,

        /// <summary>
        ///   The data type is a 64-bit floating point value (IEEE-754).
        /// </summary>
        Double,
    }

    /// <summary>
    ///   Represents the formating of an individual vertex element.
    /// </summary>
    /// <seealso cref="IEquatable{InputElement}"/>
    public struct InputElement : IEquatable<InputElement>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="InputElement"/> struct.
        /// </summary>
        /// <param name="index">
        ///   Specifies a <see cref="int"/> that represents the index of this <see cref="InputElement"/>.
        /// </param>
        /// <param name="size">
        ///   Specifies a <see cref="int"/> that represents the total number of individual components in this <see cref="InputElement"/>.
        /// </param>
        /// <param name="type">
        ///   Specifies a <see cref="InputElementType"/> represents the data type of each individual component in this <see cref="InputElement"/>.
        /// </param>
        /// <param name="relativeOffset">
        ///   Specifies a <see cref="int"/> that represents the offset in bytes from the beginning of the vertex.
        /// </param>
        public InputElement(int index, int size, InputElementType type, int relativeOffset)
        {
            this.Index = index;
            this.Size = size;
            this.Type = type;
            this.RelativeOffset = relativeOffset;
        }

        /// <summary>
        ///   Gets or sets an <see cref="int"/> that represents the index of this <see cref="InputElement"/>.
        /// </summary>
        /// <value>
        ///   The index of this <see cref="InputElement"/>.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        ///   Gets or sets an <see cref="int"/> that represents the relative offset (starting from zero) of this <see cref="InputElement"/>.
        /// </summary>
        /// <value>
        ///   The relative offset (starting from zero) of this <see cref="InputElement"/>.
        /// </value>
        /// <remarks>
        ///   The relative offset defines the offset in bytes from the beginning of the vertex.
        /// </remarks>
        public int RelativeOffset { get; set; }

        /// <summary>
        ///   Gets or sets an <see cref="int"/> that represents the total number of individual components in this <see cref="InputElement"/>.
        /// </summary>
        /// <value>
        ///   The total number of individual components in this <see cref="InputElement"/>.
        /// </value>
        public int Size { get; set; }

        /// <summary>
        ///   Gets or sets an <see cref="InputElementType"/> that represents the data type of each individual component in this <see cref="InputElement"/>.
        /// </summary>
        /// <value>
        ///   The data type of each individual component in this <see cref="InputElement"/>.
        /// </value>
        public InputElementType Type { get; set; }

        /// <summary>
        ///   Implements the operator !=.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="InputElement"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="InputElement"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator !=(InputElement left, InputElement right)
        {
            return !(left == right);
        }

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name="left">
        ///   Specifies a <see cref="InputElement"/> that represents the left operand.
        /// </param>
        /// <param name="right">
        ///   Specifies a <see cref="InputElement"/> that represents the right operand.
        /// </param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator ==(InputElement left, InputElement right)
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
        public bool Equals(InputElement other)
        {
            return this.Index == other.Index &&
                   this.RelativeOffset == other.RelativeOffset &&
                   this.Size == other.Size &&
                   this.Type == other.Type;
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
            return obj is InputElement description && this.Equals(description);
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

            return (this.Index.GetHashCode() * accumulator) +
                   (this.RelativeOffset.GetHashCode() * accumulator) +
                   (this.Size.GetHashCode() * accumulator) +
                   (this.Type.GetHashCode() * accumulator);
        }
    }
}
