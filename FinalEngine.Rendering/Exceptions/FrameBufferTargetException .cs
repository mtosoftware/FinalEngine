// <copyright file="FrameBufferTargetException .cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;

public sealed class FrameBufferTargetException : Exception
{
    public FrameBufferTargetException()
    {
    }

    public FrameBufferTargetException(string? message)
        : base(message)
    {
    }

    public FrameBufferTargetException(string? message, Exception? inner)
        : base(message, inner)
    {
    }
}
