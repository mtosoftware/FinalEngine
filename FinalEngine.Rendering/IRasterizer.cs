namespace FinalEngine.Rendering
{
    /// <summary>
    ///   Defines an interface that represents the rasterizeration stage of a rendering pipeline..
    /// </summary>
    public interface IRasterizer
    {
        /// <summary>
        ///   Sets the state of the rasterizzer to the specified <paramref name="description"/>.
        /// </summary>
        /// <param name="description">
        ///   Specifies a <see cref="RasterStateDescription"/> that represents the describes the state to set this <see cref="IRasterizer"/> too.
        /// </param>
        void SetRasterState(RasterStateDescription description);

        /// <summary>
        ///   Sets the viewport.
        /// </summary>
        /// <param name="x">
        ///   Specifies a <see cref="Int32"/> that represents the X-coordinate (from the lower left corner) of the testing area.
        /// </param>
        /// <param name="y">
        ///   Specifies a <see cref="Int32"/> that represents the Y-coordainte (From the lower left corner) of the testing area.
        /// </param>
        /// <param name="width">
        ///   Specifies a <see cref="Int32"/> that represents the width of the testing area.
        /// </param>
        /// <param name="height">
        ///   Specifies a <see cref="Int32"/> that represents the height of the testing area.
        /// </param>
        void SetViewport(int x, int y, int width, int height);
    }
}