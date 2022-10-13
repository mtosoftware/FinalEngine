// <copyright file="OpenGLOutputMerger.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Utilities;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    ///   Provides an OpenGL implementation of an <see cref="IOutputMerger"/>.
    /// </summary>
    /// <seealso cref="IOutputMerger"/>
    public class OpenGLOutputMerger : IOutputMerger
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
        ///   Initializes a new instance of the <see cref="OpenGLOutputMerger"/> class.
        /// </summary>
        /// <param name="invoker">
        ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
        /// </param>
        /// <param name="mapper">
        ///   Specifies an <see cref="IEnumMapper"/> that represents the enumeration mapper used to map OpenGL enumerations to the rendering APIs equivalent.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="invoker"/> or <paramref name="mapper"/> parameter is null.
        /// </exception>
        public OpenGLOutputMerger(IOpenGLInvoker invoker, IEnumMapper mapper)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), $"The specified {nameof(mapper)} parameter cannot be null.");
        }

        /// <summary>
        ///   Sets the blend state of the output merger to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="BlendStateDescription"/> that represents the description that defines the blend state.
        /// </param>
        public void SetBlendState(BlendStateDescription description)
        {
            this.invoker.Cap(EnableCap.Blend, description.Enabled);
            this.invoker.BlendColor(description.Color);
            this.invoker.BlendEquation(this.mapper.Forward<BlendEquationMode>(description.EquationMode));
            this.invoker.BlendFunc(this.mapper.Forward<BlendingFactor>(description.SourceMode), this.mapper.Forward<BlendingFactor>(description.DestinationMode));
        }

        /// <summary>
        ///   Sets the depth state of the output merger to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="DepthStateDescription"/> that represents the description that defines the depth state.
        /// </param>
        public void SetDepthState(DepthStateDescription description)
        {
            this.invoker.Cap(EnableCap.DepthTest, description.ReadEnabled);
            this.invoker.DepthMask(description.WriteEnabled);
            this.invoker.DepthFunc(this.mapper.Forward<DepthFunction>(description.ComparisonMode));
        }

        public void SetRenderTarget(RenderTargetBindType type, IRenderTarget? renderTarget)
        {
            var target = this.mapper.Forward<FramebufferTarget>(type);

            if (renderTarget == null)
            {
                this.invoker.Bindframebuffer(target, 0);
                return;
            }

            if (renderTarget is not IOpenGLRenderTarget glRenderTarget)
            {
                throw new ArgumentException($"The specified {nameof(renderTarget)} parameter is not of type {nameof(IOpenGLRenderTarget)}.", nameof(renderTarget));
            }

            glRenderTarget.Bind(target);
        }

        /// <summary>
        ///   Sets the stencil state of the output merger to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="StencilStateDescription"/> that represents the description that defines the stencil state.
        /// </param>
        public void SetStencilState(StencilStateDescription description)
        {
            this.invoker.Cap(EnableCap.StencilTest, description.Enabled);
            this.invoker.StencilMask(description.WriteMask);
            this.invoker.StencilFunc(this.mapper.Forward<StencilFunction>(description.ComparisonMode), description.ReferenceValue, description.ReadMask);
            this.invoker.StencilOp(this.mapper.Forward<StencilOp>(description.StencilFail), this.mapper.Forward<StencilOp>(description.DepthFail), this.mapper.Forward<StencilOp>(description.Pass));
        }
    }
}
