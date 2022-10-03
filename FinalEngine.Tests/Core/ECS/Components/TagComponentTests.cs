// <copyright file="TagComponentTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.ECS.Components
{
    using FinalEngine.ECS.Components;
    using NUnit.Framework;

    [TestFixture]
    public class TagComponentTests
    {
        private TagComponent component;

        [Test]
        public void ConstructorShouldNotThrowExceptionWhenInvoked()
        {
            // Act and assert
            Assert.DoesNotThrow(() => new TagComponent());
        }

        [SetUp]
        public void Setup()
        {
            this.component = new TagComponent();
        }

        [Test]
        public void TagShouldReturnAppleWhenSetToApple()
        {
            // Arrange
            const string Expected = "Apple";
            this.component.Tag = Expected;

            // Act
            var actual = this.component.Tag;

            // Assert
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void TagShouldReturnNullWhenInvoked()
        {
            // Act
            var actual = this.component.Tag;

            // Assert
            Assert.IsNull(actual);
        }
    }
}