namespace FinalEngine.Rendering
{
    public enum CullFaceType
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
                    CullFaceType = CullFaceType.Back,
                    WindingDirection = WindingDirection.CounterClockwise,
                    FillMode = RasterMode.Fill
                };
            }
        }

        public bool CullEnabled { get; set; }

        public CullFaceType CullFaceType { get; set; }

        public RasterMode FillMode { get; set; }

        public WindingDirection WindingDirection { get; set; }
    }
}