// <copyright file="SceneViewDocumentViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Documents
{
    using System;
    using System.Drawing;
    using FinalEngine.Editor.Views.Documents;
    using FinalEngine.Editor.Views.Events;
    using FinalEngine.Rendering;

    public class SceneViewDocumentViewModel : ViewModelBase
    {
        private readonly IRenderDevice renderDevice;

        private readonly ISceneViewDocumentView view;

        public SceneViewDocumentViewModel(ISceneViewDocumentView view, IRenderDevice renderDevice)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

            this.view.OnLoaded += this.View_OnLoaded;
            this.view.OnResized += this.View_OnResized;
            this.view.OnRender += this.View_OnRender;
        }

        private void View_OnLoaded(object? sender, EventArgs e)
        {
        }

        private void View_OnRender(object? sender, EventArgs e)
        {
            this.renderDevice.Clear(Color.CornflowerBlue);
        }

        private void View_OnResized(object? sender, SizeChangedEventArgs e)
        {
            this.renderDevice.Rasterizer.SetViewport(e.ClientRectangle);
        }
    }
}
