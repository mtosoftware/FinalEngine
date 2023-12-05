// <copyright file="IMouseStateInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation;

using global::OpenTK.Mathematics;

public interface IMouseStateInvoker
{
    public Vector2 Delta { get; }
}
