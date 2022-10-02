// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors
{
    using System;
    using System.Drawing;
    using FinalEngine.ECS;
    using FinalEngine.Examples.StarWarriors.Components;
    using FinalEngine.Examples.StarWarriors.Systems;
    using FinalEngine.Examples.StarWarriors.Templates;
    using FinalEngine.Input;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;

    public sealed class Game : GameContainer
    {
        private IEntityTemplate enemyTemplate;

        private IEntityWorld entityWorld;

        private ISpriteBatcher spriteBatcher;

        private ISpriteDrawer spriteDrawer;

        private ITextureBinder textureBinder;

        public Game()
        {
            this.Window.Title = "Star Warriors";

            this.spriteBatcher = new SpriteBatcher(this.RenderDevice.InputAssembler);
            this.textureBinder = new TextureBinder(this.RenderDevice.Pipeline);

            this.spriteDrawer = new SpriteDrawer(
                this.RenderDevice,
                this.spriteBatcher,
                this.textureBinder,
                this.Window.ClientSize.Width,
                this.Window.ClientSize.Height);

            var missileTemplate = new MissileEntityTemplate(this.ResourceManager.LoadResource<ITexture2D>("Resources\\Textures\\bullet.png"));
            this.enemyTemplate = new EnemyShipEntityTemplate(this.ResourceManager.LoadResource<ITexture2D>("Resources\\Textures\\enemy.png"));

            this.entityWorld = new EntityWorld();

            this.entityWorld.AddSystem(new SpriteRenderSystem(this.spriteDrawer));
            this.entityWorld.AddSystem(new MovementSystem());
            this.entityWorld.AddSystem(new ExpirationSystem(this.entityWorld));
            this.entityWorld.AddSystem(new EnemyMovementSystem(this.Window));
            this.entityWorld.AddSystem(new EnemyShooterSystem(missileTemplate, this.entityWorld));
            this.entityWorld.AddSystem(new EnemySpawnSystem(this.enemyTemplate, this.entityWorld, this.Window));
            this.entityWorld.AddSystem(new CollisionSystem(this.entityWorld));
            this.entityWorld.AddSystem(
                new PlayerControllerSystem(
                    this.Keyboard,
                    this.Window,
                    missileTemplate,
                    this.entityWorld));

            this.InitializePlayer();
            this.InitializeEnemyShips();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.spriteDrawer != null)
            {
                (this.spriteDrawer as IDisposable)?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void Render()
        {
            this.RenderDevice.Clear(Color.Black);
            this.entityWorld.ProcessAll(GameLoopType.Render);
            base.Render();
        }

        protected override void Update()
        {
            if (this.Keyboard.IsKeyReleased(Key.Escape))
            {
                this.Exit();
            }

            this.entityWorld.ProcessAll(GameLoopType.Update);
            base.Update();
        }

        private void InitializeEnemyShips()
        {
            var random = new Random();

            for (int index = 0; index < 2; ++index)
            {
                Entity entity = this.enemyTemplate.CreateEntity();

                entity.GetComponent<TransformComponent>().X = random.Next(this.Window.ClientSize.Width - 100) + 50;
                entity.GetComponent<TransformComponent>().Y = random.Next((int)((this.Window.ClientSize.Height * 0.75) + 0.5)) + 50;
                entity.GetComponent<VelocityComponent>().Speed = 0.05f;
                entity.GetComponent<VelocityComponent>().Angle = random.Next() % 2 == 0 ? 0 : 180;

                this.entityWorld.AddEntity(entity);
            }
        }

        private void InitializePlayer()
        {
            var entity = new Entity();

            entity.AddComponent(new TagComponent()
            {
                Tag = "Player",
            });

            entity.AddComponent(new TransformComponent()
            {
                X = (this.Window.ClientSize.Width * 0.5f) - 16,
                Y = this.Window.ClientSize.Height - 50,
            });
            entity.AddComponent(new SpriteComponent()
            {
                Texture = this.ResourceManager.LoadResource<ITexture2D>("Resources\\Textures\\player.png"),
            });

            entity.AddComponent(new HealthComponent()
            {
                Points = 30,
            });

            this.entityWorld.AddEntity(entity);
        }
    }
}