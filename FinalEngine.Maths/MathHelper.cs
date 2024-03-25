// <copyright file="MathHelper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Maths;

using System;

/// <summary>
/// Provides helper methods for mathematical operations.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Converts the specified <paramref name="angle"/> from degrees to radians.
    /// </summary>
    ///
    /// <param name="angle">
    /// The angle in degrees.
    /// </param>
    ///
    /// <returns>
    /// The equivalent angle in radians.
    /// </returns>
    ///
    /// <remarks>
    /// This method is useful for converting angles from the more commonly used degrees
    /// measurement to radians, which is often required by trigonometric functions.
    /// </remarks>
    ///
    /// <example>
    /// The example below will convert 45 degrees to radians and then print the result.
    ///
    /// <code>
    /// // Convert 45 degrees to radians
    /// float degrees = 45f;
    /// float radians = MathHelper.DegreesToRadians(degrees);
    ///
    /// Console.WriteLine($"45 degrees in radians is {radians}");
    /// </code>
    /// </example>
    public static float DegreesToRadians(float angle)
    {
        return (float)Math.PI / 180.0f * angle;
    }

    /// <summary>
    /// Converts the specified <paramref name="angle"/> from radians to degrees.
    /// </summary>
    ///
    /// <param name="angle">
    /// The angle in radians.
    /// </param>
    ///
    /// <returns>
    /// The equivalent angle in degrees.
    /// </returns>
    ///
    /// <remarks>
    /// This method is useful for converting angles from radians back to degrees,
    /// which can be more intuitive for human interpretation.
    /// </remarks>
    ///
    /// <example>
    /// The below example  will convert π/4 from radians to degrees.
    ///
    /// <code>
    /// float radians = Math.PI / 4f;
    /// float degrees = MathHelper.RadiansToDegrees(radians);
    ///
    /// Console.WriteLine($"π/4 radians in degrees is {degrees}");
    /// </code>
    /// </example>
    public static float RadiansToDegrees(float angle)
    {
        return 180.0f / (float)Math.PI * angle;
    }
}
