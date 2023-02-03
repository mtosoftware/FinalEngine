// <copyright file="GameContainerMock.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Runtime.Mocks;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.IO;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Resources;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Rendering;

public class GameContainerMock : GameContainerBase
{
    public GameContainerMock(IRuntimeFactory factory)
        : base(factory)
    {
    }

    public IDisplayManager DisplayManagerObject
    {
        get { return this.DisplayManager; }
    }

    public IFileSystem FileSystemObject
    {
        get { return this.FileSystem; }
    }

    public Action InitializeActin { get; set; }

    public bool IsRunningBoolean
    {
        get { return this.IsRunning; }
    }

    public IKeyboard KeyboardObject
    {
        get { return this.Keyboard; }
    }

    public Action LoadResourcesAction { get; set; }

    public IMouse MouseObject
    {
        get { return this.Mouse; }
    }

    public Action RenderAction { get; set; }

    public int RenderCount { get; set; }

    public IRenderDevice RenderDeviceObject
    {
        get { return this.RenderDevice; }
    }

    public IResourceManager ResourceManagerObject
    {
        get { return this.ResourceManager; }
    }

    public Action UpdateAction { get; set; }

    public int UpdateCount { get; set; }

    public IWindow WindowObject
    {
        get { return this.Window; }
    }

    protected override void Initialize()
    {
        this.InitializeActin?.Invoke();
        base.Initialize();
    }

    protected override void LoadResources()
    {
        this.LoadResourcesAction?.Invoke();
        base.LoadResources();
    }

    protected override void Render()
    {
        this.RenderCount++;
        this.RenderAction?.Invoke();
        base.Render();
    }

    protected override void Update()
    {
        this.UpdateCount++;
        this.UpdateAction?.Invoke();
        base.Update();
    }
}
