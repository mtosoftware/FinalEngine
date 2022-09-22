// <copyright file="SceneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

#define VSYNC_ENABLED

namespace FinalEngine.Editor.ViewModels.Docking.Panes
{
    using System;
    using System.Windows.Input;
    using FinalEngine.Editor.Common.Services.Factories;
    using FinalEngine.Editor.Common.Services.Scenes;
    using FinalEngine.Launching;
    using Microsoft.Toolkit.Mvvm.Input;

    public class SceneViewModel : PaneViewModelBase, ISceneViewModel
    {
        private readonly ISceneRenderer sceneRenderer;

        private IGameTime gameTime;

        private int projectionHeight;

        private int projectionWidth;

        private ICommand? renderCommand;

        public SceneViewModel(ISceneRenderer sceneRenderer, IGameTimeFactory gameTimeFactory)
        {
            if (gameTimeFactory == null)
            {
                throw new ArgumentNullException(nameof(gameTimeFactory));
            }

            this.sceneRenderer = sceneRenderer ?? throw new ArgumentNullException(nameof(sceneRenderer));
            this.gameTime = gameTimeFactory.CreateGameTime();

            this.Title = "Scene View";
            this.ContentID = "SceneViewPane";
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
#if !VSYNC_ENABLED
            while (!this.gameTime.CanProcessNextFrame())
            {
                continue;
            }
#endif
            this.sceneRenderer.Render();
        }
    }
}