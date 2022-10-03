// <copyright file="PlatformResolverTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Launching
{
    using System;
    using System.Runtime.InteropServices;
    using FinalEngine.Launching;
    using FinalEngine.Launching.Invocation;
    using Moq;
    using NUnit.Framework;

    public class PlatformResolverTests
    {
        private PlatformResolver resolver;

        private Mock<IRuntimeInformationInvoker> runtime;

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenRuntimeIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new PlatformResolver(null));
        }

        [Test]
        public void RegisterShouldNotThrowExceptionWhenPlatformAlreadyRegisteredAndRemoveIsTrue()
        {
            // Arrange
            this.resolver.Register(OSPlatform.Windows, Mock.Of<IGamePlatformFactory>());

            // Act and assert
            Assert.DoesNotThrow(() => this.resolver.Register(OSPlatform.Windows, Mock.Of<IGamePlatformFactory>(), true));
        }

        [Test]
        public void RegisterShouldThrowArgumentExceptionWhenPlatformAlreadyRegisteredAndRemoveIsFalse()
        {
            // Arrange
            this.resolver.Register(OSPlatform.Windows, Mock.Of<IGamePlatformFactory>());

            // Act and assert
            Assert.Throws<ArgumentException>(() => this.resolver.Register(OSPlatform.Windows, Mock.Of<IGamePlatformFactory>(), false));
        }

        [Test]
        public void RegisterShouldThrowArgumentNullExceptionWhenFactoryIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() => this.resolver.Register(OSPlatform.Windows, null));
        }

        [Test]
        public void ResolveShouldReturnFactoryWhenPlatformIsRegistered()
        {
            // Arrange
            IGamePlatformFactory expected = Mock.Of<IGamePlatformFactory>();

            this.resolver.Register(OSPlatform.Linux, expected);
            this.resolver.Register(OSPlatform.Windows, expected);

            // Act
            IGamePlatformFactory actual = this.resolver.Resolve();

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void ResolveShouldThrowPlatformNotSupportedExceptionWhenPlatformIsNotRegistered()
        {
            // Act and assert
            Assert.Throws<PlatformNotSupportedException>(() => this.resolver.Resolve());
        }

        [SetUp]
        public void Setup()
        {
            this.runtime = new Mock<IRuntimeInformationInvoker>();
            this.runtime.Setup(x => x.IsOSPlatform(OSPlatform.Windows)).Returns(true);

            this.resolver = new PlatformResolver(this.runtime.Object);
        }
    }
}