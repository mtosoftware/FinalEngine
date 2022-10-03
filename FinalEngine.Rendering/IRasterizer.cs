// <copyright file="IRasterizer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Drawing;

    /// <summary>
    ///   Defines an interface that represents the rasterization stage of a graphics pipeline.
    /// </summary>
    public interface IRasterizer
    {
        Rectangle GetViewport();

        /// <summary>
        ///   Sets the rasterization state of the rasterizer to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="RasterStateDescription"/> that represents the description that defines the rasterization state.
        /// </param>
        void SetRasterState(RasterStateDescription description);

        /// <summary>
        ///   Sets the scissoring rectangle of the rasterizer to the specified <paramref name="rectangle"/>.
        /// </summary>
        /// <param name="rectangle">
        ///   Specifies a <see cref="Rectangle"/> that represents the rectangle that defines the scissoring rectangle.
        /// </param>
        void SetScissor(Rectangle rectangle);

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
        void SetViewport(Rectangle rectangle, float near = 0.0f, float far = 1.0f);
    }
}