// <copyright file="EntityComponentVector2.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using System.Numerics;
using FinalEngine.ECS;

public sealed class EntityComponentVector2 : IEntityComponent
{
    public Vector2 Test { get; set; }
}
