// <copyright file="MouseTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Mouse
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using FinalEngine.Input;
    using Moq;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class MouseTests
    {
        private Mouse mouse;

        private Mock<IMouseDevice> mouseDevice;

        [Test]
        public void DeltaShouldReturnDeviceLocationDeltaWhenDeviceIsNotNull()
        {
            // Act
            PointF actual = this.mouse.Delta;

            // Assert
            this.mouseDevice.VerifyGet(x => x.LocationDelta, Times.Once);
        }

        [Test]
        public void DeltaShouldReturnPointEmptyWhenDeviceIsNull()
        {
            // Arrange
            var mouse = new Mouse(null);
            PointF expected = Point.Empty;

            // Act
            PointF actual = mouse.Delta;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeviceButtonDownShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.mouseDevice.Raise(x => x.ButtonDown += null, new object[] { new object(), null }));
        }

        [Test]
        public void DeviceButtonUpShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.mouseDevice.Raise(x => x.ButtonUp += null, new object[] { new object(), null }));
        }

        [Test]
        public void DeviceMoveShouldSetLocationWhenDeviceMoveRaised()
        {
            // Arrange
            var point = new PointF(100, 400);

            this.mouseDevice.Raise(x => x.Move += null, new MouseMoveEventArgs()
            {
                Location = point,
            });

            // Act
            PointF actual = this.mouse.Location;

            // Assert
            Assert.AreEqual(point, actual);
        }

        [Test]
        public void DeviceMoveShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.mouseDevice.Raise(x => x.Move += null, new object[] { new object(), null }));
        }

        [Test]
        public void DeviceScrollShouldSetWheelOffsetWhenDeviceScrollRaised()
        {
            // Arrange
            const double Expected = 132.0d;

            this.mouseDevice.Raise(x => x.Scroll += null, new MouseScrollEventArgs()
            {
                Offset = Expected,
            });

            // Act
            double actual = this.mouse.WheelOffset;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void DeviceScrollShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.mouseDevice.Raise(x => x.Scroll += null, new object[] { new object(), null }));
        }

        [Test]
        public void IsButtonDownShouldReturnFalseWhenButtonNotDownDuringCurrentFrame()
        {
            // Act
            bool actual = this.mouse.IsButtonDown(MouseButton.Button1);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsButtonDownShouldReturnTrueWhenButtonDownDuringCurrentFrame()
        {
            // Arrange
            const MouseButton Button = MouseButton.Button3;

            this.mouseDevice.Raise(x => x.ButtonDown += null, new MouseButtonEventArgs()
            {
                Button = Button,
            });

            // Act
            bool actual = this.mouse.IsButtonDown(Button);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsButtonPressedShouldReturnFalseWhenButtonIsNotDownDuringCurrentFrame()
        {
            // Arrange
            const MouseButton Button = MouseButton.Right;

            this.mouseDevice.Raise(x => x.ButtonDown += null, new MouseButtonEventArgs()
            {
                Button = Button,
            });

            this.mouse.Update();

            this.mouseDevice.Raise(x => x.ButtonUp += null, new MouseButtonEventArgs()
            {
                Button = Button,
            });

            // Act
            bool actual = this.mouse.IsButtonPressed(Button);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsButtonPressedShouldReturnTrueWhenButtonDownDuringCurrentFrameAndNotDownDuringPreviousFrame()
        {
            // Arrange
            const MouseButton Button = MouseButton.Button6;

            this.mouseDevice.Raise(x => x.ButtonDown += null, new MouseButtonEventArgs()
            {
                Button = Button,
            });

            // Act
            bool actual = this.mouse.IsButtonPressed(Button);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsButtonReleasedShouldReturnFalseWhenButtonDownDuringCurrentFrameAndNotDownDuringPreviousFrame()
        {
            // Arrange
            const MouseButton Button = MouseButton.Middle;

            this.mouseDevice.Raise(x => x.ButtonDown += null, new MouseButtonEventArgs()
            {
                Button = Button,
            });

            // Act
            bool actual = this.mouse.IsButtonReleased(Button);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsButtonReleasedShouldReturnTrueWhenButtonNotDownDuringCurrentFrameAndButtonDownDuringPreviousFrame()
        {
            // Arrange
            const MouseButton Button = MouseButton.Left;

            this.mouseDevice.Raise(x => x.ButtonDown += null, new MouseButtonEventArgs()
            {
                Button = Button,
            });

            this.mouse.Update();

            this.mouseDevice.Raise(x => x.ButtonUp += null, new MouseButtonEventArgs()
            {
                Button = Button,
            });

            // Act
            bool actual = this.mouse.IsButtonReleased(Button);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void LocationGetShouldReturnEmptyPointWhenNothingChanged()
        {
            // Act
            PointF actual = this.mouse.Location;

            // Assert
            Assert.AreEqual(PointF.Empty, actual);
        }

        [Test]
        public void LocationSetShouldInvokeDeviceSetCursorLocationWhenValueIsNotEqualToLocation()
        {
            // Arrange
            var point = new PointF(100, 200);

            // Act
            this.mouse.Location = point;

            // Assert
            this.mouseDevice.Verify(x => x.SetCursorLocation(point), Times.Once);
        }

        [Test]
        public void LocationSetShouldNotInvokeSetCursorLocationWhenValueIsEqualToLocation()
        {
            // Act
            this.mouse.Location = PointF.Empty;

            // Assert
            this.mouseDevice.Verify(x => x.SetCursorLocation(It.IsAny<PointF>()), Times.Never);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.mouseDevice = new Mock<IMouseDevice>();
            this.mouse = new Mouse(this.mouseDevice.Object);
        }

        [Test]
        public void WheelOffsetShouldBeZeroWhenNothingChanged()
        {
            // Act
            double actual = this.mouse.WheelOffset;

            // Assert
            Assert.Zero(actual);
        }
    }
}