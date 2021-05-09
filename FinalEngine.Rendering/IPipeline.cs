// <copyright file="IPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Numerics;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Rendering.Textures;

    /// <summary>
    ///   Defines an interface that represents the CPU-to-GPU connection of a graphics pipeline.
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        ///   Gets the maximum texture slots that can be utilized by the GPU.
        /// </summary>
        /// <value>
        ///   The maximum texture slots that can be utilized by the GPU.
        /// </value>
        int MaxTextureSlots { get; }

        /// <summary>
        ///   Sets the specified <paramref name="program"/>, binding it to the GPU.
        /// </summary>
        /// <param name="program">
        ///   Specifies an <see cref="IShaderProgram"/> that represents the shader program to bind.
        /// </param>
        /// <remarks>
        ///   Passing <c>null</c> to the <paramref name="program"/> parameter will unbind the previously bound shader program.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="program"/> is not the correct implementation. If this exception occurs, you're attempting to bind an shader program that was created with a different rendering API than the one that's currently in use.
        /// </exception>
        void SetShaderProgram(IShaderProgram? program);

        /// <summary>
        ///   Sets the specified <paramref name="texture"/>, binding it to the GPU and activates the specified <paramref name="slot"/>.
        /// </summary>
        /// <param name="texture">
        ///   Specifies a <see cref="Nullable{ITexture}"/> that represents The texture to bind.
        /// </param>
        /// <param name="slot">
        ///   Specifies an <see cref="int"/> that represents the texture slot to activate.
        /// </param>
        /// <remarks>
        ///   Passing <c>null</c> to the <paramref name="texture"/> parameter will unbind the previously bound texture.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="texture"/> is not the correct implementation. If this exception occurs, you're attempting to bind an texture that was created with a different rendering API than the one that's currently in use.
        /// </exception>
        void SetTexture(ITexture? texture, int slot = 0);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies an <see cref="int"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, int value);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies a <see cref="float"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, float value);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies a <see cref="double"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, double value);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies a <see cref="bool"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, bool value);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies a <see cref="Vector2"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, Vector2 value);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies a <see cref="Vector3"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, Vector3 value);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies a <see cref="Vector4"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, Vector4 value);

        /// <summary>
        ///   Sets the uniform of the specified <paramref name="name"/>, contained in the currently bound program to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of the uniform to set.
        /// </param>
        /// <param name="value">
        ///   Specifies a <see cref="Matrix4x4"/> that represents the value to change the uniform too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="name"/> parameter is null, empty or consists of only white-space.
        /// </exception>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        void SetUniform(string name, Matrix4x4 value);
    }
}