// <copyright file="OpenGLShaderProgram.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.OpenGL.Invocation;

public class OpenGLShaderProgram : IOpenGLShaderProgram, IDisposable
{
    private readonly IOpenGLInvoker invoker;

    private readonly Dictionary<string, int> uniformNameToLocationMap;

    private int rendererID;

    public OpenGLShaderProgram(IOpenGLInvoker invoker, IReadOnlyCollection<IOpenGLShader> shaders)
    {
        ArgumentNullException.ThrowIfNull(shaders, nameof(shaders));

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.rendererID = this.invoker.CreateProgram();
        this.uniformNameToLocationMap = [];

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

    ~OpenGLShaderProgram()
    {
        this.Dispose(false);
    }

    protected bool IsDisposed { get; private set; }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        this.invoker.UseProgram(this.rendererID);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public bool TryGetUniformLocation(string name, out int location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.uniformNameToLocationMap.TryGetValue(name, out location))
        {
            location = this.GetUniformLocation(name);

            if (location == -1)
            {
                return false;
            }

            this.uniformNameToLocationMap.Add(name, location);
        }

        return true;
    }

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

    private int GetUniformLocation(string name)
    {
        ObjectDisposedException.ThrowIf(this.IsDisposed, this);
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        return this.invoker.GetUniformLocation(this.rendererID, name);
    }
}
