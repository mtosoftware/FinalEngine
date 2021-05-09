// <copyright file="ITextureBinder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using FinalEngine.Rendering.Textures;

    /// <summary>
    ///   Defines an interface that represents a GPU-texture binder.
    /// </summary>
    public interface ITextureBinder
    {
        /// <summary>
        ///   Gets a value indicating whether the <see cref="Reset"/> method should be invoked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the method should be invoked; otherwise, <c>false</c>.
        /// </value>
        /// <seealso cref="Reset"/>
        bool ShouldReset { get; }

        /// <summary>
        ///   Gets the texture slot index assigned to the specified <paramref name="texture"/>, or assigns an index to the specified <paramref name="texture"/> and returns the value.
        /// </summary>
        /// <param name="texture">
        ///   The texture to find the index of.
        /// </param>
        /// <returns>
        ///   The texture slot index for the specified <paramref name="texture"/>.
        /// </returns>
        int GetTextureSlotIndex(ITexture texture);

        /// <summary>
        ///   Resets the texture binder by clearing all bound textures from it's cache.
        /// </summary>
        /// <remarks>
        ///   It's up to the implementation whether the textures should be unbound when <see cref="Reset"/> is called.
        /// </remarks>
        void Reset();
    }
}