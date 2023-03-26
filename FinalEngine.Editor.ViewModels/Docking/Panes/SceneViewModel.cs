// <copyright file="SceneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using System;
using System.Drawing;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Rendering;

/// <summary>
/// Represents a standard implementation of an <see cref="ISceneViewModel"/>.
/// </summary>
/// <seealso cref="PaneViewModelBase" />
/// <seealso cref="ISceneViewModel" />
public partial class SceneViewModel : PaneViewModelBase, ISceneViewModel
{
    /// <summary>
    /// The scene renderer.
    /// </summary>
    private readonly ISceneRenderer sceneRenderer;

    /// <summary>
    /// The projection size.
    /// </summary>
    private Size projectionSize;

    /// <summary>
    /// The render command.
    /// </summary>
    private ICommand? renderCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneViewModel"/> class.
    /// </summary>
    /// <param name="sceneRenderer">
    /// The scene renderer.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="sceneRenderer"/> parameter cannot be null.
    /// </exception>
    public SceneViewModel(ISceneRenderer sceneRenderer)
    {
        this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));

        this.Title = "Scene View";
    }

    /// <summary>
    /// Gets or sets the size of the projection.
    /// </summary>
    /// <value>
    /// The size of the projection.
    /// </value>
    public Size ProjectionSize
    {
        get
        {
            return this.projectionSize;
        }

        set
        {
            this.SetProperty(ref this.projectionSize, value);
            this.sceneRenderer.ChangeProjection(this.projectionSize.Width, this.projectionSize.Height);
        }
    }

    /// <summary>
    /// Gets the render command.
    /// </summary>
    /// <value>
    /// The render command.
    /// </value>
    public ICommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand(this.Render); }
    }

    /// <summary>
    /// Renders the scene to the view.
    /// </summary>
    private void Render()
    {
        this.sceneRenderer.Render();
    }
}
