// <copyright file="ServiceLocator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Services;

using System;

internal static class ServiceLocator
{
    public static IServiceProvider Provider { get; private set; }

    internal static void SetServiceProvider(IServiceProvider provider)
    {
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }
}
