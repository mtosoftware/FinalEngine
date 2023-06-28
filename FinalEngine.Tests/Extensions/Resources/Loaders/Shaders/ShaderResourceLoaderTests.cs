// <copyright file="ShaderResourceLoaderTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Extensions.Resources.Loaders.Shaders;

using System;
using System.IO;
using FinalEngine.Extensions.Resources.Loaders.Shaders;
using FinalEngine.IO;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Pipeline;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class ShaderResourceLoaderTests
{
    private Mock<IGPUResourceFactory> factory;

    private Mock<IFileSystem> fileSystem;

    private ShaderResourceLoader loader;

    private Mock<IShader> shader;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ShaderResourceLoader(null, this.fileSystem.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new ShaderResourceLoader(this.factory.Object, null);
        });
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderFragmentWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.frag");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Fragment, "test"), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderFSWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.fs");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Fragment, "test"), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderVertexWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.vert");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Vertex, "test"), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeCreateShaderVSWhenFileIsOpenedAndRead()
    {
        // Act
        this.loader.LoadResource("test.vs");

        // Assert
        this.factory.Verify(x => x.CreateShader(PipelineTarget.Vertex, "test"), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeFileSystemGetExtensionWhenFileExists()
    {
        // Act
        this.loader.LoadResource("test.vs");

        // Assert
        this.fileSystem.Verify(x => x.GetExtension("test.vs"), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeOpenFileWhenFileExists()
    {
        // Act
        this.loader.LoadResource("test.vert");

        // Assert
        this.fileSystem.Verify(x => x.OpenFile("test.vert", FileAccessMode.Read), Times.Once);
    }

    [Test]
    public void LoadResourceShouldThrowArgumentNullExceptionWhenFilePathIsEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.loader.LoadResource(string.Empty);
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
    public void LoadResourceShouldThrowArgumentNullExceptionWhenFilePathIsWhitespace()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.loader.LoadResource("\t\n\r ");
        });
    }

    [Test]
    public void LoadResourceShouldThrowFileNotFoundExceptionWhenFileExistsReturnsFalse()
    {
        // Arrange
        this.fileSystem.Setup(x => x.FileExists(It.IsAny<string>())).Returns(false);

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
        this.fileSystem = new Mock<IFileSystem>();
        this.factory = new Mock<IGPUResourceFactory>();
        this.shader = new Mock<IShader>();

        this.fileSystem.Setup(x => x.GetExtension("test.vert")).Returns(".vert");
        this.fileSystem.Setup(x => x.GetExtension("test.frag")).Returns(".frag");
        this.fileSystem.Setup(x => x.GetExtension("test.vs")).Returns(".vs");
        this.fileSystem.Setup(x => x.GetExtension("test.fs")).Returns(".fs");
        this.fileSystem.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
        this.fileSystem.Setup(x => x.OpenFile(It.IsAny<string>(), FileAccessMode.Read)).Returns(() =>
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write("test");
            writer.Flush();

            stream.Position = 0;

            return stream;
        });

        this.factory.Setup(x => x.CreateShader(It.IsAny<PipelineTarget>(), It.IsAny<string>())).Returns(this.shader.Object);

        this.loader = new ShaderResourceLoader(this.factory.Object, this.fileSystem.Object);
    }
}
