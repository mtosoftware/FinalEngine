// <copyright file="EntityComponentPrivateGetter.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Inspectors.Mocks;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;

public sealed class EntityComponentPrivateGetter : IEntityComponent
{
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used via reflection.")]
    private int Test { get; }
}
