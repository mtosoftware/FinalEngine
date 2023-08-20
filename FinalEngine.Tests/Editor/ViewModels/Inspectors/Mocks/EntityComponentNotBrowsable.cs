// <copyright file="EntityComponentNotBrowsable.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using System.ComponentModel;
using FinalEngine.ECS;

public sealed class EntityComponentNotBrowsable : IEntityComponent
{
    [Browsable(false)]
    public string Test { get; set; }
}
