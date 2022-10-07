// <copyright file="Texture2DLoaderTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Textures
{
    using System;
    using System.IO;
    using FinalEngine.Extensions.Resources.Invocation;
    using FinalEngine.Extensions.Resources.Loaders;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;
    using Moq;
    using NUnit.Framework;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    public class Texture2DLoaderTests
    {
        private Mock<IGPUResourceFactory> factory;

        private Mock<IFileSystem> fileSystem;

        private Image<Rgba32> image;

        private Mock<IImageInvoker> invoker;

        private Texture2DResourceLoader loader;

        private Mock<Stream> stream;

        private Mock<ITexture2D> texture;

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenFactoryIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Texture2DResourceLoader(this.fileSystem.Object, null, this.invoker.Object);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Texture2DResourceLoader(null, this.factory.Object, this.invoker.Object);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Texture2DResourceLoader(this.fileSystem.Object, this.factory.Object, null);
            });
        }

        [Test]
        public void LoadResourceShouldInvokeCreateTexture2DWhenLoaded()
        {
            // Act
            this.loader.LoadResource("texture");

            // Assert
            this.factory.Verify(x => x.CreateTexture2D(
                It.IsAny<Texture2DDescription>(),
                It.IsAny<int[]>(),
                PixelFormat.Rgba,
                SizedFormat.Rgba8));
        }

        [Test]
        public void LoadResourceShouldInvokeLoadWhenFileExists()
        {
            // Act
            this.loader.LoadResource("texture");

            // Assert
            this.invoker.Verify(x => x.Load<Rgba32>(this.stream.Object));
        }

        [Test]
        public void LoadResourceShouldInvokeOpenFileWhenFileExists()
        {
            // Act
            this.loader.LoadResource("texture");

            // Assert
            this.fileSystem.Verify(x => x.OpenFile("texture", FileAccessMode.Read));
        }

        [Test]
        public void LoadResourceShouldReturnTextureWhenLoaded()
        {
            // Act
            var texture = this.loader.LoadResource("texture");

            // Assert
            Assert.AreSame(this.texture.Object, texture);
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
                this.loader.LoadResource("\t\n\r");
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
                this.loader.LoadResource("texture");
            });
        }

        [SetUp]
        public void Setup()
        {
            // Setup
            this.fileSystem = new Mock<IFileSystem>();
            this.stream = new Mock<Stream>();

            this.fileSystem.Setup(x => x.FileExists(It.IsAny<string>())).Returns(true);
            this.fileSystem.Setup(x => x.OpenFile(It.IsAny<string>(), FileAccessMode.Read)).Returns(this.stream.Object);

            this.invoker = new Mock<IImageInvoker>();

            this.image = new Image<Rgba32>(1, 24);
            this.invoker.Setup(x => x.Load<Rgba32>(It.IsAny<Stream>())).Returns(this.image);

            this.factory = new Mock<IGPUResourceFactory>();

            this.texture = new Mock<ITexture2D>();
            this.factory.Setup(x => x.CreateTexture2D(
                It.IsAny<Texture2DDescription>(),
                It.IsAny<int[]>(),
                PixelFormat.Rgba,
                SizedFormat.Rgba8)).Returns(this.texture.Object);

            this.loader = new Texture2DResourceLoader(this.fileSystem.Object, this.factory.Object, this.invoker.Object);
        }

        [TearDown]
        public void Teardown()
        {
            this.image.Dispose();
        }
    }
}
