// <copyright file="IMouseStateInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation
{
    using global::OpenTK.Mathematics;
    using global::OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    ///   Defines an interface that provides methods for invocation of a <see cref="MouseState"/>.
    /// </summary>
    public interface IMouseStateInvoker
    {
        /// <inheritdoc cref="MouseState.Delta"/>
        public Vector2 Delta { get; }
    }
}