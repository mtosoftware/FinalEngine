// <copyright file="SceneRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Numerics;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.ECS.Components.Rendering;
    using FinalEngine.Rendering;

    public class SceneRenderEntitySystem : EntitySystemBase
    {
        private readonly IRenderDevice renderDevice;

        public SceneRenderEntitySystem(IRenderDevice renderDevice, IRenderingEngine renderingEngine)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

            if (renderingEngine == null)
            {
                throw new ArgumentNullException(nameof(renderingEngine));
            }

            renderingEngine.AddRenderAction(RenderStage.Geometry, this.Process);
        }

        public Entity? Camera { get; set; }

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<ModelComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            if (this.Camera == null)
            {
                return;
            }

            this.SetupCamera();

            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<TransformComponent>();
                var model = entity.GetComponent<ModelComponent>();

                this.renderDevice.Pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());

                model.Material?.Bind(this.renderDevice.Pipeline);
                model.Mesh?.Bind(this.renderDevice.InputAssembler);
                model.Mesh?.Render(this.renderDevice);
            }
        }

        private void SetupCamera()
        {
            if (!this.Camera?.ContainsComponent<PerspectiveComponent>() ?? false)
            {
                return;
            }

            if (!this.Camera?.ContainsComponent<TransformComponent>() ?? false)
            {
                return;
            }

            var perspective = this.Camera?.GetComponent<PerspectiveComponent>();
            var transform = this.Camera?.GetComponent<TransformComponent>();

            this.renderDevice.Pipeline.SetUniform("u_projection", perspective!.CreateProjectionMatrix());
            this.renderDevice.Pipeline.SetUniform("u_view", transform!.CreateViewMatrix(Vector3.UnitY));
        }
    }
}
