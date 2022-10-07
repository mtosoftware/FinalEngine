// <copyright file="IOutputMerger.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    /// <summary>
    ///   Defines an interface that represents the output-merging stage of a graphics pipeline.
    /// </summary>
    public interface IOutputMerger
    {
        /// <summary>
        ///   Sets the blend state of the output merger to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="BlendStateDescription"/> that represents the description that defines the blend state.
        /// </param>
        void SetBlendState(BlendStateDescription description);

        /// <summary>
        ///   Sets the depth state of the output merger to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="DepthStateDescription"/> that represents the description that defines the depth state.
        /// </param>
        void SetDepthState(DepthStateDescription description);

        /// <summary>
        ///   Sets the stencil state of the output merger to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="StencilStateDescription"/> that represents the description that defines the stencil state.
        /// </param>
        void SetStencilState(StencilStateDescription description);
    }
}
