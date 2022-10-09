// <copyright file="ViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using FinalEngine.Editor.Services.Resources;
    using FinalEngine.Editor.ViewModels.Documents;
    using FinalEngine.Editor.ViewModels.Tools;
    using FinalEngine.Editor.Views;
    using FinalEngine.Editor.Views.Documents;
    using FinalEngine.Editor.Views.Interactions;
    using FinalEngine.Editor.Views.Tools;
    using FinalEngine.Rendering;

    public class ViewModelFactory
    {
        private readonly IApplicationContext application;

        private readonly IResourceLoaderRegistrar registrar;

        private readonly IRenderDevice renderDevice;

        public ViewModelFactory(
            IApplicationContext application,
            IResourceLoaderRegistrar registrar,
            IRenderDevice renderDevice)
        {
            this.application = application ?? throw new ArgumentNullException(nameof(application));
            this.registrar = registrar ?? throw new ArgumentNullException(nameof(registrar));
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        }

        public MainViewModel Create(IMainView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new MainViewModel(view, this.application, this.registrar);
        }

        public SceneViewDocumentViewModel Create(ISceneViewDocumentView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new SceneViewDocumentViewModel(view, this.renderDevice);
        }

        public SceneHierarchyToolWindowViewModel Create(ISceneHierarchyToolWindowView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new SceneHierarchyToolWindowViewModel(view);
        }
    }
}
