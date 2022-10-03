// <copyright file="OpenGLRasterizer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL
{
    using System;
    using System.Drawing;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Utilities;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    ///   Provides an OpenGL implementation of an <see cref="IRasterizer"/>.
    /// </summary>
    /// <seealso cref="IRasterizer"/>
    public class OpenGLRasterizer : IRasterizer
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
        ///   Initializes a new instance of the <see cref="OpenGLRasterizer"/> class.
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
        public OpenGLRasterizer(IOpenGLInvoker invoker, IEnumMapper mapper)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), $"The specified {nameof(mapper)} parameter cannot be null.");
        }

        /// <summary>
        ///   Gets the viewport.
        /// </summary>
        /// <returns>
        ///   The viewport.
        /// </returns>
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

        /// <summary>
        ///   Sets the rasterization state of the rasterizer to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="RasterStateDescription"/> that represents the description that defines the rasterization state.
        /// </param>
        public void SetRasterState(RasterStateDescription description)
        {
            this.invoker.Cap(EnableCap.CullFace, description.CullEnabled);
            this.invoker.Cap(EnableCap.ScissorTest, description.ScissorEnabled);
            this.invoker.CullFace(this.mapper.Forward<CullFaceMode>(description.CullMode));
            this.invoker.FrontFace(this.mapper.Forward<FrontFaceDirection>(description.WindingDirection));
            this.invoker.PolygonMode(MaterialFace.FrontAndBack, this.mapper.Forward<PolygonMode>(description.FillMode));
        }

        /// <summary>
        ///   Sets the scissoring rectangle of the rasterizer to the specified <paramref name="rectangle"/>.
        /// </summary>
        /// <param name="rectangle">
        ///   Specifies a <see cref="Rectangle"/> that represents the rectangle that defines the scissoring rectangle.
        /// </param>
        public void SetScissor(Rectangle rectangle)
        {
            this.invoker.Scissor(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        ///   Sets the view-port to the specified <paramref name="rectangle"/> and sets the mapping of depth values from normalized device coordinates to window coordinates.
        /// </summary>
        /// <param name="rectangle">
        ///   Specifies a <see cref="Rectangle"/> that represents the rectangle that defines the view-port rectangle.
        /// </param>
        /// <param name="near">
        ///   Specifies a <see cref="float"/> that represents the mapping of the near clipping plane to window coordinates.
        /// </param>
        /// <param name="far">
        ///   Specifies a <see cref="float"/> that represents the mapping of the far clipping plane to window coordinates.
        /// </param>
        public void SetViewport(Rectangle rectangle, float near = 0.0f, float far = 1.0f)
        {
            this.invoker.Viewport(rectangle);
            this.invoker.DepthRange(near, far);
        }
    }
}