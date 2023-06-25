// <copyright file="MeshVertex.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;

/// <summary>
///   Represents a mesh vertex.
/// </summary>
/// <seealso cref="IEquatable{MeshVertex}"/>
[StructLayout(LayoutKind.Sequential)]
public struct MeshVertex : IEquatable<MeshVertex>
{
    /// <summary>
    ///   The size in bytes of a <see cref="MeshVertex"/>.
    /// </summary>
    public static readonly int SizeInBytes = Marshal.SizeOf<MeshVertex>();

    /// <summary>
    ///   Gets the input elements required to create an <see cref="IInputLayout"/> for use with a <see cref="MeshVertex"/>.
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
                    new InputElement(0, 3, InputElementType.Float, 0),
                    new InputElement(1, 4, InputElementType.Float, 3 * sizeof(float)),
                    new InputElement(2, 2, InputElementType.Float, 7 * sizeof(float)),
                    new InputElement(3, 3, InputElementType.Float, 9 * sizeof(float)),
                    new InputElement(4, 3, InputElementType.Float, 12 * sizeof(float)),
            };
        }
    }

    /// <summary>
    ///   Gets or sets the position.
    /// </summary>
    /// <value>
    ///   The position.
    /// </value>
    public Vector3 Position { get; set; }

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
    ///   Gets or sets the normal.
    /// </summary>
    /// <value>
    ///   The normal.
    /// </value>
    public Vector3 Normal { get; set; }

    /// <summary>
    ///   Gets or sets the tangent.
    /// </summary>
    /// <value>
    ///   The tangent.
    /// </value>
    public Vector3 Tangent { get; set; }

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
    public static bool operator ==(MeshVertex left, MeshVertex right)
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
    public static bool operator !=(MeshVertex left, MeshVertex right)
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
    public bool Equals(MeshVertex other)
    {
        return this.Position == other.Position &&
               this.Color == other.Color &&
               this.TextureCoordinate == other.TextureCoordinate &&
               this.Normal == other.Normal &&
               this.Tangent == other.Tangent;
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
        return obj is MeshVertex vertex && this.Equals(vertex);
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

        return (this.Position.GetHashCode() * accumulator) +
               (this.Color.GetHashCode() * accumulator) +
               (this.TextureCoordinate.GetHashCode() * accumulator) +
               (this.Normal.GetHashCode() * accumulator) +
               (this.Tangent.GetHashCode() * accumulator);
    }
}
