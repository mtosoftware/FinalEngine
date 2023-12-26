namespace FinalEngine.Rendering.OpenGL.Buffers;
using FinalEngine.Rendering.Buffers;

public interface IOpenGLFrameBuffer : IFrameBuffer
{
    void Bind();
    void UnBind();
}

