// <copyright file="MathHelper.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Maths;

using System;

/// <summary>
/// Provides utility methods for mathematical operations and conversions.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Converts an angle in degrees to radians.
    /// </summary>
    ///
    /// <param name="angle">
    /// Specifies a <see cref="float"/> that represents the angle in degrees to be converted to radians.
    /// </param>
    ///
    /// <example>
    /// The following code demonstrates how to use the <see cref="DegreesToRadians(float)"/> method:
    ///
    /// <code>
    /// float degrees = 45.0f;
    /// float radians = MathHelper.DegreesToRadians(degrees);
    ///
    /// // Output: 0.7853982
    /// Console.WriteLine($"Equivalent angle in radians: {radians}");
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns the equivalent specified <paramref name="angle"/> in radians.
    /// </returns>
    public static float DegreesToRadians(float angle)
    {
        return (float)Math.PI / 180.0f * angle;
    }

    /// <summary>
    /// Converts an angle in radians to degrees.
    /// </summary>
    ///
    /// <param name="angle">
    /// Specifies a <see cref="float"/> that represents the angle in radians to be converted to degrees.
    /// </param>
    ///
    /// <example>
    /// The following code demonstrates how to use the <see cref="RadiansToDegrees(float)"/> method:
    ///
    /// <code>
    /// float radians = 1.5708f; // 90 degrees in radians
    /// float degrees = MathHelper.RadiansToDegrees(radians);
    ///
    /// // Output: 90
    /// Console.WriteLine($"Equivalent angle in degrees: {degrees}");
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns the equivalent specified <paramref name="angle"/> in degrees.
    /// </returns>
    public static float RadiansToDegrees(float angle)
    {
        return 180.0f / (float)Math.PI * angle;
    }
}
