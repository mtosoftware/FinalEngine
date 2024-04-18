// <copyright file="ResourceLoaderBaseTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Resources;

using System;
using FinalEngine.Resources;
using Moq;
using NUnit.Framework;

public class ResourceLoaderBaseTests
{
    private const string FilePath = "path\\to\\file";

    private Mock<ResourceLoaderBase<IResource>> resourceLoader;

    [Test]
    public void LoadResourceInternalShouldInvokeLoadResourceWhenFilePathIsNotNullEmptyOrWhitespaceCharacters()
    {
        // Act
        (this.resourceLoader.Object as IResourceLoader)?.LoadResource(FilePath);

        // Assert
        this.resourceLoader.Verify(x => x.LoadResource(FilePath));
    }

    [Test]
    public void LoadResourceInternalShouldThrowArgumentExceptionWhenFilePathIsStringEmpty()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            (this.resourceLoader.Object as IResourceLoader)?.LoadResource(string.Empty);
        });
    }

    [Test]
    public void LoadResourceInternalShouldThrowArgumentExceptionWhenFilePathIsWhitespaceCharacters()
    {
        // Act and assert
        Assert.Throws<ArgumentException>(() =>
        {
            (this.resourceLoader.Object as IResourceLoader)?.LoadResource("\t\r\n");
        });
    }

    [Test]
    public void LoadResourceInternalShouldThrowArgumentNullExceptionWhenFilePathIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            (this.resourceLoader.Object as IResourceLoader)?.LoadResource(null);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.resourceLoader = new Mock<ResourceLoaderBase<IResource>>();
    }
}
