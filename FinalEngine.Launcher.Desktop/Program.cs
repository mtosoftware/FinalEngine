namespace RenderingAPI
{
    using System;
    using System.Runtime.InteropServices;
    using FinalEngine.Platform.Desktop;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Direct3D11;
    using Vortice.D3DCompiler;
    using Vortice.Direct3D;
    using Vortice.Direct3D11;
    using Vortice.DXGI;
    using Vortice.Mathematics;
    using PrimitiveTopology = FinalEngine.Rendering.PrimitiveTopology;
    using Usage = Vortice.DXGI.Usage;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public float X;

        public float Y;

        public float Z;

        public float CX;

        public float CY;

        public float CZ;

        public float CA;

        public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();

        public Vertex(float x, float y, float z, float cX, float cY, float cZ, float cA)
        {
            X = x;
            Y = y;
            Z = z;
            CX = cX;
            CY = cY;
            CZ = cZ;
            CA = cA;
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            var window = new OpenTKWindow(800, 600, "Final Engine")
            {
                Visible = true
            };

            if (DXGI.CreateDXGIFactory(out IDXGIFactory factory).Failure)
            {
                throw new Exception("Failed to create DXGI Factory.");
            }

            Console.WriteLine(Vertex.SizeInBytes);

            if (D3D11.D3D11CreateDevice(null,
                                        DriverType.Hardware,
                                        DeviceCreationFlags.None,
                                        null,
                                        out ID3D11Device device,
                                        out ID3D11DeviceContext deviceContext).Failure)
            {
                throw new Exception("Failed to create Direct3D 11 device.");
            }

            var swapChainDescription = new SwapChainDescription()
            {
                BufferCount = 1,
                BufferDescription = new ModeDescription(window.Width, window.Height),
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                OutputWindow = window.WindowInfo.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            IDXGISwapChain swapChain = factory.CreateSwapChain(device, swapChainDescription);

            // Set up default render target, otherwise we can't draw to the screen
            ID3D11Texture2D backBuffer = swapChain.GetBuffer<ID3D11Texture2D>(0);
            ID3D11RenderTargetView defaultTarget = device.CreateRenderTargetView(backBuffer);
            backBuffer.Release();

            // Bind the color attachment "framebuffer"
            deviceContext.OMSetRenderTargets(defaultTarget);

            deviceContext.RSSetViewport(0, 0, window.Width, window.Height);

            if (Compiler.CompileFromFile("vertex.hlsl", "VShader", "vs_4_0", out Blob vsBlob, out Blob vsError).Failure)
            {
                Console.WriteLine(vsError.ConvertToString());
            }

            if (Compiler.CompileFromFile("fragment.hlsl", "PShader", "ps_4_0", out Blob fsBlob, out Blob fsError).Failure)
            {
                Console.WriteLine(fsError.ConvertToString());
            }

            ID3D11VertexShader vertexShader = device.CreateVertexShader(vsBlob.BufferPointer, vsBlob.BufferSize);
            ID3D11PixelShader fragmentShader = device.CreatePixelShader(fsBlob.BufferPointer, fsBlob.BufferSize);

            deviceContext.VSSetShader(vertexShader);
            deviceContext.PSSetShader(fragmentShader);

            InputElementDescription[] inputElements =
            {
                new InputElementDescription("POSITION", 0, Format.R32G32B32_Float, 0, 0, InputClassification.PerVertexData, 0),
                new InputElementDescription("COLOR", 0, Format.R32G32B32_Float, 12, 0, InputClassification.PerVertexData, 0)
            };

            ID3D11InputLayout inputLayout = device.CreateInputLayout(inputElements, vsBlob);
            deviceContext.IASetInputLayout(inputLayout);

            Vertex[] vertices =
            {
                new Vertex(0, 0.5f, 0, 1, 0, 0, 1.0f),
                new Vertex(0.5f, -0.5f, 0, 0, 1, 0, 1.0f),
                new Vertex(-0.5f, -0.5f, 0, 0, 0, 1, 1.0f)
            };

            var inputAssembler = new Direct3D11InputAssembler(deviceContext);
            var resourceFactory = new Direct3D11GPUResourceFactory(device);

            IBuffer buffer = resourceFactory.CreateBuffer(BufferType.VertexBuffer, vertices, vertices.Length * Vertex.SizeInBytes, Vertex.SizeInBytes);

            inputAssembler.SetPrimitiveTopology(PrimitiveTopology.Triangle);
            inputAssembler.SetBuffer(buffer);

            while (!window.IsClosing)
            {
                deviceContext.ClearRenderTargetView(defaultTarget, new Color4(1.0f, 1.0f, 0.0f, 1.0f));
                deviceContext.Draw(3, 0);
                swapChain.Present(0, PresentFlags.None);
                window.ProcessEvents();
            }

            buffer.Dispose();

            fragmentShader.Release();
            vertexShader.Release();
            defaultTarget.Release();
            swapChain.Release();
            deviceContext.Release();
            device.Release();
            factory.Release();
            window.Dispose();
        }
    }
}