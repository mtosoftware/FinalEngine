namespace FinalEngine.Rendering.Renderers;
using System.Numerics;
using FinalEngine.Rendering.Core;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;
using FinalEngine.Rendering.Textures;
using SixLabors.ImageSharp;

public class SkyboxRenderer : ISkyboxRenderer
{
    private readonly IRenderDevice renderDevice;
    private IShaderProgram? skyboxProgram;
    private readonly IInputLayout skyboxLayout;
    private readonly IVertexBuffer skyboxVertexBuffer;
    private readonly IIndexBuffer skyboxIndexBuffer;

    private readonly float[] skyboxVertices =
    {
        // Top
        -1.0f, 1.0f, -1.0f,
        1.0f, 1.0f, -1.0f,
        1.0f, 1.0f, 1.0f,
        -1.0f, 1.0f, 1.0f,
        // Bottom                                                             
        -1.0f, -1.0f, 1.0f,
        1.0f, -1.0f, 1.0f,
        1.0f, -1.0f, -1.0f,
        -1.0f, -1.0f, -1.0f,
        // Left                                                               
        -1.0f, 1.0f, -1.0f,
        -1.0f, 1.0f, 1.0f,
        -1.0f, -1.0f, 1.0f,
        -1.0f, -1.0f, -1.0f,
        // Right                                                              
        1.0f, 1.0f, 1.0f,
        1.0f, 1.0f, -1.0f,
        1.0f, -1.0f, -1.0f,
        1.0f, -1.0f, 1.0f,
        // Back                                                               
        1.0f, 1.0f, -1.0f,
        -1.0f, 1.0f, -1.0f,
        -1.0f, -1.0f, -1.0f,
        1.0f, -1.0f, -1.0f,
        // Front                                                              
        -1.0f, 1.0f, 1.0f,
        1.0f, 1.0f, 1.0f,
        1.0f, -1.0f, 1.0f,
        -1.0f, -1.0f, 1.0f,
    };

    private readonly uint[] skyboxIndices =
    {
        0, 1, 2, 0, 2, 3,
        4, 5, 6, 4, 6, 7,
        8, 9, 10, 8, 10, 11,
        12, 13, 14, 12, 14, 15,
        16, 17, 18, 16, 18, 19,
        20, 21, 22, 20, 22, 23, };

    private IShaderProgram SkyboxProgram
    {
        get
        {
            return this.skyboxProgram ??=
                ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\skybox.fesp");
        }
    }
    public SkyboxRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice;

        this.skyboxLayout =
            this.renderDevice.Factory.CreateInputLayout(new InputElement[]
            {
                new InputElement(0, 3, InputElementType.Float, 0)
            });

        this.skyboxVertexBuffer = this.renderDevice.Factory.CreateVertexBuffer(BufferUsageType.Static,
            this.skyboxVertices,
            this.skyboxVertices.Length * sizeof(float), 3 * sizeof(float));
        this.skyboxIndexBuffer = this.renderDevice.Factory.CreateIndexBuffer(BufferUsageType.Static, this.skyboxIndices,
            this.skyboxIndices.Length * sizeof(uint));
    }

    public void Render(ICubeTexture texture, ICamera camera)
    {
        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            WriteEnabled = true,
            ReadEnabled = true,
            ComparisonMode = ComparisonMode.LessEqual
        });
        this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
        {
            CullEnabled = true,
            CullMode = FaceCullMode.Back,
            WindingDirection = WindingDirection.CounterClockwise,
            MultiSamplingEnabled = true,
        });
        this.renderDevice.Pipeline.SetShaderProgram(this.SkyboxProgram);
        this.renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);

        var view = camera.View; // remove translation from the view matrix
        Matrix4x4.Decompose(view, out var scale, out var rotation, out _);
        var viewNoTranslation = Matrix4x4.CreateScale(scale) * Matrix4x4.CreateFromQuaternion(rotation);
        this.renderDevice.Pipeline.SetUniform("u_view", viewNoTranslation);
        this.renderDevice.Pipeline.SetTexture(texture, 0);
        this.renderDevice.InputAssembler.SetInputLayout(this.skyboxLayout);
        this.renderDevice.InputAssembler.SetVertexBuffer(this.skyboxVertexBuffer);
        this.renderDevice.InputAssembler.SetIndexBuffer(this.skyboxIndexBuffer);
        this.renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, this.skyboxIndices.Length);
    }
}
