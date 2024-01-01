// <copyright file="MaterialTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Geometry;

using System;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class MaterialTests
{
    private Mock<ITexture2D> defaultDiffuseTexture;

    private Mock<ITexture2D> defaultNormalTexture;

    private Mock<ITexture2D> defaultSpecularTexture;

    private Material material;

    private Mock<IPipeline> pipeline;

    private Mock<ResourceLoaderBase<ITexture2D>> resourceLoader;

    [Test]
    public void BindShouldInvokeSetDiffuseTextureWhenInvoked()
    {
        // Act
        this.material.Bind(this.pipeline.Object);

        // Assert
        this.pipeline.Verify(x => x.SetTexture(this.material.DiffuseTexture, 0), Times.Once);
    }

    [Test]
    public void BindShouldInvokeSetNormalTextureWhenInvoked()
    {
        // Act
        this.material.Bind(this.pipeline.Object);

        // Assert
        this.pipeline.Verify(x => x.SetTexture(this.material.NormalTexture, 2), Times.Once);
    }

    [Test]
    public void BindShouldInvokeSetSpecularTextureWhenInvoked()
    {
        // Act
        this.material.Bind(this.pipeline.Object);

        // Assert
        this.pipeline.Verify(x => x.SetTexture(this.material.SpecularTexture, 1), Times.Once);
    }

    [Test]
    public void BindShouldInvokeSetUniformDiffuseTextureWhenInvoked()
    {
        // Act
        this.material.Bind(this.pipeline.Object);

        // Assert
        this.pipeline.Verify(x => x.SetUniform("u_material.diffuseTexture", 0), Times.Once);
    }

    [Test]
    public void BindShouldInvokeSetUniformNormalTextureWhenInvoked()
    {
        // Act
        this.material.Bind(this.pipeline.Object);

        // Assert
        this.pipeline.Verify(x => x.SetUniform("u_material.normalTexture", 2), Times.Once);
    }

    [Test]
    public void BindShouldInvokeSetUniformSpecularTextureWhenInvoked()
    {
        // Act
        this.material.Bind(this.pipeline.Object);

        // Assert
        this.pipeline.Verify(x => x.SetUniform("u_material.specularTexture", 1), Times.Once);
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
    public void DiffuseTextureShouldReturnAssignedTextureWhenSet()
    {
        // Arrange
        var texture = new Mock<ITexture2D>();

        // Act
        this.material.DiffuseTexture = texture.Object;

        // Assert
        Assert.That(this.material.DiffuseTexture, Is.EqualTo(texture.Object));
    }

    [Test]
    public void DiffuseTextureShouldReturnDefaultDiffuseTextureWhenNotAssigned()
    {
        // Arrange
        this.material.DiffuseTexture = null;

        // Act
        var actual = this.material.DiffuseTexture;

        // Assert
        Assert.That(actual, Is.EqualTo(ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png")));
    }

    [Test]
    public void NormalTextureShouldReturnAssignedTextureWhenSet()
    {
        // Arrange
        var texture = new Mock<ITexture2D>();

        // Act
        this.material.NormalTexture = texture.Object;

        // Assert
        Assert.That(this.material.NormalTexture, Is.EqualTo(texture.Object));
    }

    [Test]
    public void NormalTextureShouldReturnDefaultNormalTextureWhenNotAssigned()
    {
        // Arrange
        this.material.NormalTexture = null;

        // Act
        var actual = this.material.NormalTexture;

        // Assert
        Assert.That(actual, Is.EqualTo(ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_normal.png")));
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        this.resourceLoader = new Mock<ResourceLoaderBase<ITexture2D>>();

        this.defaultDiffuseTexture = new Mock<ITexture2D>();
        this.defaultSpecularTexture = new Mock<ITexture2D>();
        this.defaultNormalTexture = new Mock<ITexture2D>();

        this.resourceLoader.Setup(x => x.LoadResource("Resources\\Textures\\default_diffuse.png")).Returns(this.defaultDiffuseTexture.Object);
        this.resourceLoader.Setup(x => x.LoadResource("Resources\\Textures\\default_specular.png")).Returns(this.defaultSpecularTexture.Object);
        this.resourceLoader.Setup(x => x.LoadResource("Resources\\Textures\\default_normal.png")).Returns(this.defaultNormalTexture.Object);

        ResourceManager.Instance.RegisterLoader(this.resourceLoader.Object);
    }

    [SetUp]
    public void Setup()
    {
        this.material = new Material();
        this.pipeline = new Mock<IPipeline>();
    }

    [Test]
    public void ShininessShouldReturnDefaultValueWhenInvoked()
    {
        // Arrange
        const float expected = 32.0f;

        // Act
        float actual = this.material.Shininess;

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
    }

    [Test]
    public void SpecularTextureShouldReturnAssignedTextureWhenSet()
    {
        // Arrange
        var texture = new Mock<ITexture2D>();

        // Act
        this.material.SpecularTexture = texture.Object;

        // Assert
        Assert.That(this.material.SpecularTexture, Is.EqualTo(texture.Object));
    }

    [Test]
    public void SpecularTextureShouldReturnDefaultSpecularTextureWhenNotAssigned()
    {
        // Arrange
        this.material.SpecularTexture = null;

        // Act
        var actual = this.material.SpecularTexture;

        // Assert
        Assert.That(actual, Is.EqualTo(ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_specular.png")));
    }
}
