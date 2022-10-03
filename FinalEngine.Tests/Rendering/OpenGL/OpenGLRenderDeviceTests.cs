// <copyright file="OpenGLRenderDeviceTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Exceptions;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using Moq;
    using NUnit.Framework;
    using OpenTK.Graphics.OpenGL4;

    [SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "This is done in TearDown.")]
    public class OpenGLRenderDeviceTests
    {
        private Mock<IOpenGLInvoker> invoker;

        private OpenGLRenderDevice renderDevice;

        [Test]
        public void ClearShouldInvokeClearColorWhenInvoked()
        {
            // Act
            this.renderDevice.Clear(Color.Cornsilk);

            // Assert
            this.invoker.Verify(x => x.ClearColor(Color.Cornsilk), Times.Once);
        }

        [Test]
        public void ClearShouldInvokeClearDepthWhenInvoked()
        {
            // Act
            this.renderDevice.Clear(Color.Empty, 5, 0);

            // Assert
            this.invoker.Verify(x => x.ClearDepth(5), Times.Once);
        }

        [Test]
        public void ClearShouldInvokeClearStencilWhenInvoked()
        {
            // Act
            this.renderDevice.Clear(Color.Empty, 0, 63);

            // Assert
            this.invoker.Verify(x => x.ClearStencil(63), Times.Once);
        }

        [Test]
        public void ClearShouldInvokeClearWhenInvoked()
        {
            // Act
            this.renderDevice.Clear(Color.Empty);

            // Assert
            this.invoker.Verify(x => x.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit), Times.Once);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLRenderDevice(null));
        }

        [Test]
        public void DisposeShouldNotExecuteWhenAlreadyDisposed()
        {
            // Arrange
            this.renderDevice.Dispose();

            // Act
            this.renderDevice.Dispose();

            // Assert
            this.invoker.Verify(x => x.DeleteVertexArray(0), Times.Once);
        }

        [Test]
        public void DrawIndicesShouldInvokeDrawElementsWhenInvoked()
        {
            // Act
            this.renderDevice.DrawIndices(PrimitiveTopology.TriangleStrip, 2, 5);

            // Assert
            this.invoker.Verify(x => x.DrawElements(PrimitiveType.TriangleStrip, 5, DrawElementsType.UnsignedInt, 2), Times.Once);
        }

        [Test]
        public void FactoryShouldReturnOpenGLGPUResourceFactoryWhenInvoked()
        {
            // Act
            IGPUResourceFactory actual = this.renderDevice.Factory;

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLGPUResourceFactory), actual);
        }

        [Test]
        public void InitializeShouldInvokeBindVertexArrayWhenInvoked()
        {
            // Act
            this.renderDevice.Initialize();

            // Assert
            this.invoker.Verify(x => x.BindVertexArray(0), Times.Once);
        }

        [Test]
        public void InitializeShouldInvokeGenVertexArrayWhenInvoked()
        {
            // Act
            this.renderDevice.Initialize();

            // Assert
            this.invoker.Verify(x => x.GenVertexArray(), Times.Once);
        }

        [Test]
        public void InitializeShouldThrowRenderDeviceInitializationExceptionWhenInitializeAlreadyInvoked()
        {
            // Arrange
            this.renderDevice.Initialize();

            // Act and assert
            Assert.Throws<RenderDeviceInitializationException>(() => this.renderDevice.Initialize());
        }

        [Test]
        public void InputAssemblerShouldReturnOpenGLInputAssemblerWhenInvoked()
        {
            // Act
            IInputAssembler actual = this.renderDevice.InputAssembler;

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLInputAssembler), actual);
        }

        [Test]
        public void OutputMergerShouldReturnOpenGLOutputMergerWhenInvoked()
        {
            // Act
            IOutputMerger actual = this.renderDevice.OutputMerger;

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLOutputMerger), actual);
        }

        [Test]
        public void PipelineShouldReturnOpenGLPipelineWhenInvoked()
        {
            // Act
            IPipeline actual = this.renderDevice.Pipeline;

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLPipeline), actual);
        }

        [Test]
        public void RasterizerShouldReturnOpenGLRasterizerWhenInvoked()
        {
            // Act
            IRasterizer actual = this.renderDevice.Rasterizer;

            // Assert
            Assert.IsInstanceOf(typeof(OpenGLRasterizer), actual);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.renderDevice = new OpenGLRenderDevice(this.invoker.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.renderDevice.Dispose();
        }
    }
}