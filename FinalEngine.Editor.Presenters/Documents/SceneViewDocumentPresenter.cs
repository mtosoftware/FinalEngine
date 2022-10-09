// <copyright file="SceneViewDocumentPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Presenters.Documents
{
    using System;
    using System.Drawing;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Views.Documents;
    using FinalEngine.Editor.Views.Events;
    using FinalEngine.Rendering;

    public class SceneViewDocumentPresenter
    {
        private readonly IEntityWorld entityWorld;

        private readonly IRenderDevice renderDevice;

        private readonly ISceneViewDocumentView sceneViewDocumentView;

        public SceneViewDocumentPresenter(
            ISceneViewDocumentView sceneViewDocumentView,
            IEntityWorld entityWorld,
            IRenderDevice renderDevice)
        {
            this.sceneViewDocumentView = sceneViewDocumentView ?? throw new ArgumentNullException(nameof(sceneViewDocumentView));
            this.entityWorld = entityWorld ?? throw new ArgumentNullException(nameof(entityWorld));
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

            this.sceneViewDocumentView.OnResized += this.SceneViewDocumentView_OnResized;
            this.sceneViewDocumentView.OnRender += this.SceneViewDocumentView_OnRender;
        }

        private void SceneViewDocumentView_OnRender(object? sender, EventArgs e)
        {
            this.renderDevice.Clear(Color.CadetBlue);
            this.entityWorld.ProcessAll(GameLoopType.Render);
        }

        private void SceneViewDocumentView_OnResized(object? sender, SizeChangedEventArgs e)
        {
            this.renderDevice.Rasterizer.SetViewport(e.ClientRectangle);
        }
    }
}
