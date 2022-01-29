// <copyright file="KeyEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Keyboard
{
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Input;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class KeyEventArgsTests
    {
        [Test]
        public void AltShouldReturnTrueWhenModifierHasAltFlag()
        {
            // Arrange
            var keyEventArgs = new KeyEventArgs()
            {
                Modifiers = KeyModifiers.Alt | KeyModifiers.CapsLock,
            };

            // Act
            bool actual = keyEventArgs.Alt;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void CapsLockShouldReturnTrueWhenModifierHasCapsLockFlag()
        {
            // Arrange
            var keyEventArgs = new KeyEventArgs()
            {
                Modifiers = KeyModifiers.Alt | KeyModifiers.CapsLock,
            };

            // Act
            bool actual = keyEventArgs.CapsLock;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ControlShouldReturnTrueWhenModifierHasControlFlag()
        {
            // Arrange
            var keyEventArgs = new KeyEventArgs()
            {
                Modifiers = KeyModifiers.Control | KeyModifiers.NumLock,
            };

            // Act
            bool actual = keyEventArgs.Control;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void KeyShouldReturnSameAsInputWhenSet()
        {
            // Arrange
            const Key Expected = Key.CapsLock;

            // Act
            var keyEventArgs = new KeyEventArgs()
            {
                Key = Expected,
            };

            // Assert
            Assert.AreEqual(Expected, keyEventArgs.Key);
        }

        [Test]
        public void ModifiersShouldReturnSameAsInputWhenSet()
        {
            // Arrange
            const KeyModifiers Expected = KeyModifiers.CapsLock | KeyModifiers.Control;

            // Act
            var keyEventArgs = new KeyEventArgs()
            {
                Modifiers = Expected,
            };

            // Assert
            Assert.AreEqual(Expected, keyEventArgs.Modifiers);
        }

        [Test]
        public void NumLockShouldReturnTrueWhenModifierHasNumLockFlag()
        {
            // Arrange
            var keyEventArgs = new KeyEventArgs()
            {
                Modifiers = KeyModifiers.Control | KeyModifiers.NumLock,
            };

            // Act
            bool actual = keyEventArgs.NumLock;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ShiftShouldReturnTrueWhenModifierHasShiftFlag()
        {
            // Arrange
            var keyEventArgs = new KeyEventArgs()
            {
                Modifiers = KeyModifiers.Shift | KeyModifiers.Super,
            };

            // Act
            bool actual = keyEventArgs.Shift;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void SuperShouldReturnTrueWhenModifierHasSuperFlag()
        {
            // Arrange
            var keyEventArgs = new KeyEventArgs()
            {
                Modifiers = KeyModifiers.Shift | KeyModifiers.Super,
            };

            // Act
            bool actual = keyEventArgs.Super;

            // Assert
            Assert.True(actual);
        }
    }
}