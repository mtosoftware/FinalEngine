// <copyright file="ShaderResourceLoaderTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Loaders.Shaders;

using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Pipeline;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class ShaderResourceLoaderTests
{
    private Mock<IGPUResourceFactory> factory;

    private MockFileSystem fileSystem;

    private ShaderResourceLoader loader;

    private Mock<IShader> shader;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ShaderResourceLoader(this.fileSystem, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ShaderResourceLoader(null, this.factory.Object);
        });
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderFragmentWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.frag");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Fragment, string.Empty), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderFSWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.fs");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Fragment, string.Empty), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderVertexWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.vert");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Vertex, string.Empty), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderVSWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.vs");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Vertex, string.Empty), Times.Once);
    }

    [Test]
    public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.loader.LoadResource(string.Empty);
        });
    }

    [Test]
    public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.loader.LoadResource("\t\n\r ");
        });
    }

    [Test]
    public void LoadResourceShouldThrowArgumentNullExceptionWhenFilePathIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.loader.LoadResource(null);
        });
    }

    [Test]
    public void LoadResourceShouldThrowFileNotFoundExceptionWhenFileExistsReturnsFalse()
    {
        // Act and assert
        Assert.Throws<FileNotFoundException>(() =>
        {
            this.loader.LoadResource("test");
        });
    }

    [Test]
    public void LoadResourceShouldThrowNotSupportedExceptionWhenExtensionDoesNotExist()
    {
        // Act and assert
        Assert.Throws<NotSupportedException>(() =>
        {
            this.loader.LoadResource("test.txt");
        });
    }

    [SetUp]
    public void Setup()
    {
        this.factory = new Mock<IGPUResourceFactory>();
        this.shader = new Mock<IShader>();
        this.fileSystem = new MockFileSystem();

        this.fileSystem.AddEmptyFile("test.vert");
        this.fileSystem.AddEmptyFile("test.frag");
        this.fileSystem.AddEmptyFile("test.vs");
        this.fileSystem.AddEmptyFile("test.fs");
        this.fileSystem.AddEmptyFile("test.txt");

        this.factory.Setup(x => x.CreateShader(It.IsAny<PipelineTarget>(), It.IsAny<string>())).Returns(this.shader.Object);

        this.loader = new ShaderResourceLoader(this.fileSystem, this.factory.Object);
    }
}
