// <copyright file="SceneRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.ECS.Components.Rendering;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    public class SceneRenderEntitySystem : EntitySystemBase
    {
        private static readonly IShaderProgram ShaderProgram = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Programs\\Default");

        private readonly IRenderDevice renderDevice;

        public SceneRenderEntitySystem(IRenderDevice renderDevice)
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
            this.renderDevice.Clear(Color.CornflowerBlue);
            this.renderDevice.Pipeline.SetShaderProgram(ShaderProgram);

            foreach (var entity in entities)
            {
                //// var transform = entity.GetComponent<TransformComponent>();
                var model = entity.GetComponent<ModelComponent>();

                model.Material?.Bind(this.renderDevice.Pipeline);
                model.Mesh?.Bind(this.renderDevice.InputAssembler);
                model.Mesh?.Render(this.renderDevice);
            }
        }
    }
}
