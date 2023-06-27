// <copyright file="IMesh.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;

/// <summary>
/// Defines an interface that is responsible for rendering geometry.
/// </summary>
/// <seealso cref="System.IDisposable" />
public interface IMesh : IDisposable
{
    /// <summary>
    /// Binds this <see cref="IMesh"/> to the GPU using the specified <paramref name="inputAssembler"/>.
    /// </summary>
    /// <param name="inputAssembler">
    /// The input assembler used to bind this <see cref="IMesh"/> to the GPU.
    /// </param>
    void Bind(IInputAssembler inputAssembler);

    /// <summary>
    /// Draws this <see cref="IMesh"/> to the currently bound render target using the specified <paramref name="renderDevice"/>.
    /// </summary>
    /// <param name="renderDevice">
    /// The render device used to draw this <see cref="IMesh"/> to the currently bound render target.
    /// </param>
    void Draw(IRenderDevice renderDevice);
}
