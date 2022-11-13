// <copyright file="GeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using FinalEngine.Rendering.Data;

    public class GeometryRenderer : IGeometryRenderer
    {
        private readonly IList<GeometryData> geometry;

        private readonly IRenderDevice renderDevice;

        public GeometryRenderer(IRenderDevice renderDevice)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.geometry = new List<GeometryData>();
        }

        public void AddGeometry(GeometryData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            this.geometry.Add(data);
        }

        public void ClearGeometry()
        {
            this.geometry.Clear();
        }

        public void Render()
        {
            foreach (var data in this.geometry)
            {
                var transform = data.Transformation;
                var mesh = data.Mesh;
                var material = data.Material;

                this.UpdateUniforms(transform, material);

                this.renderDevice.Pipeline.SetTexture(material.DiffuseTexture, 0);
                this.renderDevice.Pipeline.SetTexture(material.SpecularTexture, 1);

                mesh.Bind(this.renderDevice.InputAssembler);
                mesh.Render(this.renderDevice);
            }
        }

        private void UpdateUniforms(Matrix4x4 transform, IMaterial material)
        {
            var uniforms = this.renderDevice.Pipeline.ShaderUniforms;

            foreach (var uniform in uniforms)
            {
                string name = uniform.Name;

                switch (name)
                {
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

                    default:
                        continue;
                }
            }
        }
    }
}
