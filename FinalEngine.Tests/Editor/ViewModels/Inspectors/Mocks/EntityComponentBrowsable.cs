// <copyright file="EntityComponentBrowsable.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using System.ComponentModel;
using FinalEngine.ECS;

public sealed class EntityComponentBrowsable : IEntityComponent
{
    [Browsable(true)]
    public string Test { get; set; }
}
