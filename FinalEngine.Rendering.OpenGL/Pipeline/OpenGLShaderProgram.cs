// <copyright file="OpenGLShaderProgram.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;

/// <summary>
///   Provides an OpenGL implementation of an <see cref="IOpenGLShaderProgram"/>.
/// </summary>
/// <seealso cref="IOpenGLShaderProgram"/>
public class OpenGLShaderProgram : IOpenGLShaderProgram
{
    /// <summary>
    ///   The OpenGL invoker.
    /// </summary>
    private readonly IOpenGLInvoker invoker;

    /// <summary>
    ///   The OpenGL renderer identifier.
    /// </summary>
    private int rendererID;

    /// <summary>
    ///   Initializes a new instance of the <see cref="OpenGLShaderProgram"/> class.
    /// </summary>
    /// <param name="invoker">
    ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
    /// </param>
    /// <param name="shaders">
    ///   Specifies a <see cref="IReadOnlyCollection{IOpenGLShader}"/> that represents the collection of shaders to attach to this <see cref="OpenGLShaderProgram"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="invoker"/> or <paramref name="shaders"/> parameter is null.
    /// </exception>
    /// <exception cref="ProgramLinkingException">
    ///   The <see cref="OpenGLShaderProgram"/> failed to link - see <see cref="ProgramLinkingException.ErrorLog"/> for more details.
    /// </exception>
    public OpenGLShaderProgram(IOpenGLInvoker invoker, IReadOnlyCollection<IOpenGLShader> shaders)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        if (shaders == null)
        {
            throw new ArgumentNullException(nameof(shaders));
        }

        this.rendererID = this.invoker.CreateProgram();

        foreach (var shader in shaders)
        {
            if (shader == null)
            {
                continue;
            }

            shader.Attach(this.rendererID);
        }

        this.invoker.LinkProgram(this.rendererID);
        this.invoker.ValidateProgram(this.rendererID);

        string? log = this.invoker.GetProgramInfoLog(this.rendererID);

        if (!string.IsNullOrWhiteSpace(log))
        {
            throw new ProgramLinkingException($"The {nameof(OpenGLShaderProgram)} failed to link.", log);
        }
    }

    /// <summary>
    ///   Finalizes an instance of the <see cref="OpenGLShaderProgram"/> class.
    /// </summary>
    ~OpenGLShaderProgram()
    {
        this.Dispose(false);
    }

    /// <summary>
    ///   Gets a value indicating whether this <see cref="OpenGLShaderProgram"/> is disposed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="OpenGLShaderProgram"/> is disposed; otherwise, <c>false</c>.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <summary>
    ///   Binds this <see cref="OpenGLShaderProgram"/> to the graphics processing unit.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="OpenGLShaderProgram"/> has been disposed.
    /// </exception>
    public void Bind()
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenGLShader));
        }

        this.invoker.UseProgram(this.rendererID);
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Gets the uniform location of the specified uniform, <paramref name="name"/>.
    /// </summary>
    /// <param name="name">
    ///   Specifies a <see cref="string"/> that represents the name of uniform to look for.
    /// </param>
    /// <returns>
    ///   The location of the uniform, or -1 if the uniform wasn't located.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    ///   The <see cref="OpenGLShaderProgram"/> has been disposed.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="name"/> parameter is null, empty or consists of only whitespace.
    /// </exception>
    public int GetUniformLocation(string name)
    {
        if (this.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(OpenGLShader));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"The specified {nameof(name)} parameter cannot be null, empty or consist of only whitespace characters.", nameof(name));
        }

        return this.invoker.GetUniformLocation(this.rendererID, name);
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteProgram(this.rendererID);
            this.rendererID = -1;
        }

        this.IsDisposed = true;
    }
}
