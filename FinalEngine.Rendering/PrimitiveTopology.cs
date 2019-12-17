namespace FinalEngine.Rendering
{
    /// <summary>
    ///   Enumerates the available topologies that determine how consecutive vertices are organized into primitives, along with the type of primitive that will be used.
    /// </summary>
    public enum PrimitiveTopology
    {
        /// <summary>
        ///   Specifies a list of isolated triangles.
        /// </summary>
        Triangle,

        /// <summary>
        ///   Specifies a series of connected triangles.
        /// </summary>
        TriangleStrip,

        /// <summary>
        ///   Specifies a list of isolated, straight line segments.
        /// </summary>
        Line,

        /// <summary>
        ///   Specifies a series of connected line segments.
        /// </summary>
        LineStrip
    }
}