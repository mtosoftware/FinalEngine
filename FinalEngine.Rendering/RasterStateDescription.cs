namespace FinalEngine.Rendering
{
    public enum CullFaceMode
    {
        Front,

        Back
    }

    public enum RasterMode
    {
        Fill,

        Line
    }

    public enum WindingDirection
    {
        Clockwise,

        CounterClockwise
    }

    public struct RasterStateDescription
    {
        public static RasterStateDescription Default
        {
            get
            {
                return new RasterStateDescription()
                {
                    CullEnabled = false,
                    CullFaceMode = CullFaceMode.Back,
                    WindingDirection = WindingDirection.CounterClockwise,
                    FillMode = RasterMode.Fill,
                    ScissorEnabled = false
                };
            }
        }

        public bool CullEnabled { get; set; }

        public CullFaceMode CullFaceMode { get; set; }

        public RasterMode FillMode { get; set; }

        public bool ScissorEnabled { get; set; }

        public WindingDirection WindingDirection { get; set; }
    }
}