namespace FinalEngine.Rendering
{
    public interface IRasterizer
    {
        void SetRasterState(RasterStateDescription description);

        void SetViewport(int x, int y, int width, int height);
    }
}