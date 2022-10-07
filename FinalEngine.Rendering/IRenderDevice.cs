// <copyright file="IRenderDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Drawing;

    /// <summary>
    ///   Enumerates the available topologies that determine how vertex data interpreted by the rasterizer and rendered to the screen.
    /// </summary>
    /// <seealso cref="IRenderDevice.DrawIndices(PrimitiveTopology, int, int)"/>
    public enum PrimitiveTopology
    {
        /// <summary>
        ///   The vertex data is interpreted as a list of lines.
        /// </summary>
        Line,

        /// <summary>
        ///   The vertex data is interpreted as a series of connected lines.
        /// </summary>
        LineStrip,

        /// <summary>
        ///   The vertex data is interpreted as a list of triangles.
        /// </summary>
        Triangle,

        /// <summary>
        ///   The vertex data is interpreted as a series of connected triangles.
        /// </summary>
        TriangleStrip,
    }

    /// <summary>
    ///   Defines an interface that represents a device, physical or hardware, that can provide low-level rendering functionality.
    /// </summary>
    public interface IRenderDevice
    {
        /// <summary>
        ///   Gets an <see cref="IGPUResourceFactory"/> that represents the factory used to create resources for this <see cref="IRenderDevice"/>.
        /// </summary>
        /// <value>
        ///   The factory used to create resources for this <see cref="IRenderDevice"/>.
        /// </value>
        IGPUResourceFactory Factory { get; }

        /// <summary>
        ///   Gets an <see cref="IInputAssembler"/> that represents the input-assembly stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </summary>
        /// <value>
        ///   The input-assembly stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </value>
        IInputAssembler InputAssembler { get; }

        /// <summary>
        ///   Gets an <see cref="IOutputMerger"/> that represents the output-merging stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </summary>
        /// <value>
        ///   The output-merging stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </value>
        IOutputMerger OutputMerger { get; }

        /// <summary>
        ///   Gets an <see cref="IPipeline"/> that represents the CPU-to-GPU connection of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </summary>
        /// <value>
        ///   The CPU-to-GPU connection of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </value>
        IPipeline Pipeline { get; }

        /// <summary>
        ///   Gets an <see cref="IRasterizer"/> that represents the rasterization stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </summary>
        /// <value>
        ///   The rasterization stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
        /// </value>
        IRasterizer Rasterizer { get; }

        /// <summary>
        ///   Clears the currently bound target to the specified <paramref name="color"/>, <paramref name="depth"/> and <paramref name="stencil"/> values.
        /// </summary>
        /// <param name="color">
        ///   Specifies a <see cref="Color"/> that represents the clear value for the color buffer of the currently bound target.
        /// </param>
        /// <param name="depth">
        ///   Specifies a <see cref="float"/> that represents the clear value for the depth buffer of the currently bound target.
        /// </param>
        /// <param name="stencil">
        ///   Specifies an <see cref="int"/> that represents the clear value for the stencil buffer of currently bound target.
        /// </param>
        void Clear(Color color, float depth = 1.0f, int stencil = 0);

        /// <summary>
        ///   Draws indexed, non-instanced primitives, of the specified <paramref name="topology"/>, starting at the specified <paramref name="first"/> location and drawing a total number of <paramref name="count"/> primitives.
        /// </summary>
        /// <param name="topology">
        ///   Specifies a <see cref="PrimitiveTopology"/> that represents the topology used to draw the indexed primitives.
        /// </param>
        /// <param name="first">
        ///   Specifies an <see cref="int"/> that represents the first index to draw from.
        /// </param>
        /// <param name="count">
        ///   Specifies an <see cref="int"/> that represents the total number of indices to draw.
        /// </param>
        void DrawIndices(PrimitiveTopology topology, int first, int count);
    }
}
