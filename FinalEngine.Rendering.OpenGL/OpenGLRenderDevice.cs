// <copyright file="OpenGLRenderDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using System.Collections.Generic;
using System.Drawing;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;
using BlendEquationMode = FinalEngine.Rendering.BlendEquationMode;
using PixelFormat = FinalEngine.Rendering.Textures.PixelFormat;
using PixelType = FinalEngine.Rendering.Textures.PixelType;
using TextureWrapMode = FinalEngine.Rendering.Textures.TextureWrapMode;
using TKBlendEquationMode = OpenTK.Graphics.OpenGL4.BlendEquationMode;
using TKPixelForamt = OpenTK.Graphics.OpenGL4.PixelFormat;
using TKPixelType = OpenTK.Graphics.OpenGL4.PixelType;

/// <summary>
///   Provides an OpenGL implementation of an <see cref="IRenderDevice"/>.
/// </summary>
/// <seealso cref="IRenderDevice"/>
public class OpenGLRenderDevice : IRenderDevice
{
    /// <summary>
    ///   The OpenGL invoker.
    /// </summary>
    private readonly IOpenGLInvoker invoker;

    /// <summary>
    ///   The OpenGL-to-FinalEngine enumeration mapper.
    /// </summary>
    /// <remarks>
    ///   Used to map OpenGL enumerations to the rendering APIs equivalent.
    /// </remarks>
    private readonly IEnumMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenGLRenderDevice"/> class.
    /// </summary>
    public OpenGLRenderDevice()
        : this(new OpenGLInvoker())
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="OpenGLRenderDevice"/> class.
    /// </summary>
    /// <param name="invoker">
    ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="invoker"/> parameter is null.
    /// </exception>
    public OpenGLRenderDevice(IOpenGLInvoker invoker)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        var map = new Dictionary<Enum, Enum>()
        {
            { PrimitiveTopology.Line, PrimitiveType.Lines },
            { PrimitiveTopology.LineStrip, PrimitiveType.LineStrip },
            { PrimitiveTopology.Triangle, PrimitiveType.Triangles },
            { PrimitiveTopology.TriangleStrip, PrimitiveType.TriangleStrip },
            { FaceCullMode.Back, CullFaceMode.Back },
            { FaceCullMode.Front, CullFaceMode.Front },
            { WindingDirection.Clockwise, FrontFaceDirection.Cw },
            { WindingDirection.CounterClockwise, FrontFaceDirection.Ccw },
            { RasterMode.Solid, PolygonMode.Fill },
            { RasterMode.Wireframe, PolygonMode.Line },
            { InputElementType.Byte, VertexAttribType.Byte },
            { InputElementType.Double, VertexAttribType.Double },
            { InputElementType.Float, VertexAttribType.Float },
            { InputElementType.Int, VertexAttribType.Int },
            { InputElementType.Short, VertexAttribType.Short },
            { PipelineTarget.Vertex, ShaderType.VertexShader },
            { PipelineTarget.Fragment, ShaderType.FragmentShader },
            { BlendEquationMode.Add, TKBlendEquationMode.FuncAdd },
            { BlendEquationMode.Max, TKBlendEquationMode.Max },
            { BlendEquationMode.Min, TKBlendEquationMode.Min },
            { BlendEquationMode.Subtract, TKBlendEquationMode.FuncSubtract },
            { BlendMode.DestinationAlpha, BlendingFactor.DstAlpha },
            { BlendMode.DestinationColor, BlendingFactor.DstColor },
            { BlendMode.One, BlendingFactor.One },
            { BlendMode.OneMinusDestinationAlpha, BlendingFactor.OneMinusDstAlpha },
            { BlendMode.OneMinusDestinationColor, BlendingFactor.OneMinusDstColor },
            { BlendMode.OneMinusSourceAlpha, BlendingFactor.OneMinusSrcAlpha },
            { BlendMode.OneMinusSourceColor, BlendingFactor.OneMinusSrcColor },
            { BlendMode.SourceAlpha, BlendingFactor.SrcAlpha },
            { BlendMode.SourceColor, BlendingFactor.SrcColor },
            { BlendMode.Zero, BlendingFactor.Zero },
            { ComparisonMode.Always, All.Always },
            { ComparisonMode.Equal, All.Equal },
            { ComparisonMode.Greater, All.Greater },
            { ComparisonMode.GreaterEqual, All.Gequal },
            { ComparisonMode.Less, All.Less },
            { ComparisonMode.LessEqual, All.Lequal },
            { ComparisonMode.Never, All.Never },
            { StencilOperation.Decrement, StencilOp.Decr },
            { StencilOperation.DecrementWrap, StencilOp.DecrWrap },
            { StencilOperation.Increment, StencilOp.Incr },
            { StencilOperation.IncrementWrap, StencilOp.IncrWrap },
            { StencilOperation.Invert, StencilOp.Invert },
            { StencilOperation.Keep, StencilOp.Keep },
            { StencilOperation.Replace, StencilOp.Replace },
            { StencilOperation.Zero, StencilOp.Zero },
            { TextureFilterMode.Linear, All.Linear },
            { TextureFilterMode.Nearest, All.Nearest },
            { TextureFilterMode.LinearMipmapLinear, All.LinearMipmapLinear },
            { TextureWrapMode.Clamp, All.ClampToEdge },
            { TextureWrapMode.Repeat, All.Repeat },
            { PixelType.Byte,  TKPixelType.UnsignedByte },
            { PixelType.Int, TKPixelType.UnsignedInt },
            { PixelType.Short, TKPixelType.UnsignedShort },
            { PixelFormat.R, TKPixelForamt.Red },
            { PixelFormat.Rg, TKPixelForamt.Rg },
            { PixelFormat.Rgb, TKPixelForamt.Rgb },
            { PixelFormat.Rgba, TKPixelForamt.Rgba },
            { PixelFormat.Depth, TKPixelForamt.DepthComponent },
            { PixelFormat.DepthAndStencil, TKPixelForamt.DepthStencil },
            { SizedFormat.R8, SizedInternalFormat.R8 },
            { SizedFormat.Rg8, SizedInternalFormat.Rg8 },
            { SizedFormat.Rgb8, All.Rgb8 },
            { SizedFormat.Rgba8, SizedInternalFormat.Rgba8 },
            { BufferUsageType.Static, BufferUsageHint.StaticDraw },
            { BufferUsageType.Dynamic, BufferUsageHint.DynamicDraw },
        };

