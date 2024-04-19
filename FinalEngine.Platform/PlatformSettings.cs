// <copyright file="PlatformSettings.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System.Drawing;

public sealed class PlatformSettings
{
    public Size ClientSize { get; set; } = new Size(1280, 720);

    public string Title { get; set; } = "Game";
}
