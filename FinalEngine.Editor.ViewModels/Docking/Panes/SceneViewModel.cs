﻿// <copyright file="SceneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes
{
    using System;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Services.Scenes;
    using Microsoft.Toolkit.Mvvm.Input;

    public class SceneViewModel : PaneViewModelBase, ISceneViewModel
    {
        private readonly ISceneRenderer sceneRenderer;

        private ICommand? renderCommand;

        public SceneViewModel(ISceneRenderer sceneRenderer)
        {
            this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));

            this.Title = "Scene View";
            this.ContentID = "SceneViewPane";
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
}