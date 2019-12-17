namespace FinalEngine.Rendering.OpenGL.Invokers
{
    using OpenTK.Graphics.OpenGL;

    public sealed class OpenGLInvoker : IOpenGLInvoker
    {
        public void CullFace(CullFaceMode mode)
        {
            GL.CullFace(mode);
        }

        public void Disable(EnableCap cap)
        {
            GL.Disable(cap);
        }

        public void Enable(EnableCap cap)
        {
            GL.Enable(cap);
        }

        public void FrontFace(FrontFaceDirection mode)
        {
            GL.FrontFace(mode);
        }

        public void PolygonMode(MaterialFace face, PolygonMode mode)
        {
            GL.PolygonMode(face, mode);
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