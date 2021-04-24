// <copyright file="SpriteBatcher.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Rendering.Buffers;

    public class SpriteBatcher : ISpriteBatcher
    {
        private readonly IInputAssembler inputAssembler;

        private readonly IList<Vertex> vertices;

        public SpriteBatcher(IInputAssembler inputAssembler, int maxCapacity)
        {
            this.inputAssembler = inputAssembler ?? throw new ArgumentNullException(nameof(inputAssembler), $"The specified {nameof(inputAssembler)} parameter cannot be null.");

            if (maxCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCapacity), $"The specified {nameof(maxCapacity)} parameter must be greater than zero.");
            }

            this.MaxVertexCount = maxCapacity * 4;
            this.MaxIndexCount = maxCapacity * 6;

            this.vertices = new List<Vertex>();
        }

        public int CurrentIndexCount { get; private set; }

        public int CurrentVertexCount { get; private set; }

        public int MaxIndexCount { get; }

        public int MaxVertexCount { get; }

        public bool ShouldReset
        {
            get { return this.vertices.Count >= this.MaxVertexCount; }
        }

        public void Batch(float textureSlotIndex, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale)
        {
            float x = position.X;
            float y = position.Y;

            float dx = -origin.X;
            float dy = -origin.Y;

            float w = scale.X;
            float h = scale.Y;

            float cos = (float)Math.Cos(rotation);
            float sin = (float)Math.Sin(rotation);

            var vecColor = new Vector4(
                color.R / 255.0f,
                color.G / 255.0f,
                color.B / 255.0f,
                color.A / 255.0f);

            // Top right
            this.vertices.Add(new Vertex()
            {
                Position = new Vector2(x + ((dx + w) * cos) - (dy * sin), y + ((dx + w) * sin) + (dy * cos)),
                Color = vecColor,
                TextureCoordinate = new Vector2(1, 1),
                TextureSlotIndex = textureSlotIndex,
            });

            // Top left
            this.vertices.Add(new Vertex()
            {
                Position = new Vector2(x + (dx * cos) - (dy * sin), y + (dx * sin) + (dy * cos)),
                Color = vecColor,
                TextureCoordinate = new Vector2(0, 1),
                TextureSlotIndex = textureSlotIndex,
            });

            // Bottom left
            this.vertices.Add(new Vertex()
            {
                Position = new Vector2(x + (dx * cos) - ((dy + h) * sin), y + (dx * sin) + ((dy + h) * cos)),
                Color = vecColor,
                TextureCoordinate = new Vector2(0, 0),
                TextureSlotIndex = textureSlotIndex,
            });

            // Bottom right
            this.vertices.Add(new Vertex()
            {
                Position = new Vector2(x + ((dx + w) * cos) - ((dy + h) * sin), y + ((dx + w) * sin) + ((dy + h) * cos)),
                Color = vecColor,
                TextureCoordinate = new Vector2(1, 0),
                TextureSlotIndex = textureSlotIndex,
            });

            this.CurrentIndexCount += 6;
            this.CurrentVertexCount += 4;
        }

        public void ProcessBatch(IVertexBuffer vertexBuffer)
        {
            if (vertexBuffer == null)
            {
                throw new ArgumentNullException(nameof(vertexBuffer), $"The specified {nameof(vertexBuffer)} parameter cannot be null.");
            }

            this.inputAssembler.UpdateVertexBuffer(vertexBuffer, (IReadOnlyCollection<Vertex>)this.vertices, Vertex.SizeInBytes);
        }

        public void Reset()
        {
            this.vertices.Clear();
        }
    }
}