namespace FinalEngine.Rendering.Direct3D11.Buffers
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using Vortice.Direct3D11;

    public sealed class Direct3D11Buffer : IBuffer
    {
        private bool isDisposed;

        private ID3D11Buffer resource;

        public Direct3D11Buffer(ID3D11Device device, BufferType type, IntPtr data, int sizeInBytes)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
            }

            if (data == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(device), $"The specified { nameof(data) } parameter is null.");
            }

            var description = new BufferDescription()
            {
                BindFlags = type == BufferType.VertexBuffer ? BindFlags.VertexBuffer : BindFlags.IndexBuffer,
                Usage = Usage.Immutable,
                CpuAccessFlags = CpuAccessFlags.None,
                SizeInBytes = sizeInBytes,
                StructureByteStride = 0,
                OptionFlags = ResourceOptionFlags.None
            };

            resource = device.CreateBuffer(description, data);
        }

        ~Direct3D11Buffer()
        {
            Dispose(false);
        }

        public int SizeInBytes { get; }

        public BufferType Type { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (resource != null)
                {
                    resource.Dispose();
                    resource = null;
                }
            }

            isDisposed = true;
        }
    }
}