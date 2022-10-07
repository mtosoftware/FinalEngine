// <copyright file="Texture2DDescriptionTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Textures
{
    using FinalEngine.Rendering.Textures;
    using NUnit.Framework;

    public class Texture2DDescriptionTests
    {
        private Texture2DDescription description;

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            var right = new Texture2DDescription()
            {
                Width = 42,
                Height = 55,
                MinFilter = TextureFilterMode.Nearest,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Byte,
                WrapS = TextureWrapMode.Repeat,
                WrapT = TextureWrapMode.Clamp,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualityOperatorShouldReturnTrueWhenPropertiesMatch()
        {
            // Arrange
            var left = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            var right = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            // Act
            bool actual = left == right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNotTexture2DDescription()
        {
            // Act
            bool actual = this.description.Equals(new object());

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNull()
        {
            // Act
            bool actual = this.description.Equals(null);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            var right = new Texture2DDescription()
            {
                Width = 42,
                Height = 55,
                MinFilter = TextureFilterMode.Nearest,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Byte,
                WrapS = TextureWrapMode.Repeat,
                WrapT = TextureWrapMode.Clamp,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenObjectIsTexture2DDescriptionAndHasSameProperties()
        {
            // Arrange
            var left = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            object right = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void GetHashCodeShouldReturnSameAsOtherObjectWhenPropertiesAreEqual()
        {
            // Arrange
            var left = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            var right = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            // Act
            int leftHashCode = left.GetHashCode();
            int rightHashCode = right.GetHashCode();

            // Assert
            Assert.AreEqual(leftHashCode, rightHashCode);
        }

        [Test]
        public void HeightShouldReturnTenWhenSetToTen()
        {
            // Act
            this.description.Height = 10;

            // Assert
            Assert.AreEqual(10, this.description.Height);
        }

        [Test]
        public void HeightShouldReturnZeroWhenDefault()
        {
            // Act
            int actual = this.description.Height;

            // Assert
            Assert.Zero(actual);
        }

        [Test]
        public void InEqualityOperatorShouldReturnFalseWhenPropertiesMatch()
        {
            // Arrange
            var left = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            var right = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void InEqualityOperatorShouldReturnTrueWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new Texture2DDescription()
            {
                Width = 2,
                Height = 5,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Short,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Repeat,
            };

            var right = new Texture2DDescription()
            {
                Width = 42,
                Height = 55,
                MinFilter = TextureFilterMode.Nearest,
                MagFilter = TextureFilterMode.Nearest,
                PixelType = PixelType.Byte,
                WrapS = TextureWrapMode.Repeat,
                WrapT = TextureWrapMode.Clamp,
            };

            // Act
            bool actual = left != right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void MagFilterShouldReturnLinearWhenDefault()
        {
            // Act
            var actual = this.description.MagFilter;

            // Assert
            Assert.AreEqual(TextureFilterMode.Linear, actual);
        }

        [Test]
        public void MagFilterShouldReturnNearestWhenSetToNearest()
        {
            // Act
            this.description.MagFilter = TextureFilterMode.Nearest;

            // Assert
            Assert.AreEqual(TextureFilterMode.Nearest, this.description.MagFilter);
        }

        [Test]
        public void MinFilterShouldReturnLinearWhenDefault()
        {
            // Act
            var actual = this.description.MinFilter;

            // Assert
            Assert.AreEqual(TextureFilterMode.Linear, actual);
        }

        [Test]
        public void MinFilterShouldReturnNearestWhenSetToNearest()
        {
            // Act
            this.description.MinFilter = TextureFilterMode.Nearest;

            // Assert
            Assert.AreEqual(TextureFilterMode.Nearest, this.description.MinFilter);
        }

        [Test]
        public void PixelTypeShouldReturnByteWhenDefault()
        {
            // Act
            var actual = this.description.PixelType;

            // Assert
            Assert.AreEqual(PixelType.Byte, actual);
        }

        [Test]
        public void PixelTypeShouldReturnShortWhenSetToShort()
        {
            // Act
            this.description.PixelType = PixelType.Short;

            // Assert
            Assert.AreEqual(PixelType.Short, this.description.PixelType);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.description = default;
        }

        [Test]
        public void WidthShouldReturnTwentySevenWhenSetToTwentySeven()
        {
            // Act
            this.description.Width = 27;

            // Assert
            Assert.AreEqual(27, this.description.Width);
        }

        [Test]
        public void WidthShouldReturnZeroWhenDefault()
        {
            // Act
            int actual = this.description.Width;

            // Assert
            Assert.Zero(actual);
        }

        [Test]
        public void WrapSShouldReturnClampWhenSetToClamp()
        {
            // Act
            this.description.WrapS = TextureWrapMode.Clamp;

            // Assert
            Assert.AreEqual(TextureWrapMode.Clamp, this.description.WrapS);
        }

        [Test]
        public void WrapSShouldReturnRepeatWhenDefault()
        {
            // Act
            var actual = this.description.WrapS;

            // Assert
            Assert.AreEqual(TextureWrapMode.Repeat, actual);
        }

        [Test]
        public void WrapTShouldReturnClampWhenSetToClamp()
        {
            // Act
            this.description.WrapT = TextureWrapMode.Clamp;

            // Assert
            Assert.AreEqual(TextureWrapMode.Clamp, this.description.WrapT);
        }

        [Test]
        public void WrapTShouldReturnRepeatWhenDefault()
        {
            // Act
            var actual = this.description.WrapT;

            // Assert
            Assert.AreEqual(TextureWrapMode.Repeat, actual);
        }
    }
}
