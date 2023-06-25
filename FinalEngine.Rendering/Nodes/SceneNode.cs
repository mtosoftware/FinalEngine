// <copyright file="SceneNode.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Nodes;

using System.Numerics;

public abstract class SceneNode
{
    public Matrix4x4 Transformation { get; set; }
}
