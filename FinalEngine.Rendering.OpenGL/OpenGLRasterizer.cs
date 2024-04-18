// <copyright file="OpenGLRasterizer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using System.Drawing;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;

internal sealed class OpenGLRasterizer : IRasterizer
{
    private readonly IOpenGLInvoker invoker;

    private readonly IEnumMapper mapper;

    private RasterStateDescription currentDescription;

    public OpenGLRasterizer(IOpenGLInvoker invoker, IEnumMapper mapper)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public RasterStateDescription GetRasterState()
    {
        return this.currentDescription;
    }

    public Rectangle GetViewport()
    {
        int[] data = new int[4];
        this.invoker.GetInteger(GetIndexedPName.Viewport, 0, data);

        return new Rectangle()
        {
            X = data[0],
            Y = data[1],
            Width = data[2],
            Height = data[3],
        };
    }

    public void SetRasterState(RasterStateDescription description)
    {
        if (this.currentDescription == description)
        {
            return;
        }

        this.invoker.Cap(EnableCap.CullFace, description.CullEnabled);
        this.invoker.Cap(EnableCap.ScissorTest, description.ScissorEnabled);
        this.invoker.Cap(EnableCap.Multisample, description.MultiSamplingEnabled);
        this.invoker.CullFace(this.mapper.Forward<CullFaceMode>(description.CullMode));
        this.invoker.FrontFace(this.mapper.Forward<FrontFaceDirection>(description.WindingDirection));
        this.invoker.PolygonMode(MaterialFace.FrontAndBack, this.mapper.Forward<PolygonMode>(description.FillMode));

        this.currentDescription = description;
    }

    public void SetScissor(Rectangle rectangle)
    {
        this.invoker.Scissor(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
    }

    public void SetViewport(Rectangle rectangle, float near = 0.0f, float far = 1.0f)
    {
        this.invoker.Viewport(rectangle);
        this.invoker.DepthRange(near, far);
    }
}
