// <copyright file="EntityComponentUnsupportedPropertyType.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using FinalEngine.ECS;

public sealed class EntityComponentUnsupportedPropertyType : IEntityComponent
{
    public object Test { get; set; }
}
