namespace FinalEngine.Rendering.Direct3D.Pipeline
{
    using System;
    using FinalEngine.Rendering.Direct3D.Invokers;
    using FinalEngine.Rendering.Pipeline;
    using Vortice.Direct3D;
    using Vortice.Direct3D11;

    public sealed class Direct3DFragmentShader : IShader
    {
        private bool isDisposed;

        public Direct3DFragmentShader(ID3D11DeviceInvoker device, Blob blob)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
            }

            Resource = device.CreatePixelShader(blob.BufferPointer, blob.BufferSize);
        }

        ~Direct3DFragmentShader()
        {
            Dispose(false);
        }

        public PipelineTarget EntryPoint
        {
            get { return PipelineTarget.Fragment; }
        }

        public ID3D11PixelShader Resource { get; private set; }

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
                    Resource.Release();
                    Resource = null;
                }
            }

            isDisposed = true;
        }
    }
}