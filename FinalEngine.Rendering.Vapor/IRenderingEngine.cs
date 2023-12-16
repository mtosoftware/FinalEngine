// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor;

using System;
using FinalEngine.Rendering.Vapor.Core;
using FinalEngine.Rendering.Vapor.Data;

public interface IRenderingEngine
{
    ICamera? Camera { get; set; }

    void AddModel(Model model);

    void RemoveModel(Guid entityId);

    void Render();
}
