namespace FinalEngine.Rendering.Direct3D.Pipeline
{
    using System;
    using FinalEngine.Rendering.Pipeline;
    using Vortice.Direct3D11;

    public sealed class Direct3DFragmentShader : IShader
    {
        private bool isDisposed;

        private ID3D11PixelShader resource;

        public Direct3DFragmentShader(ID3D11PixelShader resource)
        {
            this.resource = resource ?? throw new ArgumentNullException(nameof(resource), $"The specified { nameof(resource) } parameter is null.");
        }

        ~Direct3DFragmentShader()
        {
            Dispose(false);
        }

        public PipelineTarget EntryPoint
        {
            get { return PipelineTarget.Fragment; }
        }

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
                    resource.Release();
                    resource = null;
                }
            }

            isDisposed = true;
        }
    }
}