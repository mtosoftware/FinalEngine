// <copyright file="MouseMoveEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Mouse
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using FinalEngine.Input;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class MouseMoveEventArgsTests
    {
        [Test]
        public void LocationShouldReturnSameAsInputWhenSet()
        {
            // Arrange
            var expected = new PointF(100, 200);

            // Act
            var eventArgs = new MouseMoveEventArgs()
            {
                Location = expected,
            };

            // Assert
            Assert.AreEqual(expected, eventArgs.Location);
        }
    }
}