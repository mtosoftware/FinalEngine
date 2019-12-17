namespace FinalEngine.Rendering.Direct3D.Pipeline
{
    using System;
    using FinalEngine.Rendering.Direct3D.Invokers;
    using FinalEngine.Rendering.Pipeline;
    using Vortice.Direct3D;
    using Vortice.Direct3D11;

    public sealed class Direct3DVertexShader : IShader
    {
        private bool isDisposed;

        public Direct3DVertexShader(ID3D11DeviceInvoker device, Blob blob)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), $"The specified { nameof(device) } parameter is null.");
            }

            if (blob == null)
            {
                throw new ArgumentNullException(nameof(blob), $"The specified { nameof(blob) } parameter is null.");
            }

            Resource = device.CreateVertexShader(blob.BufferPointer, blob.BufferSize);
            Blob = blob;
        }

        ~Direct3DVertexShader()
        {
            Dispose(false);
        }

        public Blob Blob { get; private set; }

        public PipelineTarget EntryPoint
        {
            get { return PipelineTarget.Vertex; }
        }

        public ID3D11VertexShader Resource { get; private set; }

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

                if (Blob != null)
                {
                    Blob.Release();
                    Blob = null;
                }
            }

            isDisposed = true;
        }
    }
}