// <copyright file="MeshNode.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Nodes;

public sealed class MeshNode : SceneNode
{
    public IMaterial? Material { get; set; }

    public IMesh? Mesh { get; set; }
}
