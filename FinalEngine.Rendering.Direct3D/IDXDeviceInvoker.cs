namespace FinalEngine.Rendering.Direct3D
{
    public interface IDXDeviceInvoker
    {
        void SetRasterState(RasterStateDescription description);

        void SetScissor(int x, int y, int width, int height);

        void SetViewport(int x, int y, int width, int height);
    }
}