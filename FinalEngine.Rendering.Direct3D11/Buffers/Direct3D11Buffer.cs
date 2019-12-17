namespace FinalEngine.Rendering.Direct3D11.Buffers
{
    using System;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Direct3D11.Invokers;
    using Vortice.Direct3D11;

    /// <summary>
    ///   Provides a Direct3D 11 implementation of an <see cref="IBuffer"/>.
    /// </summary>
    /// <seealso cref="FinalEngine.Rendering.Buffers.IBuffer"/>
    public sealed class Direct3D11Buffer : IBuffer
    {
        /// <summary>
        ///   Indicates whether this instance is disposed.
        /// </summary>
        private bool isDisposed;

        public Direct3D11Buffer(ID3D11DeviceInvoker device, BufferType type, IntPtr data, int sizeInBytes, int strideReference)
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

            Type = type;
            SizeInBytes = sizeInBytes;
            StrideReference = strideReference;
        }

        /// <summary>
        ///   Finalizes an instance of the <see cref="Direct3D11Buffer"/> class.
        /// </summary>
        ~Direct3D11Buffer()
        {
            Dispose(false);
        }

        /// <summary>
        ///   Gets a <see cref="int"/> that represents the size in bytes of this <see cref="Direct3D11Buffer"/>.
        /// </summary>
        /// <value>
        ///   The size in bytes of this <see cref="Direct3D11Buffer"/>.
        /// </value>
        public int SizeInBytes { get; }

        /// <summary>
        ///   Gets a <see cref="int"/> that represents the stride reference of this <see cref="Direct3D11Buffer"/>.
        /// </summary>
        /// <value>
        ///   The stride reference of this <see cref="Direct3D11Buffer"/>.
        /// </value>
        public int StrideReference { get; }

        /// <summary>
        ///   Gets a <see cref="BufferType"/> that represents the type of this <see cref="Direct3D11Buffer"/>.
        /// </summary>
        /// <value>
        ///   The type of this <see cref="Direct3D11Buffer"/>.
        /// </value>
        public BufferType Type { get; }

        /// <summary>
        ///   Gets a <see cref="ID3D11Buffer"/> that represents the internal resource of this <see cref="Direct3D11Buffer"/>.
        /// </summary>
        /// <value>
        ///   The internal resource of this <see cref="Direct3D11Buffer"/>.
        /// </value>
        /// <remarks>
        ///   The <see cref="Resource"/> property is provided internally to allow the developer to access the current internal state of a <see cref="Direct3D11Buffer"/>. It's important to <strong>never</strong> call the dispose method from the <see cref="Resource"/> property as this will change the internal state of the <see cref="Direct3D11Buffer"/>. The <see cref="Resource"/> property is only provided to developers as a means to allow development of the Direct3D rendering pipeline.
        /// </remarks>
        internal ID3D11Buffer Resource { get; private set; }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
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