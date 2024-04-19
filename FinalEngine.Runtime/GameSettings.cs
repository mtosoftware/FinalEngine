// <copyright file="GameSettings.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using FinalEngine.Platform;

public sealed class GameSettings
{
    private PlatformSettings? platformSettings;

    public double FrameCap { get; set; } = 120.0d;

    public PlatformSettings PlatformSettings
    {
        get { return this.platformSettings ??= new PlatformSettings(); }
        set { this.platformSettings = value; }
    }
}
