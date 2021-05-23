// <copyright file="GameTimeTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Launching
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Launching;
    using FinalEngine.Launching.Invocation;
    using Moq;
    using NUnit.Framework;

    [ExcludeFromCodeCoverage]
    public class GameTimeTests
    {
        private const double FrameCap = 120.0d;

        private GameTime gameTime;

        private Mock<IStopwatchInvoker> watch;

        [Test]
        public void CanProcessNextFrameShouldInvokeWatchRestartWhenWatchIsNotRunning()
        {
            // Arrange
            this.watch.SetupGet(x => x.IsRunning).Returns(false);

            // Act
            this.gameTime.CanProcessNextFrame(out _);

            // Assert
            this.watch.Verify(x => x.Restart(), Times.Once);
        }

        [Test]
        public void CanProcessNextFrameShouldOutDefaultGameTimeInfoWhenCurrentTimeIsNotEqualToLastTimePlusWaitTime()
        {
            // Act
            this.gameTime.CanProcessNextFrame(out GameTimeInfo actual);

            // Assert
            Assert.AreEqual(default(GameTimeInfo), actual);
        }

        [Test]
        public void CanProcessNextFrameShouldReturnCorrectGameTimeInfoWhenCurrentTimeIsGreaterThanLastTimePlusWaitTime()
        {
            // Arrange
            const double WaitTime = 8.4d;

            this.watch.SetupGet(x => x.Elapsed).Returns(TimeSpan.FromMilliseconds(WaitTime));

            double delta = TimeSpan.FromMilliseconds(WaitTime).TotalMilliseconds;

            var expected = new GameTimeInfo()
            {
                Delta = delta,
                FrameRate = Math.Round(1000.0d / delta),
            };

            // Act
            this.gameTime.CanProcessNextFrame(out GameTimeInfo actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanProcessNextFrameShouldReturnFalseWhenCurrentTimeIsNotEqualToLastTimePlusWaitTime()
        {
            // Act
            bool actual = this.gameTime.CanProcessNextFrame(out _);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void CanProcessNextFrameShouldReturnTrueWhenCurrentTimeIsGreaterThanLastTimePlusWaitTime()
        {
            // Arrange
            this.watch.SetupGet(x => x.Elapsed).Returns(TimeSpan.FromMilliseconds(8.4d));

            // Act
            bool actual = this.gameTime.CanProcessNextFrame(out _);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenWatchIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new GameTime(null, FrameCap));
        }

        [Test]
        public void ConstructorShouldThrowDivideByZeroExceptionWhenFrameCapIsEqualToZero()
        {
            // Arrange, act and assert
            Assert.Throws<DivideByZeroException>(() => new GameTime(this.watch.Object, 0.0d));
        }

        [Test]
        public void ConstructorShouldThrowDivideByZeroExceptionWhenFrameCapIsLessThanZero()
        {
            // Arrange, act and assert
            Assert.Throws<DivideByZeroException>(() => new GameTime(this.watch.Object, -0.1d));
        }

        [SetUp]
        public void Setup()
        {
            this.watch = new Mock<IStopwatchInvoker>();
            this.gameTime = new GameTime(this.watch.Object, FrameCap);
        }
    }
}