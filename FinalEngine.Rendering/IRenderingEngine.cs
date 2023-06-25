// <copyright file="IRenderingEngine.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using FinalEngine.Rendering.Nodes;

public interface IRenderingEngine
{
    void EnqueueNode(MeshNode node);

    void Render();
}
