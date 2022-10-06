// <copyright file="PerspectiveCameraUpdateEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Cameras
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;
    using FinalEngine.ECS.Components.Cameras;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.Rendering;

    public class PerspectiveCameraUpdateEntitySystem : EntitySystemBase
    {
        private readonly IPipeline pipeline;

        private readonly IRasterizer rasterizer;

        public PerspectiveCameraUpdateEntitySystem(IPipeline pipeline, IRasterizer rasterizer)
        {
            this.pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
            this.rasterizer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));
            this.LoopType = GameLoopType.Render;
        }

        public override GameLoopType LoopType { get; }

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<PerspectiveCameraComponent>() &&
                   entity.ContainsComponent<TransformComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (dynamic entity in entities)
            {
                TransformComponent transform = entity.Transform;
                PerspectiveCameraComponent camera = entity.PerspectiveCamera;

                if (!camera.IsEnabled)
                {
                    continue;
                }

                this.rasterizer.SetViewport(camera.Viewport);
                this.pipeline.SetUniform("u_projection", camera.CreateProjectionMatrix());
                this.pipeline.SetUniform("u_view", transform.CreateViewMatrix(Vector3.UnitY));
            }
        }
    }
}