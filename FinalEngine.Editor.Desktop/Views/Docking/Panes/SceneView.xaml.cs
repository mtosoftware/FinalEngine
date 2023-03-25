// <copyright file="SceneView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking.Panes;

using System;
using System.Windows;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;

/// <summary>
/// Interaction logic for SceneView.xaml.
/// </summary>
public partial class SceneView : GLWpfControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SceneView"/> class.
    /// </summary>
    public SceneView()
    {
        this.InitializeComponent();

        this.Start(new GLWpfControlSettings()
        {
            MajorVersion = 4,
            MinorVersion = 5,
            GraphicsContextFlags = ContextFlags.ForwardCompatible,
            GraphicsProfile = ContextProfile.Core,
            RenderContinuously = true,
        });

        this.Render += this.SceneView_Render;
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo info)
    {
        // I hate to do this because I'm violating MVVM but GLWpfControl doesn't use dependency properties.
        if (this.DataContext is not ISceneViewModel viewModel)
        {
            throw new InvalidOperationException($"The current {nameof(this.DataContext)} is not of type {nameof(ISceneViewModel)}.");
        }

        viewModel.ProjectionWidth = (int)info.NewSize.Width;
        viewModel.ProjectionHeight = (int)info.NewSize.Height;

        base.OnRenderSizeChanged(info);
    }

    //// TODO: Add issue to GLWpfControl repo to support commands.
    private void SceneView_Render(System.TimeSpan obj)
    {
        GL.Finish();
    }
}
