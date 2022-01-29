// <copyright file="MouseScrollEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Mouse
{
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Input;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class MouseScrollEventArgsTests
    {
        [Test]
        public void OffsetShouldReturnSameAsInputWhenSet()
        {
            // Arrange
            const double Expected = 124.0d;

            // Act
            var eventArgs = new MouseScrollEventArgs()
            {
                Offset = Expected,
            };

            // Assert
            Assert.AreEqual(Expected, eventArgs.Offset);
        }
    }
}