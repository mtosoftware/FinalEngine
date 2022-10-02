namespace FinalEngine.Examples.HelloSpriteBatch
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;

    public class Game : GameContainer
    {
        private ISpriteBatcher spriteBatcher;

        private ISpriteDrawer spriteDrawer;

        private ITexture2D texture;

        private ITextureBinder textureBinder;

        public Game()
        {
            this.spriteBatcher = new SpriteBatcher(this.RenderDevice.InputAssembler);
            this.textureBinder = new TextureBinder(this.RenderDevice.Pipeline);
            this.spriteDrawer = new SpriteDrawer(this.RenderDevice, spriteBatcher, textureBinder, this.Window.ClientSize.Width, this.Window.ClientSize.Height);

            texture = this.ResourceManager.LoadResource<ITexture2D>("Resources\\Textures\\player.png");
        }

        protected override void Render()
        {
            this.RenderDevice.Clear(Color.CornflowerBlue);

            this.spriteDrawer.Begin();

            this.spriteDrawer.Draw(
                texture,
                Color.Red,
                Vector2.Zero,
                Vector2.Zero,
                0,
                new Vector2(256, 256));

            this.spriteDrawer.End();

            base.Render();
        }
    }
}