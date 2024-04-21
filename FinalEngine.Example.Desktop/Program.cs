// <copyright file="Program.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example.Desktop;

using FinalEngine.Runtime;
using Microsoft.Extensions.DependencyInjection;

internal static class Program
{
    internal static void Main()
    {
        var startup = new Startup();
        var services = new ServiceCollection();

        startup.ConfigureServices(services);

        services.BuildServiceProvider().GetRequiredService<IEngineDriver>().Start();
    }
}
