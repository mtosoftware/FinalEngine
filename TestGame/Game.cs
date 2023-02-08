namespace TestGame;

using FinalEngine.Extensions.Resources.Invocation;
using FinalEngine.Extensions.Resources.Loaders.Textures;
using FinalEngine.GUI;
using FinalEngine.GUI.Panels;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Textures;
using FinalEngine.Runtime.Desktop;

public class Game : GameBase
{
    private readonly ISpriteDrawer drawer;

    private Panel panel;

    public Game()
    {
        var binder = new TextureBinder(this.RenderDevice.Pipeline);
        var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);

        this.drawer = new SpriteDrawer(
            this.RenderDevice,
            batcher,
            binder,
            this.DisplayManager.DisplayWidth,
            this.DisplayManager.DisplayHeight);
    }

    protected override void LoadResources()
    {
        this.ResourceManager.RegisterLoader(new Texture2DResourceLoader(this.FileSystem, this.RenderDevice.Factory, new ImageInvoker()));

        this.panel = new Panel
        {
            Anchor = Anchor.Center,
            Texture = this.ResourceManager.LoadResource<ITexture2D>("panel.png"),
        };

        base.LoadResources();
    }

    protected override void Render()
    {
        this.drawer.Begin();
        this.panel.Draw(this.drawer, this.RenderDevice.Rasterizer);

        this.drawer.End();
        base.Render();
    }
}
