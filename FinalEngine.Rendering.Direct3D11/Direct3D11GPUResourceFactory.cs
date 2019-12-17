namespace FinalEngine.Rendering.Direct3D11
{
    using System;
    using System.Runtime.InteropServices;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Direct3D11.Buffers;
    using FinalEngine.Rendering.Direct3D11.Invokers;

    public sealed class Direct3D11GPUResourceFactory : IGPUResourceFactory
    {
        private readonly ID3D11DeviceInvoker device;

        public Direct3D11GPUResourceFactory(ID3D11DeviceInvoker device)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
        }

        public IBuffer CreateBuffer<T>(BufferType type, T[] data, int sizeInBytes, int structStride) where T : struct
        {
            IBuffer result = null;

            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);

            try
            {
                IntPtr ptr = handle.AddrOfPinnedObject();
                result = new Direct3D11Buffer(device, type, ptr, sizeInBytes, structStride);
            }
            finally
            {
                if (handle.IsAllocated)
                {
                    handle.Free();
                }
            }

            return result;
        }
    }
}