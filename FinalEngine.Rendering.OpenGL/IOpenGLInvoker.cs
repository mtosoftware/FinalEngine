namespace FinalEngine.Rendering.OpenGL
{
    public interface IOpenGLInvoker
    {
        void SetRasterState(RasterStateDescription description);

        void SetScissor(int x, int y, int width, int height);

        void SetViewport(int x, int y, int width, int height);
    }
}