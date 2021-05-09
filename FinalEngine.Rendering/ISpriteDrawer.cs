// <copyright file="ISpriteDrawer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Rendering.Textures;

    /// <summary>
    ///   Defines an interface that draws textures to an unspecified surface.
    /// </summary>
    public interface ISpriteDrawer
    {
        /// <summary>
        ///   Gets or sets the projection.
        /// </summary>
        /// <value>
        ///   The projection.
        /// </value>
        Matrix4x4 Projection { get; set; }

        /// <summary>
        ///   Gets or sets the transform.
        /// </summary>
        /// <value>
        ///   The transform.
        /// </value>
        Matrix4x4 Transform { get; set; }

        /// <summary>
        ///   Initializes the drawer, this must be called <c>before</c> you invoke the <see cref="Draw(ITexture2D, Color, Vector2, Vector2, float, Vector2)"/> method.
        /// </summary>
        void Begin();

        /// <summary>
        ///   Draws the specified texture, blended with the specified <paramref name="color"/>, with the specified <paramref name="origin"/>, at the specified <paramref name="position"/>, <paramref name="rotation"/> and <paramref name="scale"/>.
        /// </summary>
        /// <param name="texture">
        ///   The texture to draw.
        /// </param>
        /// <param name="color">
        ///   The color of the texture.
        /// </param>
        /// <param name="origin">
        ///   The origin of the texture.
        /// </param>
        /// <param name="position">
        ///   The position of the texture, relative to it's origin.
        /// </param>
        /// <param name="rotation">
        ///   The rotation of the texture, relative to it's origin.
        /// </param>
        /// <param name="scale">
        ///   The scale of the texture.
        /// </param>
        void Draw(ITexture2D texture, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale);

        /// <summary>
        ///   Flushes the contents of the drawer to the unspecified surface.
        /// </summary>
        /// <remarks>
        ///   This must be called <c>after</c> you've made a call too <see cref="Draw(ITexture2D, Color, Vector2, Vector2, float, Vector2)"/> as otherwise the drawer might behave incorrectly.
        /// </remarks>
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Begin/End is easier to understand.")]
        void End();
    }
}