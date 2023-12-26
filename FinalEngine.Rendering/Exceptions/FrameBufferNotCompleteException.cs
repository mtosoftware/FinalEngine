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
