// <copyright file="ResourceManagerTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Resources;

using System;
using FinalEngine.Resources;
using FinalEngine.Resources.Exceptions;
using FinalEngine.Tests.Core.Resources.Mocks;
using Moq;
using NUnit.Framework;

public class ResourceManagerTests
{
    private const string DisposableFilePath = "path/to/file/disposable";

    private const string FilePath = "path/to/file";

    private MockDisposableResource disposableResource;

    private Mock<ResourceLoaderBase<MockDisposableResource>> disposableResourceLoader;

    private Mock<IResource> resource;

    private Mock<ResourceLoaderBase<IResource>> resourceLoader;

    private ResourceManager resourceManager;

    [Test]
    public void DisposeShouldInvokeDisposeableResourceDisposeWhenInvoked()
    {
        // Arrange
        this.resourceManager.RegisterLoader<MockDisposableResource>(this.disposableResourceLoader.Object);
        _ = this.resourceManager.LoadResource<MockDisposableResource>(DisposableFilePath);

        // Act
        this.resourceManager.Dispose();

        // Assert
        Assert.That(this.disposableResource.IsDisposed, Is.True);
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
    public void LoadResourceShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.resourceManager.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.resourceManager.LoadResource<IResource>("resource");
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

    [Test]
    public void RegisterLoaderShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.resourceManager.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.resourceManager.RegisterLoader(this.resourceLoader.Object);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.resourceManager = new ResourceManager();
        this.resourceLoader = new Mock<ResourceLoaderBase<IResource>>();
        this.disposableResourceLoader = new Mock<ResourceLoaderBase<MockDisposableResource>>();
        this.resource = new Mock<IResource>();
        this.disposableResource = new MockDisposableResource();

        this.resourceLoader.Setup(x => x.LoadResource(FilePath)).Returns(this.resource.Object);
        this.disposableResourceLoader.Setup(x => x.LoadResource(DisposableFilePath)).Returns(this.disposableResource);
    }

    [TearDown]
    public void TearDown()
    {
        this.resourceManager.Dispose();
    }

    [Test]
    public void UnloadResourceShouldInvokeDisposeWhenResourceImplementsIDisposable()
    {
        // Arrange
        this.resourceManager.RegisterLoader<MockDisposableResource>(this.disposableResourceLoader.Object);
        _ = this.resourceManager.LoadResource<MockDisposableResource>(DisposableFilePath);

        // Act
        this.resourceManager.UnloadResource(this.disposableResource);

        // Assert
        Assert.That(this.disposableResource.IsDisposed, Is.True);
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

    [Test]
    public void UnloadResourceShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.resourceManager.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.resourceManager.UnloadResource(this.resource.Object);
        });
    }
}
