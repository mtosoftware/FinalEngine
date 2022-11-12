// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using FinalEngine.Rendering.Lighting;

    public interface IRenderingEngine : IDisposable
    {
        void Enqueue(LightBase light);

        void Enqueue(GeometryData data);

        void Render(CameraData camera);
    }
}
