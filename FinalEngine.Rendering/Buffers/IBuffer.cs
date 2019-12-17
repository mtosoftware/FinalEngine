namespace FinalEngine.Rendering.Buffers
{
    using System;

    public interface IBuffer : IDisposable
    {
        int SizeInBytes { get; }

        BufferType Type { get; }
    }
}