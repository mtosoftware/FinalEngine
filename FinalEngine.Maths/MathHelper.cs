// <copyright file="MathHelper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Maths;

using System;

/// <summary>
/// Provides helpful math functions.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Converts the specified <paramref name="angle"/> (in degrees) to radians.
    /// </summary>
    /// <param name="angle">
    /// The angle (in degrees) to convert to radians.
    /// </param>
    /// <returns>
    /// The specified <paramref name="angle"/>, converted to radians.
    /// </returns>
    public static float DegreesToRadians(float angle)
    {
        return (float)Math.PI / 180.0f * angle;
    }

    /// <summary>
    /// Converts the specified <paramref name="radians"/> to degrees.
    /// </summary>
    /// <param name="radians">
    /// The radians to convert to degrees.
    /// </param>
    /// <returns>
    /// The specified <paramref name="radians"/>, converted to degrees.
    /// </returns>
    public static float RadiansToDegrees(float radians)
    {
        return 180.0f / (float)Math.PI * radians;
    }
}
