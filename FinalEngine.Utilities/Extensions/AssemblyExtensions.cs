// <copyright file="AssemblyExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities.Extensions;

using System;
using System.Reflection;

public static class AssemblyExtensions
{
    public static string GetVersionString(this Assembly assembly)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        return assembly.GetName().Version?.ToString() ?? string.Empty;
    }
}
