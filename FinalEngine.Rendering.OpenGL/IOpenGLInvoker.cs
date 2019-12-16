namespace FinalEngine.Rendering.OpenGL
{
    using OpenTK.Graphics.OpenGL;

    public interface IOpenGLInvoker
    {
        void CullFace(CullFaceMode mode);

        void Disable(EnableCap cap);

        void Enable(EnableCap cap);

        void FrontFace(FrontFaceDirection mode);

        void PolygonMode(MaterialFace face, PolygonMode mode);

        void SetScissor(int x, int y, int width, int height);

        void SetViewport(int x, int y, int width, int height);
    }
}