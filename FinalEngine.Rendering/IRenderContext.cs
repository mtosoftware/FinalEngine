namespace FinalEngine.Rendering
{
    using System;

    public interface IRenderContext : IDisposable
    {
        void SwapBuffers();
    }
}