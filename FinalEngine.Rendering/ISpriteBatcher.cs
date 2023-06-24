// <copyright file="ISpriteBatcher.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Buffers;

/// <summary>
///   Defines an interface that provides batching functionality for two-dimensional graphics.
/// </summary>
public interface ISpriteBatcher
{
    /// <summary>
    ///   Gets the current number of indices that are batched.
    /// </summary>
    /// <value>
    ///   The current number of indices that are batched.
    /// </value>
    int CurrentIndexCount { get; }

    /// <summary>
    ///   Gets the current number of vertices that are batched.
    /// </summary>
    /// <value>
    ///   The current number of vertices that are batched.
    /// </value>
    int CurrentVertexCount { get; }

    /// <summary>
    ///   Gets the maximum number of indices that can be batched.
    /// </summary>
    /// <value>
    ///   The maximum number of indices that can be batched.
    /// </value>
    int MaxIndexCount { get; }

    /// <summary>
    ///   Gets the maximum number of vertices that can be batched.
    /// </summary>
    /// <value>
    ///   The maximum number of vertices that can be batched.
    /// </value>
    int MaxVertexCount { get; }

    /// <summary>
    ///   Gets a value indicating whether the <see cref="Reset"/> method should be invoked.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the method should be invoked; otherwise, <c>false</c>.
    /// </value>
    bool ShouldReset { get; }

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
    /// <param name="scale">
    ///   The size of the vertices.
    /// </param>
    /// <param name="textureWidth">
    /// The width of the texture (in pixels).
    /// </param>
    /// <param name="textureHeight">
    /// The height of the texture (in pixels).
    /// </param>
    void Batch(float textureSlotIndex, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale, int textureWidth, int textureHeight);

    /// <summary>
    ///   Resets the sprite batcher by clearing all batched vertices from it's cache.
    /// </summary>
    void Reset();

    /// <summary>
    ///   Updates the specified <paramref name="vertexBuffer"/> with the vertices that have been batched.
    /// </summary>
    /// <param name="vertexBuffer">
    ///   The vertex buffer to update the contents of.
    /// </param>
    void Update(IVertexBuffer vertexBuffer);
}
