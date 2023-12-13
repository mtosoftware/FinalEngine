// <copyright file="MaterialTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
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

    private Material material;

    private Mock<IPipeline> pipeline;

    private Mock<ResourceLoaderBase<ITexture2D>> resourceLoader;

    [Test]
    public void BindShouldInvokeSetTextureWhenInvoked()
    {
        // Act
        this.material.Bind(this.pipeline.Object);

        // Assert
        this.pipeline.Verify(x => x.SetTexture(this.material.DiffuseTexture, 0), Times.Once);
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

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        this.resourceLoader = new Mock<ResourceLoaderBase<ITexture2D>>();
        this.defaultDiffuseTexture = new Mock<ITexture2D>();

        this.resourceLoader.Setup(x => x.LoadResource("Resources\\Textures\\default_diffuse.png")).Returns(this.defaultDiffuseTexture.Object);

        ResourceManager.Instance.RegisterLoader(this.resourceLoader.Object);
    }

    [SetUp]
    public void Setup()
    {
        this.material = new Material();
        this.pipeline = new Mock<IPipeline>();
    }
}
