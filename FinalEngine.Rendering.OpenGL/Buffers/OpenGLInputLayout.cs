// <copyright file="OpenGLInputLayout.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Buffers;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;

/// <summary>
///   Provides an OpenGL implementation of an <see cref="IOpenGLInputLayout"/>.
/// </summary>
/// <seealso cref="IOpenGLInputLayout"/>
public class OpenGLInputLayout : IOpenGLInputLayout
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
    ///   Initializes a new instance of the <see cref="OpenGLInputLayout"/> class.
    /// </summary>
    /// <param name="invoker">
    ///   Specifies an <see cref="IOpenGLInvoker"/> that represents the invoker used to invoke OpenGL calls.
    /// </param>
    /// <param name="mapper">
    ///   Specifies an <see cref="IEnumMapper"/> that represents the enumeration mapper used to map OpenGL enumerations to the rendering APIs equivalent.
    /// </param>
    /// <param name="elements">
    ///   Specifies a <see cref="IReadOnlyCollection{InputElement}"/> that represents each individual elements formatting of the vertex buffer data.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="invoker"/>, <paramref name="mapper"/> or <paramref name="elements"/> parameter is null.
    /// </exception>
    public OpenGLInputLayout(IOpenGLInvoker invoker, IEnumMapper mapper, IReadOnlyCollection<InputElement> elements)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.Elements = elements ?? throw new ArgumentNullException(nameof(elements));
    }

    /// <summary>
    ///   Gets the elements that describe the formating of vertex buffer data for this <see cref="OpenGLInputLayout"/>.
    /// </summary>
    /// <value>
    ///   The elements that describe the formating of vertex buffer data for this <see cref="OpenGLInputLayout"/>.
    /// </value>
    public IEnumerable<InputElement> Elements { get; }

    /// <summary>
    ///   Binds this <see cref="OpenGLInputLayout"/> to the graphics processing unit.
    /// </summary>
    public void Bind()
    {
        foreach (var element in this.Elements)
        {
            this.invoker.VertexAttribFormat(element.Index, element.Size, this.mapper.Forward<VertexAttribType>(element.Type), false, element.RelativeOffset);
            this.invoker.VertexAttribBinding(element.Index, 0);
            this.invoker.EnableVertexAttribArray(element.Index);
        }
    }

    /// <summary>
    ///   Unbinds this <see cref="OpenGLInputLayout"/> from the graphics processing unit.
    /// </summary>
    public void Unbind()
    {
        foreach (var element in this.Elements)
        {
            this.invoker.DisableVertexAttribArray(element.Index);
        }
    }
}
