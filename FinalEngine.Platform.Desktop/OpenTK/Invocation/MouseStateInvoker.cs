// <copyright file="MouseStateInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using global::OpenTK.Mathematics;
    using global::OpenTK.Windowing.GraphicsLibraryFramework;

    [ExcludeFromCodeCoverage(Justification = "Invocation Class")]
    public class MouseStateInvoker : IMouseStateInvoker
    {
        private readonly MouseState state;

        public MouseStateInvoker(MouseState state)
        {
            this.state = state ?? throw new ArgumentNullException(nameof(state), $"The specified {nameof(state)} parameter cannot be null.");
        }

        public Vector2 Delta
        {
            get { return this.state.Delta; }
        }
    }
}