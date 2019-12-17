namespace FinalEngine.Rendering.OpenGL
{
    using System;
    using OpenTK.Graphics.OpenGL;

    public sealed class OpenGLRasterizer : IRasterizer
    {
        private readonly IOpenGLInvoker invoker;

        public OpenGLRasterizer(IOpenGLInvoker invoker)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified { nameof(invoker) } parameter is null.");
        }

        public void SetRasterState(RasterStateDescription description)
        {
            if (description.CullEnabled)
            {
                invoker.Enable(EnableCap.CullFace);
            }
            else
            {
                invoker.Disable(EnableCap.CullFace);
            }

            invoker.CullFace(description.CullFaceType == CullFaceType.Front ? CullFaceMode.Front : CullFaceMode.Back);
            invoker.FrontFace(description.WindingDirection == WindingDirection.Clockwise ? FrontFaceDirection.Cw : FrontFaceDirection.Ccw);
            invoker.PolygonMode(MaterialFace.FrontAndBack, description.FillMode == RasterMode.Fill ? PolygonMode.Fill : PolygonMode.Line);
        }

        public void SetScissor(int x, int y, int width, int height)
        {
            invoker.SetScissor(x, y, width, height);
        }

        public void SetViewport(int x, int y, int width, int height)
        {
            invoker.SetViewport(x, y, width, height);
        }
    }
}