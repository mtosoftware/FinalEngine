// <copyright file="RenderingEngine.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using System.Drawing;
using FinalEngine.Rendering.Nodes;

public sealed class RenderingEngine : IRenderingEngine
{
    private readonly IList<MeshNode> meshes;

    private readonly IRenderDevice renderDevice;

    public RenderingEngine(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        this.meshes = new List<MeshNode>();
    }

    public void EnqueueNode(MeshNode node)
    {
        if (node == null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        if (this.meshes.Contains(node))
        {
            throw new ArgumentException($"The specified {nameof(node)} has already been enqueued for rendering.", nameof(node));
        }

        this.meshes.Add(node);
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);
        this.RenderScene();
        this.meshes.Clear();
    }

    private void RenderScene()
    {
        foreach (var node in this.meshes)
        {
            var mesh = node.Mesh;
            var material = node.Material;
            var transform = node.Transformation;

            if (mesh == null || material == null)
            {
                continue;
            }

            this.renderDevice.Pipeline.SetUniform("u_transform", transform);

            //// TODO: Move this the initialization of the bound shader.
            this.renderDevice.Pipeline.SetUniform("u_material.diffuseTexture", 0);
            this.renderDevice.Pipeline.SetUniform("u_material.specularTexture", 1);
            this.renderDevice.Pipeline.SetUniform("u_material.normalTexture", 2);

            this.renderDevice.Pipeline.SetUniform("u_material.shininess", material.Shininess);

            this.renderDevice.Pipeline.SetTexture(material.DiffuseTexture, 0);
            this.renderDevice.Pipeline.SetTexture(material.SpecularTexture, 1);
            this.renderDevice.Pipeline.SetTexture(material.NormalTexture, 2);

            mesh.Bind(this.renderDevice.InputAssembler);
            mesh.Render(this.renderDevice);
        }
    }
}
