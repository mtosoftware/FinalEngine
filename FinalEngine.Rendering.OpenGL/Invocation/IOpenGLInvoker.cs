// <copyright file="IOpenGLInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Invocation
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using OpenTK;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    ///   Defines an interface that provides methods for invocation of the <see cref="GL"/> functions.
    /// </summary>
    public interface IOpenGLInvoker
    {
        /// <inheritdoc cref="GL.AttachShader(int, int)"/>
        void AttachShader(int program, int shader);

        /// <inheritdoc cref="GL.BindBuffer(BufferTarget, int)"/>
        void BindBuffer(BufferTarget target, int buffer);

        /// <inheritdoc cref="GL.BindTexture(TextureTarget, int)"/>
        void BindTexture(TextureTarget target, int texture);

        /// <inheritdoc cref="GL.BindTextureUnit(int, int)"/>
        void BindTextureUnit(int unit, int texture);

        /// <inheritdoc cref="GL.BindVertexArray(int)"/>
        void BindVertexArray(int array);

        /// <inheritdoc cref="GL.BindVertexBuffer(int, int, IntPtr, int)"/>
        void BindVertexBuffer(int bindingindex, int buffer, IntPtr offset, int stride);

        /// <inheritdoc cref="GL.BindVertexBuffers(int, int, int*, IntPtr*, int*)"/>
        void BindVertexBuffers(int first, int count, int[] buffers, IntPtr[] offsets, int[] strides);

        /// <inheritdoc cref="GL.BlendColor(Color)"/>
        void BlendColor(Color color);

        /// <inheritdoc cref="GL.BlendEquation(BlendEquationMode)"/>
        void BlendEquation(BlendEquationMode mode);

        /// <inheritdoc cref="GL.BlendFunc(BlendingFactor, BlendingFactor)"/>
        void BlendFunc(BlendingFactor sfactor, BlendingFactor dfactor);

        /// <summary>
        ///   Enables or disables the specified <paramref name="cap"/> based on the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="cap">
        ///   Specifies a <see cref="EnableCap"/> that represents the cap to change.
        /// </param>
        /// <param name="value">
        ///   if set to <c>true</c> the cap will be enabled; otherwise <c>disabled</c>.
        /// </param>
        void Cap(EnableCap cap, bool value);

        /// <inheritdoc cref="GL.Clear(ClearBufferMask)"/>
        void Clear(ClearBufferMask mask);

        /// <inheritdoc cref="GL.ClearColor(Color)"/>
        void ClearColor(Color color);

        /// <inheritdoc cref="GL.ClearDepth(double)"/>
        void ClearDepth(float depth);

        /// <inheritdoc cref="GL.ClearStencil(int)"/>
        void ClearStencil(int s);

        /// <inheritdoc cref="GL.CompileShader(int)"/>
        void CompileShader(int shader);

        /// <inheritdoc cref="GL.CreateBuffers(int, out int)"/>
        int CreateBuffer();

        /// <inheritdoc cref="GL.CreateProgram"/>
        int CreateProgram();

        /// <inheritdoc cref="GL.CreateShader(ShaderType)"/>
        int CreateShader(ShaderType type);

        /// <inheritdoc cref="GL.CreateTextures(TextureTarget, int, out int)"/>
        int CreateTexture(TextureTarget target);

        /// <inheritdoc cref="GL.CullFace(CullFaceMode)"/>
        void CullFace(CullFaceMode mode);

        /// <inheritdoc cref="GL.DeleteBuffer(int)"/>
        void DeleteBuffer(int buffers);

        /// <inheritdoc cref="GL.DeleteProgram(int)"/>
        void DeleteProgram(int program);

        /// <inheritdoc cref="GL.DeleteShader(int)"/>
        void DeleteShader(int shader);

        /// <inheritdoc cref="GL.DeleteTexture(int)"/>
        void DeleteTexture(int textures);

        /// <inheritdoc cref="GL.DeleteVertexArray(int)"/>
        void DeleteVertexArray(int arrays);

        /// <inheritdoc cref="GL.DepthFunc(DepthFunction)"/>
        void DepthFunc(DepthFunction func);

        /// <inheritdoc cref="GL.DepthMask(bool)"/>
        void DepthMask(bool flag);

        /// <inheritdoc cref="GL.DepthRange(float, float)"/>
        void DepthRange(float n, float f);

        /// <inheritdoc cref="GL.Disable(EnableCap)"/>
        void Disable(EnableCap cap);

        /// <inheritdoc cref="GL.DisableVertexAttribArray(int)"/>
        void DisableVertexAttribArray(int index);

        /// <inheritdoc cref="GL.DrawElements(PrimitiveType, int, DrawElementsType, int)"/>
        void DrawElements(PrimitiveType mode, int count, DrawElementsType type, int indices);

        /// <inheritdoc cref="GL.Enable(EnableCap)"/>
        void Enable(EnableCap cap);

        /// <inheritdoc cref="GL.EnableVertexAttribArray(int)"/>
        void EnableVertexAttribArray(int index);

        /// <inheritdoc cref="GL.FrontFace(FrontFaceDirection)"/>
        void FrontFace(FrontFaceDirection mode);

        /// <inheritdoc cref="GL.GenVertexArray"/>
        int GenVertexArray();

        /// <inheritdoc cref="GL.GetInteger(GetIndexedPName, uint, out int)"/>
        void GetInteger(GetIndexedPName target, int index, int[] data);

        /// <inheritdoc cref="GL.GetInteger(GetPName)"/>
        int GetInteger(GetPName pname);

        /// <inheritdoc cref="GL.GetProgramInfoLog(int)"/>
        string GetProgramInfoLog(int program);

        /// <inheritdoc cref="GL.GetShaderInfoLog(int)"/>
        string GetShaderInfoLog(int shader);

        /// <inheritdoc cref="GL.GetUniformLocation(int, string)"/>
        int GetUniformLocation(int program, string name);

        /// <inheritdoc cref="GL.LinkProgram(int)"/>
        void LinkProgram(int program);

        /// <inheritdoc cref="GL.LoadBindings(IBindingsContext)"/>
        void LoadBindings(IBindingsContext context);

        /// <inheritdoc cref="GL.NamedBufferData{T2}(int, int, T2[], BufferUsageHint)"/>
        void NamedBufferData<T2>(int buffer, int size, T2[] data, BufferUsageHint usage)
            where T2 : struct;

        /// <inheritdoc cref="GL.NamedBufferSubData{T3}(int, IntPtr, int, T3[])"/>
        void NamedBufferSubData<T3>(int buffer, IntPtr offset, int size, T3[] data)
            where T3 : struct;

        /// <inheritdoc cref="GL.PolygonMode(MaterialFace, PolygonMode)"/>
        void PolygonMode(MaterialFace face, PolygonMode mode);

        /// <inheritdoc cref="GL.Scissor(int, int, int, int)"/>
        void Scissor(int x, int y, int width, int height);

        /// <inheritdoc cref="GL.ShaderSource(int, string)"/>
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Must Match API")]
        [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Must Much API")]
        void ShaderSource(int shader, string @string);

        /// <inheritdoc cref="GL.StencilFunc(StencilFunction, int, int)"/>
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Must Match API")]
        void StencilFunc(StencilFunction func, int @ref, int mask);

        /// <inheritdoc cref="GL.StencilMask(int)"/>
        void StencilMask(int mask);

        /// <inheritdoc cref="GL.StencilOp(StencilOp, StencilOp, StencilOp)"/>
        void StencilOp(StencilOp fail, StencilOp zfail, StencilOp zpass);

        /// <inheritdoc cref="GL.TextureParameter(int, TextureParameterName, int)"/>
        void TextureParameter(int texture, TextureParameterName pname, int param);

        /// <inheritdoc cref="GL.TextureStorage2D(int, int, SizedInternalFormat, int, int)"/>
        void TextureStorage2D(int texture, int levels, SizedInternalFormat internalFormat, int width, int height);

        /// <inheritdoc cref="GL.TextureSubImage2D(int, int, int, int, int, int, PixelFormat, PixelType, IntPtr)"/>
        void TextureSubImage2D(int texture, int level, int xoffset, int yoffset, int width, int height, PixelFormat format, PixelType type, IntPtr pixels);

        /// <inheritdoc cref="GL.Uniform1(int, int)"/>
        void Uniform1(int location, int x);

        /// <inheritdoc cref="GL.Uniform1(int, float)"/>
        void Uniform1(int location, float v0);

        /// <inheritdoc cref="GL.Uniform1(int, double)"/>
        void Uniform1(int location, double x);

        /// <inheritdoc cref="GL.Uniform2(int, float, float)"/>
        void Uniform2(int location, float v0, float v1);

        /// <inheritdoc cref="GL.Uniform3(int, float, float, float)"/>
        void Uniform3(int location, float v0, float v1, float v2);

        /// <inheritdoc cref="GL.Uniform4(int, float, float, float, float)"/>
        void Uniform4(int location, float v0, float v1, float v2, float v3);

        /// <inheritdoc cref="GL.UniformMatrix4(int, int, bool, float[])"/>
        void UniformMatrix4(int location, int count, bool transpose, float[] value);

        /// <inheritdoc cref="GL.UseProgram(int)"/>
        void UseProgram(int program);

        /// <inheritdoc cref="GL.ValidateProgram(int)"/>
        void ValidateProgram(int program);

        /// <inheritdoc cref="GL.VertexAttribBinding(int, int)"/>
        void VertexAttribBinding(int attribindex, int bindingindex);

        /// <inheritdoc cref="GL.VertexAttribFormat(int, int, VertexAttribType, bool, int)"/>
        void VertexAttribFormat(int attribindex, int size, VertexAttribType type, bool normalized, int relativeoffset);

        /// <inheritdoc cref="GL.Viewport(Rectangle)"/>
        void Viewport(Rectangle rectangle);
    }
}
