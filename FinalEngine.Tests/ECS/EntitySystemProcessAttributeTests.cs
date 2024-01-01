// <copyright file="EntitySystemProcessAttributeTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.ECS;

using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using NUnit.Framework;

[TestFixture]
public class EntitySystemProcessAttributeTests
{
    [Test]
    public void ExecutionTypeShouldReturnRenderWhenSetToRender()
    {
        // Arrange
        var attribute = new EntitySystemProcessAttribute()
        {
            ExecutionType = GameLoopType.Render,
        };

        // Act
        var actual = attribute.ExecutionType;

        // Assert
        Assert.That(actual, Is.EqualTo(GameLoopType.Render));
    }

    [Test]
    public void ExecutionTypeShouldReturnUpdateWhenNotSet()
    {
        // Arrange
        var attribute = new EntitySystemProcessAttribute();

        // Act
        var actual = attribute.ExecutionType;

        // Assert
        Assert.That(actual, Is.EqualTo(GameLoopType.Update));
    }
}
