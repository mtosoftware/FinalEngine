// <copyright file="Game.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example;

using System.Drawing;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Systems;
using FinalEngine.Rendering.Textures;
using FinalEngine.Runtime;

public sealed class Game : GameContainerBase
{
    public override void Initialize()
    {
        this.World.AddSystem<SpriteRenderEntitySystem>();

        var entity = new Entity();

        entity.AddComponent<TransformComponent>();
        entity.AddComponent(new SpriteComponent()
        {
            Color = Color.RebeccaPurple,
            Origin = Vector2.Zero,
            Texture = this.ResourceManager.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png"),
        });

        this.World.AddEntity(entity);

        base.Initialize();
    }
}
