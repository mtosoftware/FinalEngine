// <copyright file="OpenGLTexture2DTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Textures
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Textures;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Utilities;
    using Moq;
    using NUnit.Framework;
    using OpenTK.Graphics.OpenGL4;
    using PixelFormat = FinalEngine.Rendering.Textures.PixelFormat;
    using PixelType = FinalEngine.Rendering.Textures.PixelType;
    using TextureWrapMode = FinalEngine.Rendering.Textures.TextureWrapMode;
    using TKPixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
    using TKPixelType = OpenTK.Graphics.OpenGL4.PixelType;
    using TKTextureWrapMode = OpenTK.Graphics.OpenGL4.TextureWrapMode;

    [SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "This is done in TearDown.")]
    public class OpenGLTexture2DTests
    {
        private const int ID = 104;

        private Texture2DDescription description;

        private Mock<IOpenGLInvoker> invoker;

        private Mock<IEnumMapper> mapper;

        private OpenGLTexture2D texture;

        [Test]
        public void BindShouldInvokeBindTextureUnitWhenTextureIsNotDisposed()
        {
            // Act
            this.texture.Bind(4);

            // Assert
            this.invoker.Verify(x => x.BindTextureUnit(4, ID), Times.Once);
        }

        [Test]
        public void BindShouldThrowObjectDisposedExceptionWhenTextureIsDisposed()
        {
            // Arrange
            this.texture.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() => this.texture.Bind(0));
        }

        [Test]
        public void ConstructorShouldInvokeGenTextureWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.CreateTexture(TextureTarget.Texture2D), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeTextureParameterMagFilterWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.TextureParameter(ID, TextureParameterName.TextureMagFilter, (int)this.mapper.Object.Forward<TextureMagFilter>(this.description.MagFilter)), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeTextureParameterMinFilterWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.TextureParameter(ID, TextureParameterName.TextureMinFilter, (int)this.mapper.Object.Forward<TextureMinFilter>(this.description.MinFilter)), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeTextureParameterWrapSWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.TextureParameter(ID, TextureParameterName.TextureWrapS, (int)this.mapper.Object.Forward<TKTextureWrapMode>(this.description.WrapS)), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeTextureParameterWrapTWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.TextureParameter(ID, TextureParameterName.TextureWrapT, (int)this.mapper.Object.Forward<TKTextureWrapMode>(this.description.WrapT)), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeTextureStorage2DWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.TextureStorage2D(ID, 1, this.mapper.Object.Forward<SizedInternalFormat>(this.texture.InternalFormat), this.description.Width, this.description.Height));
        }

        [Test]
        public void ConstructorShouldInvokeTextureSubImage2DWhenInvoked()
        {
            // Assert
            this.invoker.Verify(x => x.TextureSubImage2D(ID, 0, 0, 0, this.description.Width, this.description.Height, this.mapper.Object.Forward<TKPixelFormat>(this.texture.Format), this.mapper.Object.Forward<TKPixelType>(this.description.PixelType), new IntPtr(1)));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLTexture2D(null, this.mapper.Object, default, PixelFormat.Rgba, SizedFormat.R8, new IntPtr(1)));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLTexture2D(this.invoker.Object, null, default, PixelFormat.Depth, SizedFormat.R8, new IntPtr(1)));
        }

        [Test]
        public void DescriptionShouldReturnSameAsConstructorWhenInvoked()
        {
            // Act
            Texture2DDescription actual = this.texture.Description;

            // Assert
            Assert.AreEqual(this.description, actual);
        }

        [Test]
        public void FormatShouldReturnRgbaWhenInvoked()
        {
            // Act
            PixelFormat actual = this.texture.Format;

            // Assert
            Assert.AreEqual(PixelFormat.Rgba, actual);
        }

        [Test]
        public void InternalFormatShouldReturnRgWhenInvoked()
        {
            // Act
            SizedFormat actual = this.texture.InternalFormat;

            // Assert
            Assert.AreEqual(SizedFormat.R8, actual);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.invoker.Setup(x => x.CreateTexture(TextureTarget.Texture2D)).Returns(ID);

            this.mapper = new Mock<IEnumMapper>();

            this.description = new Texture2DDescription()
            {
                Width = 20,
                Height = 30,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            this.texture = new OpenGLTexture2D(this.invoker.Object, this.mapper.Object, this.description, PixelFormat.Rgba, SizedFormat.R8, new IntPtr(1));
        }

        [TearDown]
        public void Teardown()
        {
            this.texture.Dispose();
        }

        [Test]
        public void UnbindShouldInvokeBindTextureWhenTextureIsNotDisposed()
        {
            // Act
            this.texture.Unbind();

            // Assert
            this.invoker.Verify(x => x.BindTexture(TextureTarget.Texture2D, 0), Times.Once);
        }

        [Test]
        public void UnbindShouldThrowObjectDisposedExceptionWhenTextureIsDisposed()
        {
            // Arrange
            this.texture.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() => this.texture.Unbind());
        }
    }
}