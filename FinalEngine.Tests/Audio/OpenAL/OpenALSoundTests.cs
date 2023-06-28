// <copyright file="OpenALSoundTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Audio.OpenAL;

using System;
using FinalEngine.Audio.OpenAL;
using Moq;
using NUnit.Framework;
using ICASLSound = CASL.ISound;

[TestFixture]
public sealed class OpenALSoundTests
{
    private Mock<ICASLSound> caslSound;

    private OpenALSound sound;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSoundIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new OpenALSound(sound: null);
        });
    }

    [Test]
    public void DisposeShouldInvokeCASLSoundDisposeWhenNotDisposed()
    {
        // Act
        this.sound.Dispose();

        // Assert
        this.caslSound.Verify(x => x.Dispose(), Times.Once);
    }

    [Test]
    public void DisposeShouldNotInvokeCASLSoundDisposeWhenDisposed()
    {
        // Arrange
        this.sound.Dispose();
        this.caslSound.Reset();

        // Act
        this.sound.Dispose();

        // Assert
        this.caslSound.Verify(x => x.Dispose(), Times.Never);
    }

    [Test]
    public void IsLoopingGetShouldThrowObjectDisposedExceptionWHenDisposed()
    {
        // Arrange
        this.sound.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            _ = this.sound.IsLooping;
        });
    }

    [Test]
    public void IsLoopingSetShouldSetIsLoopingToTrueWhenSetToTrue()
    {
        // Act
        this.sound.IsLooping = true;

        // Assert
        Assert.That(this.sound.IsLooping, Is.True);
    }

    [Test]
    public void IsLoopingSetShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.sound.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.sound.IsLooping = false;
        });
    }

    [Test]
    public void PauseShouldInvokeCASLSoundPauseWhenInvoked()
    {
        // Act
        this.sound.Pause();

        // Assert
        this.caslSound.Verify(x => x.Pause(), Times.Once);
    }

    [Test]
    public void PauseShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.sound.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.sound.Pause);
    }

    [SetUp]
    public void Setup()
    {
        this.caslSound = new Mock<ICASLSound>();
        this.caslSound.SetupAllProperties();

        this.sound = new OpenALSound(this.caslSound.Object);
    }

    [Test]
    public void StartShouldInvokeCASLSoundPauseWhenInvoked()
    {
        // Act
        this.sound.Start();

        // Assert
        this.caslSound.Verify(x => x.Play(), Times.Once);
    }

    [Test]
    public void StartShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.sound.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.sound.Start);
    }

    [Test]
    public void StopShouldInvokeCASLSoundPauseWhenInvoked()
    {
        // Act
        this.sound.Stop();

        // Assert
        this.caslSound.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    public void StopShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.sound.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(this.sound.Stop);
    }

    [Test]
    public void VolumeGetShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.sound.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            _ = this.sound.Volume;
        });
    }

    [Test]
    public void VolumeSetShouldSetVolumeToOneWhenSetToOne()
    {
        // Arrange
        const float expected = 1.0f;

        // Act
        this.sound.Volume = expected;

        // Assert
        Assert.That(this.sound.Volume, Is.EqualTo(expected));
    }

    [Test]
    public void VolumeSetShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.sound.Dispose();

        // Act and assert
        Assert.Throws<ObjectDisposedException>(() =>
        {
            this.sound.Volume = 1.0f;
        });
    }
}
