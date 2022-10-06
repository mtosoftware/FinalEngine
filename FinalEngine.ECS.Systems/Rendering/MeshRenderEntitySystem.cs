// <copyright file="MeshRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.ECS.Components.Rendering;
    using FinalEngine.Rendering;

    public class MeshRenderEntitySystem : EntitySystemBase
    {
        private readonly IRenderDevice renderDevice;

        public MeshRenderEntitySystem(IRenderDevice renderDevice)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.LoopType = GameLoopType.Render;
        }

        public override GameLoopType LoopType { get; }

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<ModelComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            foreach (dynamic entity in entities)
            {
                TransformComponent transform = entity.Transform;
                ModelComponent model = entity.Model;

                if (model.Mesh == null)
                {
                    continue;
                }

                if (model.Material != null)
                {
                    model.Material.Bind(this.renderDevice.Pipeline);
                }
                else
                {
                    Material.Default.Bind(this.renderDevice.Pipeline);
                }

                this.renderDevice.Pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());

                model.Mesh.Bind(this.renderDevice.InputAssembler);
                model.Mesh.Render(this.renderDevice);
            }
        }
    }
}