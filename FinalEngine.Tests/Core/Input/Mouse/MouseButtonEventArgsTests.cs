// <copyright file="MouseButtonEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Mouse
{
    using FinalEngine.Input;
    using FinalEngine.Input.Mouses;
    using NUnit.Framework;

    public class MouseButtonEventArgsTests
    {
        [Test]
        public void ButtonShouldReturnSameAsInputWhenSet()
        {
            // Arrange
            const MouseButton expected = MouseButton.Button6;

            // Act
            var eventArgs = new MouseButtonEventArgs()
            {
                Button = expected,
            };

            // Assert
            Assert.AreEqual(expected, eventArgs.Button);
        }
    }
}
