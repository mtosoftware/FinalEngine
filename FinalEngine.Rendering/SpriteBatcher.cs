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

    /// <summary>
    ///   Provides a standard implementation of an <see cref="ISpriteBatcher"/> that batches quads to be filled into a vertex buffer.
    /// </summary>
    /// <seealso cref="FinalEngine.Rendering.ISpriteBatcher"/>
    public class SpriteBatcher : ISpriteBatcher
    {
        /// <summary>
        ///   The input assembler, used to fill the contents of a vertex buffer with <see cref="vertices"/>.
        /// </summary>
        private readonly IInputAssembler inputAssembler;

        /// <summary>
        ///   The vertices that have been batched.
        /// </summary>
        private readonly IList<Vertex> vertices;

        /// <summary>
        ///   Initializes a new instance of the <see cref="SpriteBatcher"/> class.
        /// </summary>
        /// <param name="inputAssembler">
        ///   The input assembler, used to fill the contents of a vertex buffer with the batched vertices via <see cref="Update(IVertexBuffer)"/>.
        /// </param>
        /// <param name="maxCapacity">
        ///   The maximum capacity of quads that can be batched.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   iThe specified <paramref name="inputAssembler"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   The specified <paramref name="maxCapacity"/> parameter must be greater than zero.
        /// </exception>
        public SpriteBatcher(IInputAssembler inputAssembler, int maxCapacity = 10000)
        {
            this.inputAssembler = inputAssembler ?? throw new ArgumentNullException(nameof(inputAssembler), $"The specified {nameof(inputAssembler)} parameter cannot be null.");

            if (maxCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxCapacity), $"The specified {nameof(maxCapacity)} parameter must be greater than zero.");
            }

            this.MaxVertexCount = maxCapacity * 4;
            this.MaxIndexCount = maxCapacity * 6;

            this.vertices = new List<Vertex>(this.MaxVertexCount);
        }

        /// <summary>
        ///   Gets the current number of indices that are batched.
        /// </summary>
        /// <value>
        ///   The current number of indices that are batched.
        /// </value>
        public int CurrentIndexCount { get; private set; }

        /// <summary>
        ///   Gets the current number of vertices that are batched.
        /// </summary>
        /// <value>
        ///   The current number of vertices that are batched.
        /// </value>
        public int CurrentVertexCount { get; private set; }

        /// <summary>
        ///   Gets the maximum number of indices that can be batched.
        /// </summary>
        /// <value>
        ///   The maximum number of indices that can be batched.
        /// </value>
        public int MaxIndexCount { get; }

        /// <summary>
        ///   Gets the maximum number of vertices that can be batched.
        /// </summary>
        /// <value>
        ///   The maximum number of vertices that can be batched.
        /// </value>
        public int MaxVertexCount { get; }

        /// <summary>
        ///   Gets a value indicating whether the <see cref="Reset"/> method should be invoked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the method should be invoked; otherwise, <c>false</c>.
        /// </value>
        public bool ShouldReset
        {
            get { return this.CurrentVertexCount >= this.MaxVertexCount; }
        }

        /// <summary>
        ///   Constructs a set of vertices from the specified parameters and stores to updated via the <see cref="Update(IVertexBuffer)"/> method.
        /// </summary>
        /// <param name="textureSlotIndex">
        ///   The index of the texture slot.
        /// </param>
        /// <param name="color">
        ///   The color of the vertices.
        /// </param>
        /// <param name="origin">
        ///   The origin of the vertices.
        /// </param>
        /// <param name="position">
        ///   The absolute position of the vertices.
        /// </param>
        /// <param name="rotation">
        ///   The rotation of the vertices.
        /// </param>
        /// <param name="size">
        ///   The size of the vertices.
        /// </param>
        public void Batch(float textureSlotIndex, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 size)
        {
            float x = position.X;
            float y = position.Y;

            float dx = -origin.X;
            float dy = -origin.Y;

            float w = size.X;
            float h = size.Y;

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

        /// <summary>
        ///   Resets the sprite batcher by clearing all batched vertices from it's cache.
        /// </summary>
        public void Reset()
        {
            this.vertices.Clear();
            this.CurrentIndexCount = 0;
            this.CurrentVertexCount = 0;
        }

        /// <summary>
        ///   Updates the specified <paramref name="vertexBuffer"/> with the vertices that have been batched.
        /// </summary>
        /// <param name="vertexBuffer">
        ///   The vertex buffer to update the contents of.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="vertexBuffer"/> parameter cannot be null.
        /// </exception>
        public void Update(IVertexBuffer vertexBuffer)
        {
            if (vertexBuffer == null)
            {
                throw new ArgumentNullException(nameof(vertexBuffer), $"The specified {nameof(vertexBuffer)} parameter cannot be null.");
            }

            this.inputAssembler.UpdateVertexBuffer(vertexBuffer, (IReadOnlyCollection<Vertex>)this.vertices, Vertex.SizeInBytes);
        }
    }
}