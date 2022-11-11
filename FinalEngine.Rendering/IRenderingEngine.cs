// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;

    public enum RenderStage
    {
        Geometry,
    }

    public interface IRenderingEngine : IDisposable
    {
        void AddRenderAction(RenderStage stage, Action render);

        void RemoveRenderAction(RenderStage stage);

        void Render();
    }
}
