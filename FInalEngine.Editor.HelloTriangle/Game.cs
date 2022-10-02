namespace FInalEngine.Examples.HelloTriangle
{
    using System.Drawing;
    using System.IO;
    using FinalEngine.Input;
    using FinalEngine.IO;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Pipeline;

    public class Game : GameContainer
    {
        private IShader fragmentShader;

        private IIndexBuffer indexBuffer;

        private IInputLayout inputLayout;

        private IShaderProgram shaderProgram;

        private IVertexBuffer vertexBuffer;

        private IShader vertexShader;

        public Game()
        {
            this.vertexShader = this.RenderDevice.Factory.CreateShader(PipelineTarget.Vertex, this.ReadTextFile("Resources\\Shaders\\vertexShader.vert"));
            this.fragmentShader = this.RenderDevice.Factory.CreateShader(PipelineTarget.Fragment, this.ReadTextFile("Resources\\Shaders\\fragmentShader.frag"));
            this.shaderProgram = this.RenderDevice.Factory.CreateShaderProgram(new[] { this.vertexShader, this.fragmentShader });

            this.inputLayout = this.RenderDevice.Factory.CreateInputLayout(new InputElement[]
            {
                new InputElement(0, 3, InputElementType.Float, 0),
                new InputElement(1, 4, InputElementType.Float, 3 * sizeof(float)),
            });

            // (coordinates) x, y, z, (color) r, g, b, a
            float[] vertices =
            {
                -1, -1, 0, 1, 0, 0, 1,
                1, -1, 0, 0, 1, 0, 1,
                0, 1, 0, 0, 0, 1, 1,
            };

            int[] indices =
            {
                0, 1, 2,
            };

            this.vertexBuffer = this.RenderDevice.Factory.CreateVertexBuffer(BufferUsageType.Static, vertices, vertices.Length * sizeof(float), 7 * sizeof(float));
            this.indexBuffer = this.RenderDevice.Factory.CreateIndexBuffer(BufferUsageType.Static, indices, indices.Length * sizeof(int));
        }

        protected override void Render()
        {
            this.RenderDevice.InputAssembler.SetVertexBuffer(vertexBuffer);
            this.RenderDevice.InputAssembler.SetIndexBuffer(indexBuffer);
            this.RenderDevice.InputAssembler.SetInputLayout(inputLayout);

            this.RenderDevice.Pipeline.SetShaderProgram(shaderProgram);

            this.RenderDevice.Clear(Color.CornflowerBlue);
            this.RenderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, 3);
            base.Render();
        }

        protected override void Update()
        {
            if (this.Keyboard.IsKeyReleased(Key.Escape))
            {
                Exit();
            }

            base.Update();
        }

        private string ReadTextFile(string filePath)
        {
            using (var stream = this.FileSystem.OpenFile(filePath, FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}