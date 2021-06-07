// <copyright file="EntitySystemBaseTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.ECS
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS;

    [ExcludeFromCodeCoverage]
    public class EntitySystemBaseTests
    {
        private readonly EntitySystemBase system;

        private class MockEntitySystem : EntitySystemBase
        {
            private readonly Predicate<IReadOnlyEntity> isMatch;

            private readonly Action<IEnumerable<Entity>> process;

            public MockEntitySystem(GameLoopType type, Predicate<IReadOnlyEntity> isMatch, Action<IEnumerable<Entity>> process)
            {
                this.LoopType = type;
                this.isMatch = isMatch;
                this.process = process;
            }

            public override GameLoopType LoopType { get; }

            protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
            {
                return this.isMatch(entity);
            }

            protected override void Process([NotNull] IEnumerable<Entity> entities)
            {
                this.process(entities);
            }
        }
    }
}