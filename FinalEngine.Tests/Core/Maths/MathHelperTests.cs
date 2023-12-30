// <copyright file="MathHelperTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Maths;

using System;
using FinalEngine.Maths;
using NUnit.Framework;

[TestFixture]
public class MathHelperTests
{
    [Test]
    public void DegreesToRadiansShouldReturnPiWhenInvoked()
    {
        // Arrange
        float degrees = 180.0f;

        // Act
        float actual = MathHelper.DegreesToRadians(degrees);

        // Assert
        Assert.That(actual, Is.EqualTo((float)Math.PI));
    }

    [Test]
    public void RadiansToDegreesShouldReturnOneEightyWhenInvoked()
    {
        // Arrange
        float radians = (float)Math.PI;

        // Act
        float actual = MathHelper.RadiansToDegrees(radians);

        // Assert
        Assert.That(actual, Is.EqualTo(180.0f));
    }
}
