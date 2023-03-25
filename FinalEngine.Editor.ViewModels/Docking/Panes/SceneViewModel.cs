// <copyright file="SceneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Rendering;

public partial class SceneViewModel : PaneViewModelBase, ISceneViewModel
{
    private readonly ISceneRenderer sceneRenderer;

    private int projectionHeight;

    private int projectionWidth;

    private ICommand? renderCommand;

    public SceneViewModel(ISceneRenderer sceneRenderer)
    {
        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));

        this.Title = "Scene View";
    }

    public int ProjectionHeight
    {
        get
        {
            return this.projectionHeight;
        }

        set
        {
            this.SetProperty(ref this.projectionHeight, value);
            this.sceneRenderer.ChangeProjection(this.ProjectionWidth, this.ProjectionHeight);
        }
    }

    public int ProjectionWidth
    {
        get
        {
            return this.projectionWidth;
        }

        set
        {
            this.SetProperty(ref this.projectionWidth, value);
            this.sceneRenderer.ChangeProjection(this.ProjectionWidth, this.ProjectionHeight);
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
