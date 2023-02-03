// <copyright file="SpriteBatcherTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Buffers;
using Moq;
using NUnit.Framework;

public class SpriteBatcherTests
{
    private const int MaxCapacity = 1000;

    private SpriteBatcher batcher;

    private Mock<IInputAssembler> inputAssembler;

    private Mock<IVertexBuffer> vertexBuffer;

    [Test]
    public void BatchShouldIncrementCurrentIndexCountBySixWhenInvoked()
    {
        // Arrange
        const int incrementer = 10;

        // Act
        for (int i = 0; i < incrementer; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Assert
        Assert.AreEqual(this.batcher.CurrentIndexCount, incrementer * 6);
    }

    [Test]
    public void BatchShouldIncrementCurrentVertexCountByFourWhenInvoked()
    {
        // Arrange
        const int incrementer = 10;

        // Act
        for (int i = 0; i < incrementer; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Assert
        Assert.AreEqual(this.batcher.CurrentVertexCount, incrementer * 4);
    }

    [Test]
    public void ConstructorShouldSetMaxIndexCountToSixThousandWhenInvoked()
    {
        // Arrange
        const int expected = MaxCapacity * 6;

        // Assert
        Assert.AreEqual(expected, this.batcher.MaxIndexCount);
    }

    [Test]
    public void ConstructorShouldSetMaxVertexCountToFourThousandWhenInvoked()
    {
        // Arrange
        const int expected = MaxCapacity * 4;

        // Assert
        Assert.AreEqual(expected, this.batcher.MaxVertexCount);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenInputAssemblerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SpriteBatcher(null, MaxCapacity);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentOutOfRangeExceptionWhenMaxCapacityIsEqualToZero()
    {
        // Act and assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new SpriteBatcher(this.inputAssembler.Object, 0);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentOutOfRangeExceptionWhenMaxCapacityIsLessThanZero()
    {
        // Act and assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            new SpriteBatcher(this.inputAssembler.Object, -1);
        });
    }

    [Test]
    public void ResetShouldSetCurrentIndexCountToZeroWhenInvoked()
    {
        // Arrange
        for (int i = 0; i < MaxCapacity; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Act
        this.batcher.Reset();

        // Assert
        Assert.Zero(this.batcher.CurrentIndexCount);
    }

    [Test]
    public void ResetShouldSetCurrentVertexCountToZeroWhenInvoked()
    {
        // Arrange
        for (int i = 0; i < MaxCapacity; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Act
        this.batcher.Reset();

        // Assert
        Assert.Zero(this.batcher.CurrentVertexCount);
    }

    [Test]
    public void ResetShouldSetShouldResetToFalseWhenInvoked()
    {
        // Arrange
        for (int i = 0; i < MaxCapacity; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Act
        this.batcher.Reset();

        // Assert
        Assert.False(this.batcher.ShouldReset);
    }

    [SetUp]
    public void Setup()
    {
        this.inputAssembler = new Mock<IInputAssembler>();
        this.vertexBuffer = new Mock<IVertexBuffer>();
        this.batcher = new SpriteBatcher(this.inputAssembler.Object, MaxCapacity);
    }

    [Test]
    public void ShouldResetShouldReturnFalseWhenBatchHasBeenCalledLessThanMaxVertexCount()
    {
        // Act
        for (int i = 0; i < MaxCapacity - 1; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Assert
        Assert.False(this.batcher.ShouldReset);
    }

    [Test]
    public void ShouldResetShouldReturnTrueWhenBatchHasBeenCalledEqualToMaxVertexCount()
    {
        // Act
        for (int i = 0; i < MaxCapacity; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Assert
        Assert.True(this.batcher.ShouldReset);
    }

    [Test]
    public void ShouldResetShouldReturnTrueWhenBatchHasBeenCalledGreaterThanMaxVertexCount()
    {
        // Act
        for (int i = 0; i < MaxCapacity + 1; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Assert
        Assert.True(this.batcher.ShouldReset);
    }

    [Test]
    public void UpdateShouldInvokeUpdateVertexBufferWhenVertexBufferIsNotNull()
    {
        // Arrange
        for (int i = 0; i < MaxCapacity + 1; i++)
        {
            this.batcher.Batch(0, Color.White, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero, 0, 0);
        }

        // Act
        this.batcher.Update(this.vertexBuffer.Object);

        // Assert
        this.inputAssembler.Verify(x => x.UpdateVertexBuffer(this.vertexBuffer.Object, It.IsAny<IReadOnlyCollection<SpriteVertex>>(), SpriteVertex.SizeInBytes));
    }

    [Test]
    public void UpdateShouldThrowArgumentNullExceptionWhenVertexBufferIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.batcher.Update(null);
        });
    }
}
