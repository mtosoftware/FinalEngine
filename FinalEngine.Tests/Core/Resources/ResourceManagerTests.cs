// <copyright file="ResourceManagerTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Resources
{
    using System;
    using FinalEngine.Resources;
    using FinalEngine.Resources.Exceptions;
    using Moq;
    using NUnit.Framework;

    public class ResourceManagerTests
    {
        private const string FilePath = "path/to/file";

        private Mock<IResource> resource;

        private Mock<ResourceLoaderBase<IResource>> resourceLoader;

        private ResourceManager resourceManager;

        [Test]
        public void DisposeShouldInvokeResourceDisposeWhenInvoked()
        {
            // Arrange
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);
            this.resourceManager.LoadResource<IResource>(FilePath);

            // Act
            this.resourceManager.Dispose();

            // Assert
            this.resource.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void InstanceShouldReturnResourceManagerWhenInvoked()
        {
            // Arrange
            var expected = ResourceManager.Instance;

            // Act
            var actual = ResourceManager.Instance;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void LoadResourceShouldInvokeResourceLoaderLoadResourceWhenResourceHasNotBeenPreviouslyLoaded()
        {
            // Arrange
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);

            // Act
            this.resourceManager.LoadResource<IResource>(FilePath);

            // Assert
            this.resourceLoader.Verify(x => x.LoadResource(FilePath), Times.Once);
        }

        [Test]
        public void LoadResourceShouldNotInvokeResourceLoaderLoadResourceWhenResourceHasBeenPreviouslyLoaded()
        {
            // Arrange
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);
            this.resourceManager.LoadResource<IResource>(FilePath);

            // Act
            this.resourceManager.LoadResource<IResource>(FilePath);

            // Assert
            this.resourceLoader.Verify(x => x.LoadResource(FilePath), Times.Once);
        }

        [Test]
        public void LoadResourceShouldReturnCorrectResourceWhenResourceHasBeenPreviouslyLoaded()
        {
            // Arrange
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);
            var expected = this.resourceManager.LoadResource<IResource>(FilePath);

            // Act
            var actual = this.resourceManager.LoadResource<IResource>(FilePath);

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsEmpty()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.resourceManager.LoadResource<IResource>(string.Empty);
            });
        }

        [Test]
        public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.resourceManager.LoadResource<IResource>(null);
            });
        }

        [Test]
        public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsWhitespace()
        {
            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.resourceManager.LoadResource<IResource>("\t\r\n");
            });
        }

        [Test]
        public void LoadResourceShouldThrowResourceLoaderNotRegisteredExceptionWhenGenericTypeNotRegisteredWithLoader()
        {
            // Act and assert
            Assert.Throws<ResourceLoaderNotRegisteredException>(() =>
            {
                this.resourceManager.LoadResource<IResource>("path/to/file");
            });
        }

        [Test]
        public void RegisterLoaderShouldThrowArgumentExceptionWhenResourceLoaderTypeHasAlreadyBeenRegistered()
        {
            // Arrange
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);
            var resourceLoader = new Mock<ResourceLoaderBase<IResource>>();

            // Act and assert
            Assert.Throws<ArgumentException>(() =>
            {
                this.resourceManager.RegisterLoader(resourceLoader.Object);
            });
        }

        [Test]
        public void RegisterLoaderShouldThrowArgumentNullExceptionWhenLoaderIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.resourceManager.RegisterLoader<IResource>(null);
            });
        }

        [SetUp]
        public void Setup()
        {
            this.resourceManager = new ResourceManager();
            this.resourceLoader = new Mock<ResourceLoaderBase<IResource>>();
            this.resource = new Mock<IResource>();

            this.resourceLoader.Setup(x => x.LoadResource(FilePath)).Returns(this.resource.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.resourceManager.Dispose();
        }

        [Test]
        public void UnloadResourceShouldCallResourceDisposeWhenReferencedOnce()
        {
            // Arrange
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);
            this.resourceManager.LoadResource<IResource>(FilePath);

            // Act
            this.resourceManager.UnloadResource(this.resource.Object);

            // Assert
            this.resource.Verify(x => x.Dispose(), Times.Once);
        }

        [Test]
        public void UnloadResourceShouldNotCallResourceDisposeWhenReferencedMoreThanOnce()
        {
            // Arrange
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);

            for (int i = 0; i < 2; i++)
            {
                this.resourceManager.LoadResource<IResource>(FilePath);
            }

            // Act
            this.resourceManager.UnloadResource(this.resource.Object);

            // Assert
            this.resource.Verify(x => x.Dispose(), Times.Never);
        }

        [Test]
        public void UnloadResourceShouldThrowArgumentNullExceptionWhenResourceIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.resourceManager.UnloadResource(null);
            });
        }
    }
}
