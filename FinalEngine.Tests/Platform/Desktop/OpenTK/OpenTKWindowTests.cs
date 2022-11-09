// <copyright file="OpenTKWindowTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Platform.Desktop.OpenTK
{
    using System;
    using System.Drawing;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using global::OpenTK.Mathematics;
    using Moq;
    using NUnit.Framework;

    public class OpenTKWindowTests
    {
        private Mock<INativeWindowInvoker> nativeWindow;

        private OpenTKWindow window;

        [Test]
        public void ClientSizeShouldInvokeNativeWindowClientSizePropertyWhenInvoked()
        {
            // Act
            _ = this.window.ClientSize;

            // Assert
            this.nativeWindow.VerifyGet(x => x.ClientSize, Times.Exactly(2));
        }

        [Test]
        public void ClientSizeShouldReturnSameAsNativeWindowClientSizeWhenInvoked()
        {
            // Arrange
            var expected = new Size(10, 34);

            this.nativeWindow.SetupGet(x => x.ClientSize).Returns(new Vector2i(expected.Width, expected.Height));

            // Act
            var actual = this.window.ClientSize;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CloseShouldInvokeNativeWindowCloseWhenNativeWindowIsNotDisposed()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsDisposed).Returns(false);

            // Act
            this.window.Close();

            // Assert
            this.nativeWindow.Verify(x => x.Close(), Times.Once);
        }

        [Test]
        public void CloseShouldThrowObjectDisposedExceptionWhenNativeWindowIsDisposed()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsDisposed).Returns(true);

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.window.Close();
            });
        }

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenNativeWindowIsNotNull()
        {
            // Arrange, act and assert
            Assert.DoesNotThrow(() =>
            {
                new OpenTKWindow(new Mock<INativeWindowInvoker>().Object);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenNativeWindowIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OpenTKWindow(null);
            });
        }

        [Test]
        public void DisposeShouldInvokeNativeWindowDisposeWhenNotDisposed()
        {
            // Arrange
            this.nativeWindow.Setup(x => x.IsDisposed).Returns(false);

            // Act
            this.window.Dispose();

            // Assert
            this.nativeWindow.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void IsExitingShouldInvokeNativeWindowIsExitingWhenRead()
        {
            // Act
            _ = this.window.IsExiting;

            // Assert
            this.nativeWindow.VerifyGet(x => x.IsExiting, Times.Once);
        }

        [Test]
        public void IsExitingShouldReturnSameAsNativeWindowIsExitingWhenRead()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsExiting).Returns(true);

            // Act
            bool actual = this.window.IsExiting;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsFocusedShouldReturnFalseWhenNativeWindowIsFocusedSetToFalse()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsFocused).Returns(false);

            // Act
            bool actual = this.window.IsFocused;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsFocusedShouldReturnTrueWhenNativeWindowIsFocusedSetToTrue()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsFocused).Returns(true);

            // Act
            bool actual = this.window.IsFocused;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ProcessEventsShouldInvokeProcessInputEventsWhenInvoked()
        {
            // Act
            this.window.ProcessEvents();

            // Assert
            this.nativeWindow.Verify(x => x.ProcessInputEvents(), Times.Once);
        }

        [Test]
        public void ProcessEventsShouldInvokeProcessWindowEventsWhenInvoked()
        {
            // Act
            this.window.ProcessEvents();

            // Assert
            this.nativeWindow.Verify(x => x.ProcessWindowEvents(false), Times.Once);
        }

        [Test]
        public void ProcessEventsShouldThrowObjectDisposedExceptionWhenNativeWindowIsDisposed()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsDisposed).Returns(true);

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.window.ProcessEvents();
            });
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.nativeWindow = new Mock<INativeWindowInvoker>();
            this.window = new OpenTKWindow(this.nativeWindow.Object);
        }

        [Test]
        public void SizeShouldInvokeNativeWindowSizePropertyWhenInvoked()
        {
            // Act
            _ = this.window.Size;

            // Assert
            this.nativeWindow.VerifyGet(x => x.Size, Times.Exactly(2));
        }

        [Test]
        public void SizeShouldInvokeNativeWindowSizeWhenSet()
        {
            // Arrange
            var expected = new Size(2423, 1243);

            // Act
            this.window.Size = expected;

            // Assert
            this.nativeWindow.VerifySet((x) =>
            {
                x.Size = new Vector2i(expected.Width, expected.Height);
            });
        }

        [Test]
        public void SizeShouldReturnSameAsNativeWindowSizePropertyWhenInvoked()
        {
            // Arrange
            var expected = new Size(543, 124);

            this.nativeWindow.SetupGet(x => x.Size).Returns(new Vector2i(expected.Width, expected.Height));

            // Act
            var actual = this.window.Size;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void TearDown()
        {
            this.window.Dispose();
        }

        [Test]
        public void TitleGetShouldInvokeNativeWindowTitleWhenRead()
        {
            // Act
            _ = this.window.Title;

            // Assert
            this.nativeWindow.VerifyGet(x => x.Title, Times.Once);
        }

        [Test]
        public void TitleSetShouldSetNativeWindowTitleWhenNativeWindowIsNotDisposed()
        {
            // Arrange
            const string expected = "Testing";
            this.nativeWindow.SetupGet(x => x.IsDisposed).Returns(false);

            // Act
            this.window.Title = expected;

            // Assert
            this.nativeWindow.VerifySet(
                x =>
            {
                x.Title = expected;
            }, Times.Once);
        }

        [Test]
        public void TitleSetShouldThrowObjectDisposedExceptionWhenNativeWindowIsDisposed()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsDisposed).Returns(true);

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.window.Title = null;
            });
        }

        [Test]
        public void VisibleGetShouldInvokeNativeWindowIsVisibleWhenRead()
        {
            // Act
            _ = this.window.Visible;

            // Assert
            this.nativeWindow.VerifyGet(x => x.IsVisible, Times.Once);
        }

        [Test]
        public void VisibleSetShouldSetNativeWindowIsVisibleWhenNativeWindowIsNotDisposed()
        {
            // Arrange
            const bool expected = true;
            this.nativeWindow.SetupGet(x => x.IsDisposed).Returns(false);

            // Act
            this.window.Visible = expected;

            // Assert
            this.nativeWindow.VerifySet(
                x =>
            {
                x.IsVisible = expected;
            }, Times.Once);
        }

        [Test]
        public void VisibleSetShouldThrowObjectDisposedExceptionWhenNativeWindowIsDisposed()
        {
            // Arrange
            this.nativeWindow.SetupGet(x => x.IsDisposed).Returns(true);

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() =>
            {
                this.window.Visible = false;
            });
        }
    }
}
