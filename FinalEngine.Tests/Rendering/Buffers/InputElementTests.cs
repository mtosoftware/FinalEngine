// <copyright file="InputElementTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Buffers
{
    using FinalEngine.Rendering.Buffers;
    using NUnit.Framework;

    public class InputElementTests
    {
        private const int Index = 234;

        private const int RelativeOffset = 304;

        private const int Size = 432;

        private const InputElementType Type = InputElementType.Float;

        private InputElement element;

        [Test]
        public void ConstructorShouldSetIndexToParameterWhenInvoked()
        {
            // Act
            int actual = this.element.Index;

            // Assert
            Assert.AreEqual(Index, actual);
        }

        [Test]
        public void ConstructorShouldSetRelativeOffsetToParameterWhenInvoked()
        {
            // Act
            int actual = this.element.RelativeOffset;

            // Assert
            Assert.AreEqual(RelativeOffset, actual);
        }

        [Test]
        public void ConstructorShouldSetSizeToParameterWhenInvoked()
        {
            // Act
            int actual = this.element.Size;

            // Assert
            Assert.AreEqual(Size, actual);
        }

        [Test]
        public void ConstructorShouldSetTypeToParameterWhenInvoked()
        {
            // Act
            InputElementType actual = this.element.Type;

            // Assert
            Assert.AreEqual(Type, actual);
        }

        [Test]
        public void EqualityOperatorShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new InputElement(1, 2, InputElementType.Byte, 2);
            var right = new InputElement(2, 3, InputElementType.Double, 4);

            // Act
            bool actual = left == right;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualityOperatorShouldReturnTrueWhenPropertiesMatch()
        {
            // Arrange
            var left = new InputElement(1, 2, InputElementType.Byte, 2);
            var right = new InputElement(1, 2, InputElementType.Byte, 2);

            // Act
            bool actual = left == right;

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNotInputElement()
        {
            // Act
            bool actual = this.element.Equals(new object());

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenObjectIsNull()
        {
            // Act
            bool actual = this.element.Equals(null);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnFalseWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new InputElement(5, 7, InputElementType.Float, 2);
            var right = new InputElement(2, 3, InputElementType.Double, 33);

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void EqualsShouldReturnTrueWhenObjectIsInputElementAndHasSameProperties()
        {
            // Arrange
            var left = new InputElement(1, 2, InputElementType.Byte, 2);
            object right = new InputElement(1, 2, InputElementType.Byte, 2);

            // Act
            bool actual = left.Equals(right);

            // Assert
            Assert.True(actual);
        }

        [Test]
        public void GetHashCodeShouldReturnSameAsOtherObjectWhenPropertiesAreEqual()
        {
            // Arrange
            var left = new InputElement(1, 2, InputElementType.Byte, 2);
            var right = new InputElement(1, 2, InputElementType.Byte, 2);

            // Act
            int leftHashCode = left.GetHashCode();
            int rightHashCode = right.GetHashCode();

            // Assert
            Assert.AreEqual(leftHashCode, rightHashCode);
        }

        [Test]
        public void InEqualityOperatorShouldReturnFalseWhenPropertiesMatch()
        {
            // Arrange
            var left = new InputElement(1, 2, InputElementType.Byte, 2);
            var right = new InputElement(1, 2, InputElementType.Byte, 2);

            // Act
            bool actual = left != right;

            // Assert
            Assert.False(actual);
        }

        [Test]
        public void InEqualityOperatorShouldReturnTrueWhenPropertiesDontMatch()
        {
            // Arrange
            var left = new InputElement(1, 5, InputElementType.Int, 2);
            var right = new InputElement(7, 2, InputElementType.Byte, 277542);

            // Act
            bool actual = left != right;

            // Assert
            Assert.True(actual);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.element = new InputElement(Index, Size, Type, RelativeOffset);
        }
    }
}