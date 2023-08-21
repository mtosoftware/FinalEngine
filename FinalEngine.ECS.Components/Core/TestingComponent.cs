// <copyright file="TestingComponent.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

using System.Numerics;

public class TestingComponent : IEntityComponent
{
    public double DoubleTest { get; set; }

    public float FloatTest { get; set; }

    public bool IsTesting { get; set; }

    public Vector3 Vector3Test { get; set; }

    public Vector4 Vector4Test { get; set; }

    public Vector2 VectorTest { get; set; }
}
