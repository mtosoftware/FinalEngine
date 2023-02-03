// <copyright file="MouseScrollEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Mouse;

using FinalEngine.Input.Mouses;
using NUnit.Framework;

public class MouseScrollEventArgsTests
{
    [Test]
    public void OffsetShouldReturnSameAsInputWhenSet()
    {
        // Arrange
        const double expected = 124.0d;

        // Act
        var eventArgs = new MouseScrollEventArgs()
        {
            Offset = expected,
        };

        // Assert
        Assert.AreEqual(expected, eventArgs.Offset);
    }
}
