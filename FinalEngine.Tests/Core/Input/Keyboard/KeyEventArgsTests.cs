// <copyright file="KeyEventArgsTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Keyboard;

using FinalEngine.Input.Keyboards;
using NUnit.Framework;

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
        const Key expected = Key.CapsLock;

        // Act
        var keyEventArgs = new KeyEventArgs()
        {
            Key = expected,
        };

        // Assert
        Assert.AreEqual(expected, keyEventArgs.Key);
    }

    [Test]
    public void ModifiersShouldReturnSameAsInputWhenSet()
    {
        // Arrange
        const KeyModifiers expected = KeyModifiers.CapsLock | KeyModifiers.Control;

        // Act
        var keyEventArgs = new KeyEventArgs()
        {
            Modifiers = expected,
        };

        // Assert
        Assert.AreEqual(expected, keyEventArgs.Modifiers);
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
