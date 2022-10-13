namespace TestProject
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Resources;
    using FinalEngine.Runtime;
    using FinalEngine.Runtime.Settings;

    public class Game : GameContainerBase
    {
        private ITexture2D colorAttachment;

        private ISpriteDrawer drawer;

        private IRenderTarget2D renderTarget;

        private ITexture2D texture;

        public Game(GameSettings settings, IRuntimeFactory factory)
            : base(settings, factory)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void Initialize()
        {
            var binder = new TextureBinder(this.RenderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);
            this.drawer = new SpriteDrawer(this.RenderDevice, batcher, binder, 1280, 720);

            this.texture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png");

            this.renderTarget = this.RenderDevice.Factory.CreateRenderTarget2D();

            this.colorAttachment = this.RenderDevice.Factory.CreateTexture2D<IntPtr>(
                new Texture2DDescription()
                {
                    Width = 1280,
                    Height = 720,
                    MinFilter = TextureFilterMode.Linear,
                    MagFilter = TextureFilterMode.Linear,
                    PixelType = PixelType.Byte,
                    WrapS = TextureWrapMode.Repeat,
                    WrapT = TextureWrapMode.Repeat,
                },
                null);

            this.renderTarget.AttachTexture(RenderTargetAttachmentType.Color, this.colorAttachment);

            if (!this.renderTarget.IsComplete(RenderTargetBindType.Both))
            {
                throw new ArgumentNullException();
            }

            base.Initialize();
        }

        protected override void Render()
        {
            this.RenderDevice.OutputMerger.SetRenderTarget(RenderTargetBindType.Both, this.renderTarget);
            this.drawer.Begin();
            this.drawer.Draw(
                this.texture,
                Color.White,
                Vector2.Zero,
                Vector2.Zero,
                0,
                new Vector2(this.texture.Description.Width, this.texture.Description.Height));
            this.drawer.End();

            this.RenderDevice.OutputMerger.SetRenderTarget(RenderTargetBindType.Both, null);

            this.drawer.Begin();
            this.drawer.Draw(
                this.colorAttachment,
                Color.White, Vector2.Zero,
                Vector2.Zero,
                0,
                new Vector2(1280, 720));
            this.drawer.End();

            base.Render();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
