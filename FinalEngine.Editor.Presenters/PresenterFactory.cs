// <copyright file="PresenterFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Presenters
{
    using System;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Presenters.Documents;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.Views;
    using FinalEngine.Editor.Views.Documents;
    using FinalEngine.Editor.Views.Interactions;
    using FinalEngine.Rendering;

    public class PresenterFactory : IPresenterFactory
    {
        private readonly IApplicationContext applicationContext;

        private readonly IEntityWorld entityWorld;

        private readonly IRenderDevice renderDevice;

        private readonly IResourceLoaderRegistrar resourceLoaderRegistrar;

        public PresenterFactory(
            IApplicationContext applicationContext,
            IRenderDevice renderDevice,
            IResourceLoaderRegistrar resourceLoaderRegistrar,
            IEntityWorld entityWorld)
        {
            this.applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
            this.resourceLoaderRegistrar = resourceLoaderRegistrar ?? throw new ArgumentNullException(nameof(resourceLoaderRegistrar));
            this.entityWorld = entityWorld ?? throw new ArgumentNullException(nameof(entityWorld));
        }

        public MainPresenter CreateMainPresenter(IMainView mainView)
        {
            if (mainView == null)
            {
                throw new ArgumentNullException(nameof(mainView));
            }

            return new MainPresenter(mainView, this.applicationContext, this.resourceLoaderRegistrar);
        }

        public SceneViewDocumentPresenter CreateSceneViewDocumentPresenter(ISceneViewDocumentView sceneViewDocumentView)
        {
            if (sceneViewDocumentView == null)
            {
                throw new ArgumentNullException(nameof(sceneViewDocumentView));
            }

            return new SceneViewDocumentPresenter(sceneViewDocumentView, this.entityWorld, this.renderDevice);
        }
    }
}
