// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Drawing;
    using FinalEngine.IO;
    using FinalEngine.Rendering.Data;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Rendering.Renderers;
    using FinalEngine.Rendering.Settings;
    using FinalEngine.Resources;

    public class RenderingEngine : IRenderingEngine
    {
        private readonly IFileSystem fileSystem;

        private readonly IGeometryRenderer geometryRenderer;

        private readonly IRenderDevice renderDevice;

        private IShaderProgram shaderProgram;

        public RenderingEngine(
            IRenderDevice renderDevice,
            IFileSystem fileSystem,
            IGeometryRenderer geometryRenderer)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        ~RenderingEngine()
        {
            this.Dispose(false);
        }

        public RenderQualitySettings RenderQualitySettings { get; }

        public TextureQualitySettings TextureQualitySettings { get; }

        protected bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Initialize()
        {
            this.fileSystem.AddVirtualTextFile("material", "Resources\\Shaders\\Includes\\material.glsl");
            this.fileSystem.AddVirtualTextFile("lighting", "Resources\\Shaders\\Includes\\lighting.glsl");

            this.shaderProgram = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\forward-geometry.fesp");
        }

        public void Render(CameraData camera)
        {
            this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
            {
                CullEnabled = true,
                CullMode = FaceCullMode.Back,
                WindingDirection = WindingDirection.CounterClockwise,
                MultiSamplingEnabled = this.RenderQualitySettings.MultiSamplingEnabled,
            });

            this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
            {
                ReadEnabled = true,
            });

            this.renderDevice.Clear(Color.Black);

            this.renderDevice.Pipeline.SetShaderProgram(this.shaderProgram);

            this.UpdateUniforms(camera);
            this.geometryRenderer.Render();

            this.geometryRenderer.ClearGeometry();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.fileSystem.RemoveVirtualTextFile("material");
            this.fileSystem.RemoveVirtualTextFile("lighting");

            ResourceManager.Instance.UnloadResource(this.shaderProgram);

            this.IsDisposed = true;
        }

        private void UpdateUniforms(CameraData camera)
        {
            var uniforms = this.renderDevice.Pipeline.ShaderUniforms;

            foreach (var uniform in uniforms)
            {
                string name = uniform.Name;

                switch (name)
                {
                    case "u_projection":
                        this.renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);
                        break;

                    case "u_view":
                        this.renderDevice.Pipeline.SetUniform("u_view", camera.View);
                        break;

                    default:
                        continue;
                }
            }
        }
    }
}
