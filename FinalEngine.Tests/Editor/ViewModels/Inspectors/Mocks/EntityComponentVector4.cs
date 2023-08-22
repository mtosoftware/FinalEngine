// <copyright file="EntityComponentVector4.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using System.Numerics;
using FinalEngine.ECS;

public sealed class EntityComponentVector4 : IEntityComponent
{
    public Vector4 Test { get; set; }
}
