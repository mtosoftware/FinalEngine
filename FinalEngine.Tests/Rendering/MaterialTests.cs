// <copyright file="MaterialTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering
{
    using System;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Resources;
    using Moq;
    using NUnit.Framework;

    public class MaterialTests
    {
        private Material material;

        private Mock<IPipeline> pipeline;

        [Test]
        public void BindShouldInvokePipelineSetTextureWhenInvoked()
        {
            // Arrange
            this.material.DiffuseTexture = Mock.Of<ITexture2D>();

            // Act
            this.material.Bind(this.pipeline.Object);

            // Assert
            this.pipeline.Verify(x => x.SetTexture(this.material.DiffuseTexture, 0), Times.Once);
        }

        [Test]
        public void BindShouldInvokePipelineSetUniformIntegerWhenInvoked()
        {
            // Arrange
            this.material.DiffuseTexture = Mock.Of<ITexture2D>();

            // Act
            this.material.Bind(this.pipeline.Object);

            // Assert
            this.pipeline.Verify(x => x.SetUniform("u_material.diffuseTexture", 0), Times.Once);
        }

        [Test]
        public void BindShouldSetDiffuseTextureToDefaultDiffuseTextureWhenTextureIsNull()
        {
            // Arrange
            var expected = Mock.Of<ITexture2D>();

            var loader = new Mock<ResourceLoaderBase<ITexture2D>>();
            loader.Setup(x => x.LoadResource("Resources\\Textures\\default_diffuse.png")).Returns(expected);

            ResourceManager.Instance.RegisterLoader<ITexture2D>(loader.Object);

            // Act
            this.material.Bind(this.pipeline.Object);

            // Assert
            Assert.AreSame(expected, this.material.DiffuseTexture);
        }

        [Test]
        public void BindShouldThrowArgumentNullExceptionWhenPipelineIsNull()
        {
            // Act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                this.material.Bind(null);
            });
        }

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenInvoked()
        {
            // Act and assert
            Assert.DoesNotThrow(() =>
            {
                new Material();
            });
        }

        [Test]
        public void DefaultShouldReturnSameMaterial()
        {
            // Arrange
            var expected = Material.Default;

            // Act
            var actual = Material.Default;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void DiffuseTextureShouldReturnNullWhenInvoked()
        {
            // Act
            var actual = this.material.DiffuseTexture;

            // Assert
            Assert.Null(actual);
        }

        [Test]
        public void DiffuseTextureShouldReturnTextureWhenSetToTexture()
        {
            // Arrange
            var expected = Mock.Of<ITexture2D>();
            this.material.DiffuseTexture = expected;

            // Act
            var actual = this.material.DiffuseTexture;

            // Assert
            Assert.AreSame(expected, actual);
        }

        [SetUp]
        public void Setup()
        {
            this.pipeline = new Mock<IPipeline>();
            this.material = new Material();
        }
    }
}
