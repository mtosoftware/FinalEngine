// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;
using FinalEngine.Rendering.Geometry;

public interface IRenderingEngine
{
    Color ClearColor { get; }

    void Render(ICamera camera);
}
