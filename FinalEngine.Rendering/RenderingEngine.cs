// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using FinalEngine.IO;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    public class RenderingEngine : IRenderingEngine
    {
        private readonly IRenderDevice renderDevice;

        private readonly IDictionary<RenderStage, Action> stageToRenderMap;

        private IShaderProgram? geometryShader;

        public RenderingEngine(IRenderDevice renderDevice, IFileSystem fileSystem)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.stageToRenderMap = new Dictionary<RenderStage, Action>();

            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            fileSystem.AddVirtualTextFile("material", "Resources\\Shaders\\Includes\\material.glsl");

            this.geometryShader = renderDevice.Factory.CreateShaderProgram(
                new[]
                {
                    ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\Forward\\forward-geometry.vert"),
                    ResourceManager.Instance.LoadResource<IShader>("Resources\\Shaders\\Forward\\forward-geometry.frag"),
                });
        }

        ~RenderingEngine()
        {
            this.Dispose(false);
        }

        protected bool IsDisposed { get; private set; }

        public void AddRenderAction(RenderStage stage, Action render)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(RenderingEngine));
            }

            if (render == null)
            {
                throw new ArgumentNullException(nameof(render));
            }

            if (this.stageToRenderMap.ContainsKey(stage))
            {
                throw new ArgumentException($"The specified {nameof(stage)} already has an action added to this {nameof(RenderingEngine)}.");
            }

            this.stageToRenderMap.Add(stage, render);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RemoveRenderAction(RenderStage stage)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(RenderingEngine));
            }

            if (!this.stageToRenderMap.ContainsKey(stage))
            {
                throw new ArgumentException($"The specified {nameof(stage)} does not have an action added to this {nameof(RenderingEngine)}.");
            }

            this.stageToRenderMap.Remove(stage);
        }

        public void Render()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(RenderingEngine));
            }

            this.renderDevice.Pipeline.SetShaderProgram(this.geometryShader!);
            this.renderDevice.Clear(Color.Black);
            this.PerformAction(RenderStage.Geometry);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.geometryShader != null)
                {
                    this.geometryShader.Dispose();
                    this.geometryShader = null;
                }
            }

            this.IsDisposed = true;
        }

        private void PerformAction(RenderStage stage)
        {
            if (!this.stageToRenderMap.TryGetValue(stage, out var render))
            {
                return;
            }

            render();
        }
    }
}
