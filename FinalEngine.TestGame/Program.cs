// <copyright file="Program.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.TestGame;

using FinalEngine.Runtime;
using FinalEngine.Runtime.Extensions;
using Microsoft.Extensions.DependencyInjection;

internal static class Program
{
    internal static void Main()
    {
        var services = new ServiceCollection();
        services.AddRuntime<Game>();

        services.BuildServiceProvider().GetRequiredService<IEngineDriver>().Start();
    }
}
