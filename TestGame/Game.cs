// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;

    public class Game : GameContainer
    {
        private const float CameraSpeed = 4.0f;

        private float camX;

        private float camY;

        private ISpriteDrawer? drawer;

        private float rotation;

        private ITexture2D? textureA;

        private ITexture2D? textureB;

        public Game()
        {
            this.Window!.Title = "My Cool Game";

            // Load some textures.
            this.textureA = this.TextureLoader.LoadTexture("Resources\\Textures\\default.png");
            this.textureB = this.TextureLoader.LoadTexture("Resources\\Textures\\wood.png");

            // Create a sprite drawer.
            var binder = new TextureBinder(this.RenderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler, 10000);
            this.drawer = new SpriteDrawer(this.RenderDevice, batcher, binder, this.Window.ClientSize.Width, this.Window.ClientSize.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.drawer != null)
                {
                    ((IDisposable)this.drawer)?.Dispose();
                    this.drawer = null;
                }

                if (this.textureB != null)
                {
                    this.textureB.Dispose();
                    this.textureB = null;
                }

                if (this.textureA != null)
                {
                    this.textureA.Dispose();
                    this.textureA = null;
                }
            }

            base.Dispose(disposing);
        }

        protected override void Render(GameTimeInfo gameTime)
        {
            this.RenderDevice.Clear(Color.CornflowerBlue);

            this.drawer!.Transform = Matrix4x4.CreateTranslation(this.camX, this.camY, 0);

            this.drawer?.Begin();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    ITexture2D? tex = (j + i) % 2 == 0 ? this.textureA : this.textureB;

                    this.drawer?.Draw(tex!, Color.White, new Vector2(128, 128), new Vector2(i * 256, j * 256), this.rotation, new Vector2(256, 256));
                }
            }

            this.drawer?.End();

            base.Render(gameTime);
        }

        protected override void Update(GameTimeInfo gameTime)
        {
            if (this.Keyboard.IsKeyDown(Key.Escape))
            {
                this.Exit();
            }

            this.rotation += 0.001f * (float)gameTime.Delta;

            if (this.Keyboard.IsKeyDown(Key.W))
            {
                this.camY -= CameraSpeed;
            }
            else if (this.Keyboard.IsKeyDown(Key.S))
            {
                this.camY += CameraSpeed;
            }
            else if (this.Keyboard.IsKeyDown(Key.A))
            {
                this.camX += CameraSpeed;
            }
            else if (this.Keyboard.IsKeyDown(Key.D))
            {
                this.camX -= CameraSpeed;
            }

            this.Window!.Title = $"{gameTime.FrameRate} : {gameTime.Delta}";

            base.Update(gameTime);
        }
    }
}