namespace FinalEngine.Rendering.Renderers;

using FinalEngine.Rendering.Core;
using Textures;

public interface ISkyboxRenderer
{
    void Render(ITextureCube texture, ICamera camera);
}
