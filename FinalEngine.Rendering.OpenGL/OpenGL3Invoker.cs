namespace FinalEngine.Rendering.OpenGL
{
    using OpenTK.Graphics.OpenGL;

    public sealed class OpenGL3Invoker : IOpenGLInvoker
    {
        public void SetRasterState(RasterStateDescription description)
        {
            if (description.CullEnabled)
            {
                GL.Enable(EnableCap.CullFace);
            }
            else
            {
                GL.Disable(EnableCap.CullFace);
            }

            if (description.ScissorEnabled)
            {
                GL.Enable(EnableCap.ScissorTest);
            }
            else
            {
                GL.Disable(EnableCap.ScissorTest);
            }

            GL.CullFace(description.CullFaceType == CullFaceType.Front ? CullFaceMode.Front : CullFaceMode.Back);
            GL.FrontFace(description.WindingDirection == WindingDirection.Clockwise ? FrontFaceDirection.Cw : FrontFaceDirection.Ccw);
            GL.PolygonMode(MaterialFace.FrontAndBack, description.FillMode == RasterMode.Fill ? PolygonMode.Fill : PolygonMode.Line);
        }

        public void SetScissor(int x, int y, int width, int height)
        {
            GL.Viewport(x, y, width, height);
        }

        public void SetViewport(int x, int y, int width, int height)
        {
            GL.Scissor(x, y, width, height);
        }
    }
}