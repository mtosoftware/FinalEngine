// <copyright file="EntityComponentPrivateSetter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using FinalEngine.ECS;

public sealed class EntityComponentPrivateSetter : IEntityComponent
{
    public int Test { get; private set; }
}
