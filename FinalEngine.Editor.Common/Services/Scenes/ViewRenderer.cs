// <copyright file="ViewRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

public sealed class ViewRenderer : IViewRenderer
{
    private readonly IRenderDevice renderDevice;

    private readonly IRenderPipeline renderPipeline;

    private readonly ISceneManager sceneManager;

    private bool isInitialized;

    private IMaterial material;

    private IMesh mesh;

    private IShaderProgram shaderProgram;

    public ViewRenderer(IRenderPipeline renderPipeline, IRenderDevice renderDevice, ISceneManager sceneManager)
    {
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
    }

    public void AdjustView(int width, int height)
    {
        this.renderDevice.Rasterizer.SetViewport(new Rectangle(0, 0, width, height));
    }

    public void Render()
    {
        this.Initialize();

        this.renderDevice.Clear(Color.FromArgb(255, 30, 30, 30));

        this.renderDevice.Pipeline.SetShaderProgram(this.shaderProgram);

        this.mesh.Bind(this.renderDevice.InputAssembler);
        this.material.Bind(this.renderDevice.Pipeline);
        this.mesh.Draw(this.renderDevice);

        this.sceneManager.ActiveScene.Render();
    }

    private void Initialize()
    {
        if (this.isInitialized)
        {
            return;
        }

        this.renderPipeline.Initialize();

        float[] vertices =
        [
            -1, -1, 0, 1, 0, 0,
            1, -1, 0, 0, 1, 0,
            0, 1, 0, 0, 0, 1,
        ];

        int[] indices =
        [
            0, 1, 2,
        ];

        InputElement[] inputElements =
        [
            new InputElement(0, 3, InputElementType.Float, 0),
            new InputElement(1, 3, InputElementType.Float, 3 * sizeof(float)),
        ];

        this.mesh = new Mesh<float>(this.renderDevice.Factory, vertices, indices, inputElements, 6 * sizeof(float));
        this.material = new Material();

        this.shaderProgram = ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Testing\\standard-triangle.fesp");

        this.isInitialized = true;
    }
}
