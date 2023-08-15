// <copyright file="FileNameAttributeTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.ViewModels.Validation;

using System;
using System.IO.Abstractions.TestingHelpers;
using FinalEngine.Editor.ViewModels.Validation;
using NUnit.Framework;

[TestFixture]
public sealed class FileNameAttributeTests
{
    private FileNameAttribute attribute;

    private MockFileSystem fileSystem;

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenInvoked()
    {
        // Assert
        Assert.DoesNotThrow(() =>
        {
            new FileNameAttribute();
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new FileNameAttribute(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.fileSystem = new MockFileSystem();
        this.attribute = new FileNameAttribute(this.fileSystem);
    }

    [Test]
    public void ValidateShouldFailWhenValueIsEmpty()
    {
        // Act
        bool result = this.attribute.IsValid(string.Empty);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateShouldFailWhenValueIsNotValidFileName()
    {
        // Act
        bool result = this.attribute.IsValid("<fileName>#$");

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateShouldFailWhenValueIsNull()
    {
        // Act
        bool result = this.attribute.IsValid(null);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateShouldFailWhenValueIsWhitespace()
    {
        // Act
        bool result = this.attribute.IsValid("\r\t\n ");

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateShouldSucceedWhenValueIsValidFileName()
    {
        // Act
        bool result = this.attribute.IsValid("fileName.txt");

        // Assert
        Assert.That(result, Is.True);
    }
}
