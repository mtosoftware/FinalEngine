// <copyright file="MockEntitySystemB.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.ECS.Mocks;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;

[EntitySystemProcess(ExecutionType = GameLoopType.Render)]
public class MockEntitySystemB : EntitySystemBase
{
    public Predicate<IReadOnlyEntity> IsMatchFunction { get; set; }

    public Action<IEnumerable<Entity>> ProcessFunction { get; set; }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return this.IsMatchFunction?.Invoke(entity) ?? false;
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        this.ProcessFunction?.Invoke(entities);
    }
}
