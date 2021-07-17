// <copyright file="MockEntitySystemA.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.ECS.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS;

    [ExcludeFromCodeCoverage]
    public class MockEntitySystemA : EntitySystemBase
    {
        public MockEntitySystemA(GameLoopType type)
        {
            this.LoopType = type;
        }

        public Predicate<IReadOnlyEntity> IsMatchFunction { get; set; }

        public override GameLoopType LoopType { get; }

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
}