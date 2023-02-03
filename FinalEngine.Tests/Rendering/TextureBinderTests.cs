// <copyright file="TextureBinderTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering;

using System;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Textures;
using Moq;
using NUnit.Framework;

public class TextureBinderTests
{
    private TextureBinder binder;

    private Mock<IPipeline> pipeline;

    private Mock<ITexture> texture;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenPipelienIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new TextureBinder(null);
        });
    }

    [Test]
    public void GetTextureSlotIndexShouldInvokePipelineSetTextureWhenInvoked()
    {
        // Act
        this.binder.GetTextureSlotIndex(this.texture.Object);

        // Assert
        this.pipeline.Verify(x => x.SetTexture(this.texture.Object, 0), Times.Once);
    }

    [Test]
    public void GetTextureSlotIndexShouldInvokeSetUniformWhenInvoked()
    {
        // Act
        this.binder.GetTextureSlotIndex(this.texture.Object);

        // Assert
        this.pipeline.Verify(x => x.SetUniform("u_textures[0]", 0), Times.Once);
    }

    [Test]
    public void GetTextureSlotIndexShouldReturnOneWhenTwoTexturesHaveBeenAddedAndTheSecondIsRetrieved()
    {
        // Arrange
        const int expected = 1;

        var textureA = new Mock<ITexture>();
        var textureB = new Mock<ITexture>();

        this.binder.GetTextureSlotIndex(textureA.Object);
        this.binder.GetTextureSlotIndex(textureB.Object);

        // Act
        int actual = this.binder.GetTextureSlotIndex(textureB.Object);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void GetTextureSlotIndexShouldReturnTwoWhenTwoTexturesHaveAlreadyBeenAdded()
    {
        // Arrange
        const int expected = 2;

        var textureA = new Mock<ITexture>();
        var textureB = new Mock<ITexture>();

        this.binder.GetTextureSlotIndex(textureA.Object);
        this.binder.GetTextureSlotIndex(textureB.Object);

        // Act
        int actual = this.binder.GetTextureSlotIndex(this.texture.Object);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void GetTextureSlotIndexShouldThrowArgumentNullExceptionWhenTextureIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.binder.GetTextureSlotIndex(null);
        });
    }

    [Test]
    public void ResetShouldCauseGetTextureSlotIndexToReturnZeroOnAlreadyAddedTexture()
    {
        // Arrange
        const int expected = 0;

        var textureA = new Mock<ITexture>();
        var textureB = new Mock<ITexture>();

        this.binder.GetTextureSlotIndex(textureA.Object);
        this.binder.GetTextureSlotIndex(textureB.Object);

        // Act
        this.binder.Reset();

        // Assert
        Assert.AreEqual(expected, this.binder.GetTextureSlotIndex(textureB.Object));
    }

    [SetUp]
    public void Setup()
    {
        this.pipeline = new Mock<IPipeline>();
        this.texture = new Mock<ITexture>();
        this.binder = new TextureBinder(this.pipeline.Object);
    }

    [Test]
    public void ShouldResetShouldReturnFalseWhenAmountOfTexturesAddedIsLessThanMaxTextureSlots()
    {
        // Arrange
        this.pipeline.Setup(x => x.MaxTextureSlots).Returns(5);

        this.binder.GetTextureSlotIndex(this.texture.Object);

        // Act
        bool actual = this.binder.ShouldReset;

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void ShouldResetShouldReturnTrueWhenAmountOfTexturesAddedIsEqualToMaxTextureSlots()
    {
        // Arrange
        this.pipeline.Setup(x => x.MaxTextureSlots).Returns(1);

        this.binder.GetTextureSlotIndex(this.texture.Object);

        // Act
        bool actual = this.binder.ShouldReset;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void ShouldResetShouldReturnTrueWhenAmountOfTexturesAddedIsGreaterThanMaxTextureSlots()
    {
        // Arrange
        this.pipeline.Setup(x => x.MaxTextureSlots).Returns(1);

        this.binder.GetTextureSlotIndex(this.texture.Object);
        this.binder.GetTextureSlotIndex(Mock.Of<ITexture>());

        // Act
        bool actual = this.binder.ShouldReset;

        // Assert
        Assert.True(actual);
    }
}
