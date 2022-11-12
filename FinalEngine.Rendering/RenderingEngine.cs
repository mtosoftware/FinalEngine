// <copyright file="RenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.IO;
    using FinalEngine.Rendering.Lighting;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    public class RenderingEngine : IRenderingEngine
    {
        private readonly IList<GeometryData> geometryDatas;

        private readonly Queue<LightBase> lights;

        private readonly IRenderDevice renderDevice;

        private LightBase? activeLight;

        private IShaderProgram? shaderProgram;

        public RenderingEngine(IRenderDevice renderDevice, IFileSystem fileSystem)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            fileSystem.AddVirtualTextFile("material", "Resources\\Shaders\\Includes\\material.glsl");
            fileSystem.AddVirtualTextFile("lighting", "Resources\\Shaders\\Includes\\lighting.glsl");

            this.geometryDatas = new List<GeometryData>();
            this.lights = new Queue<LightBase>();

            this.shaderProgram = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\forward-geometry.fesp");
        }

        ~RenderingEngine()
        {
            this.Dispose(false);
        }

        protected bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Enqueue(GeometryData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            this.geometryDatas.Add(data);
        }

        public void Enqueue(LightBase light)
        {
            if (light == null)
            {
                throw new ArgumentNullException(nameof(light));
            }

            this.lights.Enqueue(light);
        }

        public void Render(CameraData camera)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(RenderingEngine));
            }

            this.renderDevice.Clear(Color.Black);

            this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
            {
                ReadEnabled = true,
            });

            this.RenderGeometryData(this.shaderProgram!, camera);

            this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
            {
                Enabled = true,
                SourceMode = BlendMode.One,
                DestinationMode = BlendMode.Zero,
                EquationMode = BlendEquationMode.Add,
            });

            this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
            {
                ReadEnabled = true,
                WriteEnabled = false,
                ComparisonMode = ComparisonMode.Equal,
            });

            while (this.lights.Count > 0)
            {
                this.activeLight = this.lights.Dequeue();
                this.RenderGeometryData(this.activeLight.ShaderProgram, camera);
            }

            this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
            {
                ReadEnabled = true,
                WriteEnabled = true,
                ComparisonMode = ComparisonMode.Less,
            });

            this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
            {
                Enabled = false,
            });

            this.geometryDatas.Clear();
            this.lights.Clear();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.shaderProgram != null)
                {
                    ResourceManager.Instance.UnloadResource(this.shaderProgram);

                    this.shaderProgram.Dispose();
                    this.shaderProgram = null;
                }
            }

            this.IsDisposed = true;
        }

        private void RenderGeometryData(IShaderProgram shaderProgram, CameraData camera)
        {
            this.renderDevice.Pipeline.SetShaderProgram(shaderProgram);

            foreach (var data in this.geometryDatas)
            {
                var transform = data.Transformation;
                var mesh = data.Mesh;
                var material = data.Material;

                this.UpdateUniforms(transform, material, camera);

                this.renderDevice.Pipeline.SetTexture(material.DiffuseTexture, 0);
                this.renderDevice.Pipeline.SetTexture(material.SpecularTexture, 1);

                mesh.Bind(this.renderDevice.InputAssembler);
                mesh.Render(this.renderDevice);
            }
        }

        private void UpdateUniforms(Matrix4x4 transform, IMaterial material, CameraData camera)
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

                    case "u_transform":
                        this.renderDevice.Pipeline.SetUniform("u_transform", transform);
                        break;

                    case "u_material.diffuseTexture":
                        this.renderDevice.Pipeline.SetUniform("u_material.diffuseTexture", 0);
                        break;

                    case "u_material.specularTexture":
                        this.renderDevice.Pipeline.SetUniform("u_material.specularTexture", 1);
                        break;

                    case "u_material.shininess":
                        this.renderDevice.Pipeline.SetUniform("u_material.shininess", material.Shininess);
                        break;

                    case "u_light.base.ambientColor":
                        if (this.activeLight == null)
                        {
                            continue;
                        }

                        this.renderDevice.Pipeline.SetUniform("u_light.base.ambientColor", this.activeLight.AmbientColor);
                        break;

                    case "u_light.base.diffuseColor":
                        if (this.activeLight == null)
                        {
                            continue;
                        }

                        this.renderDevice.Pipeline.SetUniform("u_light.base.ambientColor", this.activeLight.DiffuseColor);
                        break;

                    case "u_light.base.specularColor":
                        if (this.activeLight == null)
                        {
                            continue;
                        }

                        this.renderDevice.Pipeline.SetUniform("u_light.base.specularColor", this.activeLight.SpecularColor);
                        break;

                    case "u_light.direction":
                        if (this.activeLight is not DirectionalLight directionalLight)
                        {
                            continue;
                        }

                        this.renderDevice.Pipeline.SetUniform("u_light.direction", directionalLight.Direction);
                        break;

                    case "u_viewPosition":
                        this.renderDevice.Pipeline.SetUniform("u_viewPosition", camera.ViewPostiion);
                        break;

                    default:
                        throw new NotSupportedException($"The specified uniform name: '{name}' is not supported by the rendering engine.");
                }
            }
        }
    }
}
