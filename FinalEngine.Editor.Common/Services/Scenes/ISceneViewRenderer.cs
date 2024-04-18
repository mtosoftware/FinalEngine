// <copyright file="IViewRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

public interface ISceneViewRenderer
{
    void AdjustView(int width, int height);

    void Render();
}
