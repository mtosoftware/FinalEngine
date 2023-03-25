// <copyright file="SceneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using System;
using System.Drawing;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Rendering;

public partial class SceneViewModel : PaneViewModelBase, ISceneViewModel
{
    private readonly ISceneRenderer sceneRenderer;

    private ICommand? renderCommand;

    private Size projectionSize;

    public SceneViewModel(ISceneRenderer sceneRenderer)
    {
        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));

        this.Title = "Scene View";
    }

    //// TODO: Hook this up to GLWpfControl.RenderSize, waiting on: https://github.com/opentk/GLWpfControl/issues/108.
    public Size ProjectionSize
    {
        get
        {
            return this.projectionSize;
        }

        private set
        {
            this.SetProperty(ref this.projectionSize, value);
            this.sceneRenderer.ChangeProjection(this.projectionSize.Width, this.projectionSize.Height);
        }
    }

    public ICommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand(this.Render); }
    }

    private void Render()
    {
        this.sceneRenderer.Render();
    }
}
