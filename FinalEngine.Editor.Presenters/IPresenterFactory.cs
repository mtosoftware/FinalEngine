// <copyright file="IPresenterFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Presenters
{
    using FinalEngine.Editor.Presenters.Documents;
    using FinalEngine.Editor.Views;
    using FinalEngine.Editor.Views.Documents;

    public interface IPresenterFactory
    {
        MainPresenter CreateMainPresenter(IMainView mainView);

        SceneViewDocumentPresenter CreateSceneViewDocumentPresenter(ISceneViewDocumentView sceneViewDocumentView);
    }
}
