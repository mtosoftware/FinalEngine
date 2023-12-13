// <copyright file="MeshVertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;

[StructLayout(LayoutKind.Sequential)]
public struct MeshVertex : IEquatable<MeshVertex>
{
    public static readonly int SizeInBytes = Marshal.SizeOf<MeshVertex>();

    public static IReadOnlyCollection<InputElement> InputElements
    {
        get
        {
            return new InputElement[]
            {
                new (0, 3, InputElementType.Float, 0),
                new (1, 4, InputElementType.Float, 3 * sizeof(float)),
                new (2, 2, InputElementType.Float, 7 * sizeof(float)),
                new (3, 3, InputElementType.Float, 9 * sizeof(float)),
                new (4, 3, InputElementType.Float, 12 * sizeof(float)),
            };
        }
    }

    public Vector3 Position { get; set; }

    public Vector4 Color { get; set; }

    public Vector2 TextureCoordinate { get; set; }

    public Vector3 Normal { get; set; }

    public Vector3 Tangent { get; set; }

    public static bool operator ==(MeshVertex left, MeshVertex right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MeshVertex left, MeshVertex right)
    {
        return !(left == right);
    }

    public readonly bool Equals(MeshVertex other)
    {
        return this.Position == other.Position &&
               this.Color == other.Color &&
               this.TextureCoordinate == other.TextureCoordinate &&
               this.Normal == other.Normal &&
               this.Tangent == other.Tangent;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is MeshVertex vertex && this.Equals(vertex);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return this.Position.GetHashCode() * accumulator +
               this.Color.GetHashCode() * accumulator +
               this.TextureCoordinate.GetHashCode() * accumulator +
               this.Normal.GetHashCode() * accumulator +
               this.Tangent.GetHashCode() * accumulator;
    }
}
