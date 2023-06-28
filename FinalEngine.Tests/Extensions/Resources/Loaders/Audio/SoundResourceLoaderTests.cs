// <copyright file="SoundResourceLoaderTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Extensions.Resources.Loaders.Audio;

using System;
using System.IO;
using FinalEngine.Audio.OpenAL;
using FinalEngine.Extensions.Resources.Factories;
using FinalEngine.Extensions.Resources.Loaders.Audio;
using FinalEngine.IO;
using Moq;
using NUnit.Framework;
using ICASLSound = CASL.ISound;

[TestFixture]
public sealed class SoundResourceLoaderTests
{
    private Mock<ICASLSoundFactory> factory;

    private Mock<IFileSystem> fileSystem;

    private SoundResourceLoader loader;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SoundResourceLoader(this.fileSystem.Object, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SoundResourceLoader(null, this.factory.Object);
        });
    }

    [Test]
    public void LoadResourceShouldInvokeFactoryCreateSoundWhenInvoked()
    {
        // Act
        this.loader.LoadResource("sound.mp3");

        // Assert
        this.factory.Verify(x => x.CreateSound("sound.mp3"), Times.Once);
    }

    [Test]
    public void LoadResourceShouldInvokeFileSystemFileExistsWhenInvoked()
    {
        // Act
        this.loader.LoadResource("sound.mp3");

        // Assert
        this.fileSystem.Verify(x => x.FileExists("sound.mp3"), Times.Once);
    }

    [Test]
    public void LoadResourceShouldReturnOpenALSoundWhenInvoked()
    {
        // Act
        var actual = this.loader.LoadResource("sound.mp3");

        // Assert
        Assert.That(actual, Is.TypeOf<OpenALSound>());
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
    public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.loader.LoadResource(null);
        });
    }

    [Test]
    public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsWhiteSpace()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            this.loader.LoadResource("\r\n\t ");
        });
    }

    [Test]
    public void LoadResourceShouldThrowFileNotFoundExceptionWhenFileSystemFileExistsReturnsFalse()
    {
        // Arrange
        this.fileSystem.Setup(x => x.FileExists(It.IsAny<string>())).Returns(false);

        // Act and assert
        Assert.Throws<FileNotFoundException>(() =>
        {
            this.loader.LoadResource("sound.mp3");
        });
    }

    [SetUp]
    public void Setup()
    {
        this.factory = new Mock<ICASLSoundFactory>();
        this.factory.Setup(x => x.CreateSound(It.IsAny<string>())).Returns(Mock.Of<ICASLSound>());
        this.fileSystem = new Mock<IFileSystem>();
        this.fileSystem.Setup(x => x.FileExists("sound.mp3")).Returns(true);

        this.loader = new SoundResourceLoader(this.fileSystem.Object, this.factory.Object);
    }
}
