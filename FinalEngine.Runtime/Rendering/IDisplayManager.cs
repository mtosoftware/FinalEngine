// <copyright file="IDisplayManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Rendering;

public interface IDisplayManager
{
    int DisplayHeight { get; }

    int DisplayWidth { get; }

    void ChangeResolution(DisplayResolution resolution);
}
