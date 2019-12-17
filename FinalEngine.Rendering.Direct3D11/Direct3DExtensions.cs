namespace FinalEngine.Rendering.Direct3D11
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using Vortice.Direct3D11;
    using D3DPrimitiveTopology = Vortice.Direct3D.PrimitiveTopology;

    public static class Direct3DExtensions
    {
        public static D3DPrimitiveTopology ToDirect3D(this PrimitiveTopology topology)
        {
            switch (topology)
            {
                case PrimitiveTopology.Line:
                    return D3DPrimitiveTopology.LineList;

                case PrimitiveTopology.LineStrip:
                    return D3DPrimitiveTopology.LineStrip;

                case PrimitiveTopology.Triangle:
                    return D3DPrimitiveTopology.TriangleList;

                case PrimitiveTopology.TriangleStrip:
                    return D3DPrimitiveTopology.TriangleStrip;

                default:
                    throw new NotSupportedException($"The specified { nameof(topology) } parameter, { topology }, is not supported yet.");
            }
        }

        public static BindFlags ToDirect3D(this BufferType type)
        {
            switch (type)
            {
                case BufferType.IndexBuffer:
                    return BindFlags.IndexBuffer;

                case BufferType.VertexBuffer:
                    return BindFlags.VertexBuffer;

                default:
                    throw new NotSupportedException($"The specified { nameof(type) } parameter, { type }, is not supported yet.");
            }
        }
    }
}