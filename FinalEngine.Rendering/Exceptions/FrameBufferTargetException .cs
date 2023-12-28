namespace FinalEngine.Rendering.Exceptions;
using System;

public class FrameBufferTargetException : Exception
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