        this.mapper = new EnumMapper(map);

        this.Factory = new OpenGLGPUResourceFactory(invoker, this.mapper);
        this.InputAssembler = new OpenGLInputAssembler();
        this.OutputMerger = new OpenGLOutputMerger(invoker, this.mapper);
        this.Pipeline = new OpenGLPipeline(invoker);
        this.Rasterizer = new OpenGLRasterizer(invoker, this.mapper);
    }

    /// <summary>
    ///   Gets an <see cref="IGPUResourceFactory"/> that represents the factory used to create resources for this <see cref="IRenderDevice"/>.
    /// </summary>
    /// <value>
    ///   The factory used to create resources for this <see cref="IRenderDevice"/>.
    /// </value>
    public IGPUResourceFactory Factory { get; }

    /// <summary>
    ///   Gets an <see cref="IInputAssembler"/> that represents the input-assembly stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </summary>
    /// <value>
    ///   The input-assembly stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </value>
    public IInputAssembler InputAssembler { get; }

    /// <summary>
    ///   Gets an <see cref="IOutputMerger"/> that represents the output-merging stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </summary>
    /// <value>
    ///   The output-merging stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </value>
    public IOutputMerger OutputMerger { get; }

    /// <summary>
    ///   Gets an <see cref="IPipeline"/> that represents the CPU-to-GPU connection of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </summary>
    /// <value>
    ///   The CPU-to-GPU connection of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </value>
    public IPipeline Pipeline { get; }

    /// <summary>
    ///   Gets an <see cref="IRasterizer"/> that represents the rasterization stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </summary>
    /// <value>
    ///   The rasterization stage of a rendering pipeline for this <see cref="IRenderDevice"/>.
    /// </value>
    public IRasterizer Rasterizer { get; }

    /// <summary>
    ///   Clears the currently bound target to the specified <paramref name="color"/>, <paramref name="depth"/> and <paramref name="stencil"/> values.
    /// </summary>
    /// <param name="color">
    ///   Specifies a <see cref="Color"/> that represents the clear value for the color buffer of the currently bound target.
    /// </param>
    /// <param name="depth">
    ///   Specifies a <see cref="float"/> that represents the clear value for the depth buffer of the currently bound target.
    /// </param>
    /// <param name="stencil">
    ///   Specifies an <see cref="int"/> that represents the clear value for the stencil buffer of currently bound target.
    /// </param>
    public void Clear(Color color, float depth = 1, int stencil = 0)
    {
        this.invoker.ClearColor(color);
        this.invoker.ClearDepth(depth);
        this.invoker.ClearStencil(stencil);
        this.invoker.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
    }

    /// <summary>
    ///   Draws indexed, non-instanced primitives, of the specified <paramref name="topology"/>, starting at the specified <paramref name="first"/> location and drawing a total number of <paramref name="count"/> primitives.
    /// </summary>
    /// <param name="topology">
    ///   Specifies a <see cref="PrimitiveTopology"/> that represents the topology used to draw the indexed primitives.
    /// </param>
    /// <param name="first">
    ///   Specifies an <see cref="int"/> that represents the first index to draw from.
    /// </param>
    /// <param name="count">
    ///   Specifies an <see cref="int"/> that represents the total number of indices to draw.
    /// </param>
    public void DrawIndices(PrimitiveTopology topology, int first, int count)
    {
        this.invoker.DrawElements(this.mapper.Forward<PrimitiveType>(topology), count, DrawElementsType.UnsignedInt, first);
    }
}
