// <copyright file="RenderEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Drawing;
    using FinalEngine.ECS;
    using FinalEngine.Rendering.Pipeline;

    public class RenderEngine : IRenderEngine
    {
        private readonly IRenderDevice renderDevice;

        private readonly IShaderProgram shaderProgram;

        public RenderEngine(IRenderDevice renderDevice, IShaderProgram shaderProgram)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.shaderProgram = shaderProgram ?? throw new ArgumentNullException(nameof(shaderProgram));
        }

        public void Initialize()
        {
            //// TODO: Set raster, depth, blend and stencil states.
            this.renderDevice.Pipeline.SetShaderProgram(this.shaderProgram);
            this.renderDevice.Pipeline.SetUniform("u_material.diffuseTexture", 0);
        }

        public void Render(IEntitySystemsProcessor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            this.renderDevice.Clear(Color.Black);
            this.renderDevice.Pipeline.SetShaderProgram(this.shaderProgram);

            processor.ProcessAll(GameLoopType.Render);
        }
    }
}
