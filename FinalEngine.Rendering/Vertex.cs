// <copyright file="Vertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Runtime.InteropServices;
    using FinalEngine.Rendering.Buffers;

    /// <summary>
    ///   Represents a vertex, generally used alongside with <see cref="SpriteBatcher"/>.
    /// </summary>
    /// <seealso cref="IEquatable{Vertex}"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex : IEquatable<Vertex>
    {
        /// <summary>
        ///   The size in bytes of a <see cref="Vertex"/>.
        /// </summary>
        public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();

        /// <summary>
        ///   Gets the input elements required to create an <see cref="IInputLayout"/> for use with a <see cref="Vertex"/>.
        /// </summary>
        /// <value>
        ///   The input elements.
        /// </value>
        public static IReadOnlyCollection<InputElement> InputElements
        {
            get
            {
                return new InputElement[]
                {
                    new (0, 2, InputElementType.Float, 0),
                    new (1, 4, InputElementType.Float, 2 * sizeof(float)),
                    new (2, 2, InputElementType.Float, 6 * sizeof(float)),
                    new (3, 1, InputElementType.Float, 8 * sizeof(float)),
                };
            }
        }

        /// <summary>
        ///   Gets or sets the position.
        /// </summary>
        /// <value>
        ///   The position.
        /// </value>
        public Vector2 Position { get; set; }

        /// <summary>
        ///   Gets or sets the color.
        /// </summary>
        /// <value>
        ///   The color.
        /// </value>
        public Vector4 Color { get; set; }

        /// <summary>
        ///   Gets or sets the texture coordinate.
        /// </summary>
        /// <value>
        ///   The texture coordinate.
        /// </value>
        public Vector2 TextureCoordinate { get; set; }

        /// <summary>
        ///   Gets or sets the index of the texture slot.
        /// </summary>
        /// <value>
        ///   The index of the texture slot.
        /// </value>
        public float TextureSlotIndex { get; set; }

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
        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Equals(right);
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
        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
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
        public bool Equals(Vertex other)
        {
            return this.Position == other.Position &&
                   this.Color == other.Color &&
                   this.TextureCoordinate == other.TextureCoordinate &&
                   this.TextureSlotIndex == other.TextureSlotIndex;
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
            return obj is Vertex vertex && this.Equals(vertex);
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

            return (this.Position.GetHashCode() * Accumulator) +
                   (this.Color.GetHashCode() * Accumulator) +
                   (this.TextureCoordinate.GetHashCode() * Accumulator) +
                   (this.TextureSlotIndex.GetHashCode() * Accumulator);
        }
    }
}