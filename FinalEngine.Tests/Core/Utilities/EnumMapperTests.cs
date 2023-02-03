// <copyright file="EnumMapperTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Utilities;

using System;
using System.Collections.Generic;
using FinalEngine.Utilities;
using NUnit.Framework;

public enum TestEnumerationA
{
    A,

    B,

    C,
}

public enum TestEnumerationB
{
    D,

    E,

    F,
}

public class EnumMapperTests
{
    private IReadOnlyDictionary<Enum, Enum> forwardToReverseMap;

    private EnumMapper mapper;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenForwardToReverseMapIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new EnumMapper(null);
        });
    }

    [Test]
    public void ForwardShouldReturnCorrectMappedKeyWhenInvoked()
    {
        // Act
        var actual = this.mapper.Forward<TestEnumerationB>(TestEnumerationA.B);

        // Assert
        Assert.AreEqual(TestEnumerationB.E, actual);
    }

    [Test]
    public void ForwardShouldThrowArgumentNullExceptionWhenEnumerationIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.mapper.Forward<Enum>(null);
        });
    }

    [Test]
    public void ForwardShouldThrowKeyNotFoundExceptionWhenEnumerationIsNotMapped()
    {
        // Act and assert
        Assert.Throws<KeyNotFoundException>(() =>
        {
            this.mapper.Forward<TestEnumerationB>(TestEnumerationA.A);
        });
    }

    [Test]
    public void ReverseShouldReturnCorrectMappedKeyWhenInvoked()
    {
        // Act
        var actual = this.mapper.Reverse<TestEnumerationA>(TestEnumerationB.E);

        // Assert
        Assert.AreEqual(TestEnumerationA.B, actual);
    }

    [Test]
    public void ReverseShouldThrowArgumentNullExceptionWhenEnumerationIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.mapper.Reverse<Enum>(null);
        });
    }

    [Test]
    public void ReverseShouldThrowKeyNotFoundExceptionWhenEnumerationIsNotMapped()
    {
        // Act and assert
        Assert.Throws<KeyNotFoundException>(() =>
        {
            this.mapper.Reverse<TestEnumerationA>(TestEnumerationB.D);
        });
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.forwardToReverseMap = new Dictionary<Enum, Enum>()
        {
            { TestEnumerationA.B, TestEnumerationB.E },
            { TestEnumerationA.C, TestEnumerationB.F },
        };

        this.mapper = new EnumMapper(this.forwardToReverseMap);
    }
}
