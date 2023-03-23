// <copyright file="AssemblyExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities.Extensions;

using System;
using System.Reflection;

/// <summary>
/// Provides extension methods for <see cref="Assembly"/>.
/// </summary>
public static class AssemblyExtensions
{
    /// <summary>
    /// Gets the <see cref="Version"/> of this <see cref="Assembly"/> as a <c>string</c>, or <see cref="string.Empty"/> if one is not found.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>
    /// The <see cref="Version"/> as a <c>string</c> or <see cref="string.Empty"/> if one is not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="assembly"/> parameter cannot be null.
    /// </exception>
    public static string GetVersionString(this Assembly assembly)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        return assembly.GetName().Version?.ToString() ?? string.Empty;
    }
}
