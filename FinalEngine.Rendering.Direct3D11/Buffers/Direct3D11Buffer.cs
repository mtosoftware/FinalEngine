namespace FinalEngine.Rendering.Direct3D11.Buffers
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using Vortice.Direct3D11;

    public sealed class Direct3D11Buffer : IBuffer
    {
        private bool isDisposed;

        public Direct3D11Buffer(ID3D11Device device, BufferType type, IntPtr data, int sizeInBytes, int strideReference)
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
                BindFlags = type.ToDirect3D(),
                Usage = Usage.Immutable,
                CpuAccessFlags = CpuAccessFlags.None,
                SizeInBytes = sizeInBytes,
                StructureByteStride = strideReference,
                OptionFlags = ResourceOptionFlags.None
            };

            Resource = device.CreateBuffer(description, data);
        }

        ~Direct3D11Buffer()
        {
            Dispose(false);
        }

        public int SizeInBytes { get; }

        public BufferType Type { get; }

        internal ID3D11Buffer Resource { get; private set; }

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
                if (Resource != null)
                {
                    Resource.Dispose();
                    Resource = null;
                }
            }

            isDisposed = true;
        }
    }
}