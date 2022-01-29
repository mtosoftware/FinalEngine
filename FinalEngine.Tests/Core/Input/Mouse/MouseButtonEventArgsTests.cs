// <copyright file="MouseButtonEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Mouse
{
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Input;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class MouseButtonEventArgsTests
    {
        [Test]
        public void ButtonShouldReturnSameAsInputWhenSet()
        {
            // Arrange
            const MouseButton Expected = MouseButton.Button6;

            // Act
            var eventArgs = new MouseButtonEventArgs()
            {
                Button = Expected,
            };

            // Assert
            Assert.AreEqual(Expected, eventArgs.Button);
        }
    }
}