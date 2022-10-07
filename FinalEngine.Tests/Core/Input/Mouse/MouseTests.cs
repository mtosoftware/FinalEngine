// <copyright file="MouseTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Mouse
{
    using System;
    using System.Drawing;
    using FinalEngine.Input;
    using FinalEngine.Input.Mouses;
    using Moq;
    using NUnit.Framework;

    public class MouseTests
    {
        private Mouse mouse;

        private Mock<IMouseDevice> mouseDevice;

        [Test]
        public void DeltaShouldReturnDeviceLocationDeltaWhenDeviceIsNotNull()
        {
            // Act
            var actual = this.mouse.Delta;

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
            var actual = mouse.Delta;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeviceButtonDownShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.mouseDevice.Raise(
                    x =>
                {
                    x.ButtonDown += null;
                }, new object[] { new object(), null });
            });
        }

        [Test]
        public void DeviceButtonUpShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.mouseDevice.Raise(
                    x =>
                {
                    x.ButtonUp += null;
                }, new object[] { new object(), null });
            });
        }

        [Test]
        public void DeviceMoveShouldSetLocationWhenDeviceMoveRaised()
        {
            // Arrange
            var point = new PointF(100, 400);

            this.mouseDevice.Raise(
                x =>
            {
                x.Move += null;
            }, new MouseMoveEventArgs()
            {
                Location = point,
            });

            // Act
            var actual = this.mouse.Location;

            // Assert
            Assert.AreEqual(point, actual);
        }

        [Test]
        public void DeviceMoveShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.mouseDevice.Raise(
                    x =>
                {
                    x.Move += null;
                }, new object[] { new object(), null });
            });
        }

        [Test]
        public void DeviceScrollShouldSetWheelOffsetWhenDeviceScrollRaised()
        {
            // Arrange
            const double expected = 132.0d;

            this.mouseDevice.Raise(
                x =>
            {
                x.Scroll += null;
            }, new MouseScrollEventArgs()
            {
                Offset = expected,
            });

            // Act
            double actual = this.mouse.WheelOffset;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeviceScrollShouldThrowArgumentNullExceptionWhenEventDataIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.mouseDevice.Raise(
                    x =>
                {
                    x.Scroll += null;
                }, new object[] { new object(), null });
            });
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
            const MouseButton button = MouseButton.Button3;

            this.mouseDevice.Raise(
                x =>
            {
                x.ButtonDown += null;
            }, new MouseButtonEventArgs()
            {
                Button = button,
            });

            // Act
            bool actual = this.mouse.IsButtonDown(button);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsButtonPressedShouldReturnFalseWhenButtonIsNotDownDuringCurrentFrame()
        {
            // Arrange
            const MouseButton button = MouseButton.Right;

            this.mouseDevice.Raise(
                x =>
            {
                x.ButtonDown += null;
            }, new MouseButtonEventArgs()
            {
                Button = button,
            });

            this.mouse.Update();

            this.mouseDevice.Raise(
                x =>
            {
                x.ButtonUp += null;
            }, new MouseButtonEventArgs()
            {
                Button = button,
            });

            // Act
            bool actual = this.mouse.IsButtonPressed(button);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsButtonPressedShouldReturnTrueWhenButtonDownDuringCurrentFrameAndNotDownDuringPreviousFrame()
        {
            // Arrange
            const MouseButton button = MouseButton.Button6;

            this.mouseDevice.Raise(
                x =>
            {
                x.ButtonDown += null;
            }, new MouseButtonEventArgs()
            {
                Button = button,
            });

            // Act
            bool actual = this.mouse.IsButtonPressed(button);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void IsButtonReleasedShouldReturnFalseWhenButtonDownDuringCurrentFrameAndNotDownDuringPreviousFrame()
        {
            // Arrange
            const MouseButton button = MouseButton.Middle;

            this.mouseDevice.Raise(
                x =>
            {
                x.ButtonDown += null;
            }, new MouseButtonEventArgs()
            {
                Button = button,
            });

            // Act
            bool actual = this.mouse.IsButtonReleased(button);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void IsButtonReleasedShouldReturnTrueWhenButtonNotDownDuringCurrentFrameAndButtonDownDuringPreviousFrame()
        {
            // Arrange
            const MouseButton button = MouseButton.Left;

            this.mouseDevice.Raise(
                x =>
            {
                x.ButtonDown += null;
            }, new MouseButtonEventArgs()
            {
                Button = button,
            });

            this.mouse.Update();

            this.mouseDevice.Raise(
                x =>
            {
                x.ButtonUp += null;
            }, new MouseButtonEventArgs()
            {
                Button = button,
            });

            // Act
            bool actual = this.mouse.IsButtonReleased(button);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void LocationGetShouldReturnEmptyPointWhenNothingChanged()
        {
            // Act
            var actual = this.mouse.Location;

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
