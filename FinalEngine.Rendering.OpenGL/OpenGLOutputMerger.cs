// <copyright file="OpenGLOutputMerger.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;

internal sealed class OpenGLOutputMerger : IOutputMerger
{
    private readonly IOpenGLInvoker invoker;

    private readonly IEnumMapper mapper;

    public OpenGLOutputMerger(IOpenGLInvoker invoker, IEnumMapper mapper)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public void SetBlendState(BlendStateDescription description)
    {
        this.invoker.Cap(EnableCap.Blend, description.Enabled);
        this.invoker.BlendColor(description.Color);
        this.invoker.BlendEquation(this.mapper.Forward<BlendEquationMode>(description.EquationMode));
        this.invoker.BlendFunc(this.mapper.Forward<BlendingFactor>(description.SourceMode), this.mapper.Forward<BlendingFactor>(description.DestinationMode));
    }

    public void SetDepthState(DepthStateDescription description)
    {
        this.invoker.Cap(EnableCap.DepthTest, description.ReadEnabled);
        this.invoker.DepthMask(description.WriteEnabled);
        this.invoker.DepthFunc(this.mapper.Forward<DepthFunction>(description.ComparisonMode));
    }

    public void SetStencilState(StencilStateDescription description)
    {
        this.invoker.Cap(EnableCap.StencilTest, description.Enabled);
        this.invoker.StencilMask(description.WriteMask);
        this.invoker.StencilFunc(this.mapper.Forward<StencilFunction>(description.ComparisonMode), description.ReferenceValue, description.ReadMask);
        this.invoker.StencilOp(this.mapper.Forward<StencilOp>(description.StencilFail), this.mapper.Forward<StencilOp>(description.DepthFail), this.mapper.Forward<StencilOp>(description.Pass));
    }
}
