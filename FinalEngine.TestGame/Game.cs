// <copyright file="Game.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.TestGame;

using System.Drawing;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Resources;
using FinalEngine.Runtime;

internal sealed class Game : GameContainerBase
{
    public Game(
        IWindow window,
        IKeyboard keyboard,
        IMouse mouse,
        IRenderDevice renderDevice,
        IResourceManager resourceManager,
        IResourceLoaderFetcher fetcher)
        : base(window, keyboard, mouse, renderDevice, resourceManager, fetcher)
    {
    }

    public override void Render(float delta)
    {
        this.RenderDevice.Clear(Color.CornflowerBlue);
        base.Render(delta);
    }

    public override void Update(float delta)
    {
        if (this.Keyboard.IsKeyReleased(Key.Escape))
        {
            this.Exit();
        }

        base.Update(delta);
    }
}
