// <copyright file="OpenGLPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using FinalEngine.Rendering.Exceptions;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Pipeline;
    using FinalEngine.Rendering.OpenGL.Textures;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Rendering.Textures;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    ///   Provides an OpenGL implementation of an <see cref="IPipeline"/>.
    /// </summary>
    /// <seealso cref="IPipeline"/>
    public class OpenGLPipeline : IPipeline
    {
        /// <summary>
        ///   The initial size capacity of the cached uniform locations.
        /// </summary>
        private const int InitialSizeCapacity = 50;

        /// <summary>
        ///   The OpenGL invoker.
        /// </summary>
        private readonly IOpenGLInvoker invoker;

        /// <summary>
        ///   The cached uniform locations.
        /// </summary>
        private readonly IDictionary<string, int> uniformLocations;

        /// <summary>
        ///   The currently bound OpenGL shader program.
        /// </summary>
        /// <remarks>
        ///   Used to unbind when a user passes <c>null</c> to <see cref="SetShaderProgram(IShaderProgram?)"/>.
        /// </remarks>
        private IOpenGLShaderProgram? boundProgram;

        /// <summary>
        ///   Initializes a new instance of the <see cref="OpenGLPipeline"/> class.
        /// </summary>
        /// <param name="invoker">
        ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="invoker"/> parameter is null.
        /// </exception>
        public OpenGLPipeline(IOpenGLInvoker invoker)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
            this.uniformLocations = new Dictionary<string, int>(InitialSizeCapacity);
        }

        /// <summary>
        ///   Gets the maximum texture slots that can be utilized by the GPU.
        /// </summary>
        /// <value>
        ///   The maximum texture slots that can be utilized by the GPU.
        /// </value>
        public int MaxTextureSlots
        {
            get { return this.invoker.GetInteger(GetPName.MaxTextureImageUnits); }
        }

        /// <summary>
        ///   Sets the specified <paramref name="program"/>, binding it to the GPU.
        /// </summary>
        /// <param name="program">
        ///   Specifies an <see cref="IShaderProgram"/> that represents the shader program to bind.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="program"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="program"/> is not the correct implementation. If this exception occurs, you're attempting to bind an shader program that does not implement <see cref="IOpenGLShaderProgram"/>.
        /// </exception>
        public void SetShaderProgram(IShaderProgram program)
        {
            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            if (program is not IOpenGLShaderProgram glProgram)
            {
                throw new ArgumentException($"The specified {nameof(program)} parameter is not of type {nameof(IOpenGLShaderProgram)}.", nameof(program));
            }

            this.boundProgram = glProgram;
            this.boundProgram.Bind();
        }

        /// <summary>
        ///   Sets the specified <paramref name="texture"/> to the specified <paramref name="slot"/> and binds it to the GPU.
        /// </summary>
        /// <param name="texture">
        ///   Specifies an <see cref="ITexture"/> that represents the texture to bind.
        /// </param>
        /// <param name="slot">
        ///   Specifies an <see cref="int"/> that represents the texture slot to the bind the <paramref name="texture"/> too.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="texture"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="texture"/> is not the correct implementation. If this exception occurs, you're attempting to bind an texture that does not implement <see cref="IOpenGLTexture"/>.
        /// </exception>
        public void SetTexture(ITexture texture, int slot = 0)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            if (texture is not IOpenGLTexture glTexture)
            {
                throw new ArgumentException($"The specified {nameof(texture)} parameter is not of type {nameof(IOpenGLTexture)}.", nameof(texture));
            }

            glTexture.Bind(slot);
        }

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
        public void SetUniform(string name, int value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value);
        }

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
        public void SetUniform(string name, float value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value);
        }

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
        public void SetUniform(string name, double value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value);
        }

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
        public void SetUniform(string name, bool value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform1(location, value ? 1 : 0);
        }

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
        public void SetUniform(string name, Vector2 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform2(location, value.X, value.Y);
        }

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
        public void SetUniform(string name, Vector3 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform3(location, value.X, value.Y, value.Z);
        }

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
        public void SetUniform(string name, Vector4 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            this.invoker.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }

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
        public void SetUniform(string name, Matrix4x4 value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
            }

            if (!this.TryGetUniformLocation(name, out int location))
            {
                return;
            }

            float[] values =
            {
                value.M11, value.M12, value.M13, value.M14,
                value.M21, value.M22, value.M23, value.M24,
                value.M31, value.M32, value.M33, value.M34,
                value.M41, value.M42, value.M43, value.M44,
            };

            this.invoker.UniformMatrix4(location, 1, false, values);
        }

        /// <summary>
        ///   Tries the get uniform location of the specified uniform, <paramref name="name"/>.
        /// </summary>
        /// <param name="name">
        ///   Specifies a <see cref="string"/> that represents the name of uniform to look for.
        /// </param>
        /// <param name="location">
        ///   Specifies an <see cref="int"/> that represents the location of the uniform, or 0 if a program isn't bound.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the uniform location has been located; <c>false</c> if a program isn't bound to the GPU.
        /// </returns>
        /// <exception cref="UniformNotLocatedException">
        ///   The specified uniform, <paramref name="name"/> couldn't be located.
        /// </exception>
        private bool TryGetUniformLocation(string name, out int location)
        {
            if (this.boundProgram == null)
            {
                location = 0;
                return false;
            }

            if (!this.uniformLocations.TryGetValue(name, out location))
            {
                location = this.boundProgram.GetUniformLocation(name);

                if (location == -1)
                {
                    throw new UniformNotLocatedException($"The specified uniform, {name} couldn't be located.", nameof(name));
                }

                this.uniformLocations.Add(name, location);
            }

            return true;
        }
    }
}
