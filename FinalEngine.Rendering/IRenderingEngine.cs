// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using FinalEngine.Rendering.Data;
    using FinalEngine.Rendering.Settings;

    public interface IRenderingEngine : IDisposable
    {
        RenderQualitySettings RenderQualitySettings { get; }

        TextureQualitySettings TextureQualitySettings { get; }

        void Initialize();

        void Render(CameraData camera);
    }
}
