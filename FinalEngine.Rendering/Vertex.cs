// <copyright file="Vertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Collections.Generic;
    using System.Numerics;
    using System.Runtime.InteropServices;
    using FinalEngine.Rendering.Buffers;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();

        public Vertex(Vector2 position, Vector4 color, Vector2 textureCoordinate, float textureSlotIndex)
        {
            this.Position = position;
            this.Color = color;
            this.TextureCoordinate = textureCoordinate;
            this.TextureSlotIndex = textureSlotIndex;
        }

        public Vector2 Position { get; set; }

        public Vector4 Color { get; set; }

        public Vector2 TextureCoordinate { get; set; }

        public float TextureSlotIndex { get; set; }

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
    }
}