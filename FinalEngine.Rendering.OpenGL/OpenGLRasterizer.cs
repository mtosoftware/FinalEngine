namespace FinalEngine.Rendering.OpenGL
{
    using System;

    public sealed class OpenGLRasterizer : IRasterizer
    {
        private readonly IOpenGLInvoker invoker;

        public OpenGLRasterizer(IOpenGLInvoker invoker)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified { nameof(invoker) } parameter is null.");
        }

        public void SetRasterState(RasterStateDescription description)
        {
            invoker.SetRasterState(description);
        }

        public void SetScissor(int x, int y, int width, int height)
        {
            invoker.SetScissor(x, y, width, height);
        }

        public void SetViewport(int x, int y, int width, int height)
        {
            invoker.SetViewport(x, y, width, height);
        }
    }
}