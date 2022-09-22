// <copyright file="SceneRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Rendering;
    using Microsoft.Extensions.Logging;

    public class SceneRenderer : ISceneRenderer
    {
        private readonly ILogger<SceneRenderer> logger;

        private readonly IRenderDevice renderDevice;

        public SceneRenderer(IRenderDevice renderDevice, ILogger<SceneRenderer> logger)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void ChangeProjection(int projectionWidth, int projectionHeight)
        {
            this.logger.LogInformation($"Changing projection to: ({projectionWidth}, {projectionHeight}).");

            this.renderDevice.Pipeline.SetUniform("u_projection", Matrix4x4.CreateOrthographicOffCenter(0, projectionWidth, 0, projectionHeight, -1, 1));
            this.renderDevice.Rasterizer.SetViewport(new Rectangle(0, 0, projectionWidth, projectionHeight));
        }

        public void Render()
        {
            this.renderDevice.Clear(Color.CornflowerBlue);
        }
    }
}