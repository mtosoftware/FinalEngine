// <copyright file="IShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Pipeline
{
    using System;
    using FinalEngine.Resources;

    /// <summary>
    ///   Enumerates the available entry point for a shader.
    /// </summary>
    public enum PipelineTarget
    {
        /// <summary>
        ///   The entry point is defined as a vertex shader.
        /// </summary>
        Vertex,

        /// <summary>
        ///   The entry point is defined as a fragment shader.
        /// </summary>
        Fragment,
    }

    /// <summary>
    ///   Defines an interface that represents a shader.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public interface IShader : IResource
    {
        /// <summary>
        ///   Gets the entry point of this <see cref="IShader"/>.
        /// </summary>
        /// <value>
        ///   The entry point of this <see cref="IShader"/>.
        /// </value>
        PipelineTarget EntryPoint { get; }
    }
}
