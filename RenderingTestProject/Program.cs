namespace RenderingAPI
{
    using System;
    using System.Numerics;
    using System.Runtime.InteropServices;
    using FinalEngine.Platform.Desktop;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Direct3D.Invokers;
    using Vortice.D3DCompiler;
    using Vortice.Direct3D;
    using Vortice.Direct3D11;
    using Vortice.DXGI;
    using Vortice.Mathematics;
    using Usage = Vortice.DXGI.Usage;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position { get; set; }

        public Vector3 Color { get; set; }

        public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();

        public Vertex(Vector3 position, Vector3 color)
        {
            Position = position;
            Color = color;
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

            var deviceInvoker = new D3D11DeviceInvoker(device);
            var contextInvoker = new D3D11DeviceContextInvoker(deviceContext);

            var rasterizer = new Direct3DRasterizer(deviceInvoker, contextInvoker);

            rasterizer.SetRasterState(RasterStateDescription.Default);
            rasterizer.SetViewport(0, 0, window.Width, window.Height);

            // Start IShaderCopmiler
            if (Compiler.CompileFromFile("vertex.hlsl", "VShader", "vs_5_0", out Blob vsBlob, out Blob vsError).Failure)
            {
                Console.WriteLine(vsError.ConvertToString());
            }

            if (Compiler.CompileFromFile("fragment.hlsl", "PShader", "ps_5_0", out Blob fsBlob, out Blob fsError).Failure)
            {
                Console.WriteLine(fsError.ConvertToString());
            }

            ID3D11VertexShader vertexShader = device.CreateVertexShader(vsBlob.BufferPointer, vsBlob.BufferSize);
            ID3D11PixelShader fragmentShader = device.CreatePixelShader(fsBlob.BufferPointer, fsBlob.BufferSize);
            // End IShaderCompiler

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
                new Vertex(new Vector3(-1, -1, 0), new Vector3(1, 0, 0)),
                new Vertex(new Vector3(1, -1, 0), new Vector3(0, 1, 0)),
                new Vertex(new Vector3(0, 1, 0), new Vector3(0, 0, 1))
            };

            ID3D11Buffer vertexBuffer = device.CreateBuffer(vertices, new BufferDescription()
            {
                Usage = Vortice.Direct3D11.Usage.Dynamic,
                SizeInBytes = Vertex.SizeInBytes * vertices.Length,
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
            });

            deviceContext.IASetVertexBuffers(0, 1, new ID3D11Buffer[] { vertexBuffer }, new int[] { Vertex.SizeInBytes }, new int[] { 0 });
            deviceContext.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);

            while (!window.IsClosing)
            {
                deviceContext.ClearRenderTargetView(defaultTarget, new Color4(0, 0, 0, 1));
                deviceContext.Draw(3, 0);
                swapChain.Present(0, PresentFlags.None);
                window.ProcessEvents();
            }

            vertexBuffer.Release();
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