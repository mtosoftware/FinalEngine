// <copyright file="OpenTKKeyboardDeviceTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Platform.Desktop.OpenTK;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Platform.Desktop.OpenTK;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using Moq;
using NUnit.Framework;
using TKKeyboardKeyEventArgs = global::OpenTK.Windowing.Common.KeyboardKeyEventArgs;
using TKKeys = global::OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using TKModifiers = global::OpenTK.Windowing.GraphicsLibraryFramework.KeyModifiers;

public class OpenTKKeyboardDeviceTests
{
    private OpenTKKeyboardDevice keyboardDevice;

    private Mock<INativeWindowInvoker> nativeWindow;

    [Test]
    public void ConstructorShouldHookOntoNativeWindowKeyDownEventWhenNativeWindowIsNotNull()
    {
        // Assert
        this.nativeWindow.VerifyAdd(
            x =>
        {
            x.KeyDown += It.IsAny<Action<TKKeyboardKeyEventArgs>>();
        }, Times.Once);
    }

    [Test]
    public void ConstructorShouldHookOntoNativeWindowKeyUpEventWhenNativeWindowIsNotNull()
    {
        // Assert
        this.nativeWindow.VerifyAdd(
            x =>
        {
            x.KeyUp += It.IsAny<Action<TKKeyboardKeyEventArgs>>();
        }, Times.Once);
    }

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenNativeWindowIsNotNull()
    {
        // Arrange, act and assert
        Assert.DoesNotThrow(() =>
        {
            new OpenTKKeyboardDevice(new Mock<INativeWindowInvoker>().Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenNativeWindowIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenTKKeyboardDevice(null);
        });
    }

    [Test]
    public void NativeWindowKeyDownEventShouldRaiseKeyDownEventWhenRaised()
    {
        // Assert
        this.keyboardDevice.KeyDown += (s, e) =>
        {
            Assert.AreSame(s, this.keyboardDevice);
            Assert.AreEqual(Key.A, e.Key);
            Assert.AreEqual(KeyModifiers.Alt, e.Modifiers);
        };

        // Act
        this.nativeWindow.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new TKKeyboardKeyEventArgs(TKKeys.A, 0, TKModifiers.Alt, false));
    }

    [Test]
    public void NativeWindowKeyDownShouldNotRaiseKeyDownWhenRaisedAndNoSubscribers()
    {
        // Act
        this.nativeWindow.Raise(
            x =>
        {
            x.KeyDown += null;
        }, default(TKKeyboardKeyEventArgs));

        // Assert
        Assert.Pass();
    }

    [Test]
    public void NativeWindowKeyUpEventShouldRaiseKeyUpEventWhenRaised()
    {
        // Assert
        this.keyboardDevice.KeyUp += (s, e) =>
        {
            Assert.AreEqual(s, this.keyboardDevice);
            Assert.AreEqual(Key.A, e.Key);
            Assert.AreEqual(KeyModifiers.Alt, e.Modifiers);
        };

        // Act
        this.nativeWindow.Raise(
            x =>
        {
            x.KeyUp += null;
        }, new TKKeyboardKeyEventArgs(TKKeys.A, 0, TKModifiers.Alt, false));
    }

    [Test]
    public void NativeWindowKeyUpShouldNotRaiseKeyUpWhenRaisedAndNoSubscribers()
    {
        // Act
        this.nativeWindow.Raise(
            x =>
        {
            x.KeyUp += null;
        }, default(TKKeyboardKeyEventArgs));

        // Assert
        Assert.Pass();
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.nativeWindow = new Mock<INativeWindowInvoker>();
        this.keyboardDevice = new OpenTKKeyboardDevice(this.nativeWindow.Object);
    }
}
