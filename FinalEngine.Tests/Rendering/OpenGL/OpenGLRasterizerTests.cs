// <copyright file="OpenGLRasterizerTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL
{
    using System;
    using System.Drawing;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Utilities;
    using Moq;
    using NUnit.Framework;
    using OpenTK.Graphics.OpenGL4;

    public class OpenGLRasterizerTests
    {
        private Mock<IOpenGLInvoker> invoker;

        private Mock<IEnumMapper> mapper;

        private OpenGLRasterizer rasterizer;

        private RasterStateDescription rasterState;

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OpenGLRasterizer(null, this.mapper.Object);
            });
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new OpenGLRasterizer(this.invoker.Object, null);
            });
        }

        [Test]
        public void GetViewportShouldInvokeGetIntegerWhenInvoked()
        {
            // Act
            this.rasterizer.GetViewport();

            // Assert
            this.invoker.Verify(x => x.GetInteger(GetIndexedPName.Viewport, 0, It.IsAny<int[]>()));
        }

        [Test]
        public void GetViewportShouldReturnViewportWhenInvoked()
        {
            // Act
            var expected = new Rectangle(0, 0, 0, 0);
            var actual = this.rasterizer.GetViewport();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SetRasterStateShouldInvokeCullFaceWhenInvoked()
        {
            // Arrange
            this.rasterState.CullMode = FaceCullMode.Front;

            // Act
            this.rasterizer.SetRasterState(this.rasterState);

            // Assert
            this.invoker.Verify(x => x.CullFace(this.mapper.Object.Forward<CullFaceMode>(FaceCullMode.Front)), Times.Once);
        }

        [Test]
        public void SetRasterStateShouldInvokeFrontFaceWhenInvoked()
        {
            // Arrange
            this.rasterState.WindingDirection = WindingDirection.CounterClockwise;

            // Act
            this.rasterizer.SetRasterState(this.rasterState);

            // Assert
            this.invoker.Verify(x => x.FrontFace(this.mapper.Object.Forward<FrontFaceDirection>(WindingDirection.CounterClockwise)), Times.Once);
        }

        [Test]
        public void SetRasterStateShouldInvokePolygonModeWhenInvoked()
        {
            // Arrange
            this.rasterState.FillMode = RasterMode.Wireframe;

            // Act
            this.rasterizer.SetRasterState(this.rasterState);

            // Assert
            this.invoker.Verify(x => x.PolygonMode(MaterialFace.FrontAndBack, this.mapper.Object.Forward<PolygonMode>(RasterMode.Wireframe)), Times.Once);
        }

        [Test]
        public void SetRasterStateShouldInvokeSwitchCullFaceFalseWhenCullEnabledIsFalse()
        {
            // Arrange
            this.rasterState.CullEnabled = false;

            // Act
            this.rasterizer.SetRasterState(this.rasterState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.CullFace, false), Times.Once);
        }

        [Test]
        public void SetRasterStateShouldInvokeSwitchCullFaceTrueWhenCullEnabledIsTrue()
        {
            // Arrange
            this.rasterState.CullEnabled = true;

            // Act
            this.rasterizer.SetRasterState(this.rasterState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.CullFace, true), Times.Once);
        }

        [Test]
        public void SetRasterStateShouldInvokeSwitchScissorTestFalseWhenScissorEnabledIsFalse()
        {
            // Arrange
            this.rasterState.ScissorEnabled = false;

            // Act
            this.rasterizer.SetRasterState(this.rasterState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.ScissorTest, false), Times.Once);
        }

        [Test]
        public void SetRasterStateShouldInvokeSwitchScissorTestTrueWhenScissorEnabledIsTrue()
        {
            // Arrange
            this.rasterState.ScissorEnabled = true;

            // Act
            this.rasterizer.SetRasterState(this.rasterState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.ScissorTest, true), Times.Once);
        }

        [Test]
        public void SetScissorShouldInvokeScissorWhenInvoked()
        {
            // Arrange
            var rectangle = new Rectangle(10, 54, 364, 251);

            // Act
            this.rasterizer.SetScissor(rectangle);

            // Assert
            this.invoker.Verify(x => x.Scissor(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), Times.Once);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.mapper = new Mock<IEnumMapper>();
            this.rasterizer = new OpenGLRasterizer(this.invoker.Object, this.mapper.Object);

            this.rasterState = default;
        }

        [Test]
        public void SetViewportShouldInvokeDepthRangeNearZeroFarOneWhenInvoked()
        {
            // Act
            this.rasterizer.SetViewport(Rectangle.Empty);

            // Assert
            this.invoker.Verify(x => x.DepthRange(0.0f, 1.0f), Times.Once);
        }

        [Test]
        public void SetViewportShouldInvokeDepthRangeWhenInvoked()
        {
            // Arrange
            const float near = 403.0f;
            const float far = 159.0f;

            // Act
            this.rasterizer.SetViewport(Rectangle.Empty, near, far);

            // Assert
            this.invoker.Verify(x => x.DepthRange(near, far), Times.Once);
        }

        [Test]
        public void SetViewportShouldInvokeViewportWhenInvoked()
        {
            // Arrange
            var rectangle = new Rectangle(124, 543, 5390, 3854);

            // Act
            this.rasterizer.SetViewport(rectangle);

            // Assert
            this.invoker.Verify(x => x.Viewport(rectangle), Times.Once);
        }
    }
}
