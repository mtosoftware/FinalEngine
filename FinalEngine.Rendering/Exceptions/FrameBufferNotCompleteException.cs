// <copyright file="FrameBufferNotCompleteException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;

public class FrameBufferNotCompleteException : Exception
{
    public FrameBufferNotCompleteException()
    {
    }

    public FrameBufferNotCompleteException(string? message)
        : base(message)
    {
    }

    public FrameBufferNotCompleteException(string? message, Exception? inner)
        : base(message, inner)
    {
    }
}
