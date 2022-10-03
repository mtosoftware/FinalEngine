// <copyright file="SpriteComponentTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Components
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.Rendering.Components;
    using FinalEngine.Rendering.Textures;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class SpriteComponentTests
    {
        private SpriteComponent component;

        private Mock<ITexture2D> texture;

        [Test]
        public void ColorShouldReturnRedWhenInvoked()
        {
            // Arrange
            this.component.Color = Color.Red;

            // Act
            var color = this.component.Color;

            // Assert
            Assert.AreEqual(Color.Red, color);
        }

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenInvoked()
        {
            // Act and assert
            Assert.DoesNotThrow(() => new SpriteComponent());
        }

        [Test]
        public void ConstructorShouldSetColorToWhiteWhenInvoked()
        {
            // Act
            var actual = this.component.Color;

            // Assert
            Assert.AreEqual(Color.White, actual);
        }

        [Test]
        public void ConstructorShouldSetOriginToZeroWhenInvoked()
        {
            // Act
            var actual = this.component.Origin;

            // Assert
            Assert.AreEqual(Vector2.Zero, actual);
        }

        [Test]
        public void ConstructorShouldSetScaleToOneWhenInvoked()
        {
            // Act
            var actual = this.component.Scale;

            // Assert
            Assert.AreEqual(Vector2.One, actual);
        }

        [Test]
        public void ConstructorShouldSetTextureToParameterWhenInvoked()
        {
            // Act
            var actual = this.component.Texture;

            // Assert
            Assert.AreSame(this.texture.Object, actual);
        }

        [Test]
        public void OriginShouldReturnValueWhenInvoked()
        {
            // Arrange
            var expected = new Vector2(10, 12);
            this.component.Origin = expected;

            // Act
            var actual = this.component.Origin;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ScaleShouldReturnValueWhenInvoked()
        {
            // Arrange
            var expected = new Vector2(42, 21);
            this.component.Scale = expected;

            // Act
            var actual = this.component.Scale;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [SetUp]
        public void Setup()
        {
            this.texture = new Mock<ITexture2D>();

            this.texture.Setup(x => x.Description).Returns(new Texture2DDescription()
            {
                Width = 256,
                Height = 512,
            });

            this.component = new SpriteComponent(this.texture.Object);
        }

        [Test]
        public void SpriteHeightShouldReturnScaleYTimesTextureHeight()
        {
            // Arrange
            const int Expected = 256;
            this.component.Scale = new Vector2(1, 0.5f);

            // Act
            var actual = this.component.SpriteHeight;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void SpriteWidthShouldReturnScaleXTimesTextureWidth()
        {
            // Arrange
            const int Expected = 512;
            this.component.Scale = new Vector2(2, 1);

            // Act
            var actual = this.component.SpriteWidth;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void TextureHeightShouldReturnHeightOfTextureWhenTextureIsNotNull()
        {
            // Act
            var actual = this.component.TextureHeight;

            // Assert
            Assert.AreEqual(this.texture.Object.Description.Height, actual);
        }

        [Test]
        public void TextureHeightShouldReturnZeroWhenTextureIsNull()
        {
            // Arrange
            this.component.Texture = null;

            // Act
            var actual = this.component.TextureHeight;

            // Assert
            Assert.Zero(actual);
        }

        [Test]
        public void TextureShouldReturnNullWhenSetToNull()
        {
            // Arrange
            this.component.Texture = null;

            // Act
            var actual = this.component.Texture;

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public void TextureShouldReturnTextureWhenSetToTexture()
        {
            // Act
            var actual = this.component.Texture;

            // Assert
            Assert.AreSame(this.texture.Object, actual);
        }

        [Test]
        public void TextureWidthShouldReturnWidthOfTextureWhenTextureIsNotNull()
        {
            // Act
            var actual = this.component.TextureWidth;

            // Assert
            Assert.AreEqual(this.texture.Object.Description.Width, actual);
        }

        [Test]
        public void TextureWidthShouldReturnZeroWhenTextureIsNull()
        {
            // Arrange
            this.component.Texture = null;

            // Act
            var actual = this.component.TextureWidth;

            // Assert
            Assert.Zero(actual);
        }
    }
}