// <copyright file="ApplicationContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;
using System.Reflection;

public sealed class ApplicationContext : IApplicationContext
{
    public string Title
    {
        get { return $"Final Engine - {this.Version}"; }
    }

    public Version Version
    {
        get { return Assembly.GetExecutingAssembly().GetName().Version ?? new Version(DateTime.UtcNow.Year, 0); }
    }
}
