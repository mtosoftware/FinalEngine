// <copyright file="EntityComponentVector3.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using System.Numerics;
using FinalEngine.ECS;

public sealed class EntityComponentVector3 : IEntityComponent
{
    public Vector3 Vector3 { get; set; }
}
