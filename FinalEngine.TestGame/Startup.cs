// <copyright file="Startup.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.TestGame;

using System;
using System.Drawing;
using FinalEngine.Platform;
using FinalEngine.Runtime.Extensions;
using Microsoft.Extensions.DependencyInjection;

internal sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddRuntime<Game>(x =>
        {
            x.FrameCap = 60.0d;
            x.PlatformSettings = new PlatformSettings()
            {
                ClientSize = new Size(1280, 720),
                Title = "My Game",
            };
        });
    }
}
