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

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex : IEquatable<Vertex>
    {
        public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();

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

        public Vector2 Position { get; set; }

        public Vector4 Color { get; set; }

        public Vector2 TextureCoordinate { get; set; }

        public float TextureSlotIndex { get; set; }

        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }

        public bool Equals(Vertex other)
        {
            return this.Position == other.Position &&
                   this.Color == other.Color &&
                   this.TextureCoordinate == other.TextureCoordinate &&
                   this.TextureSlotIndex == other.TextureSlotIndex;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vertex vertex && this.Equals(vertex);
        }

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