// <copyright file="OpenGLOutputMergerTests.cs" company="Software Antics">
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
    using BlendEquationMode = FinalEngine.Rendering.BlendEquationMode;
    using TKBlendEquationMode = OpenTK.Graphics.OpenGL4.BlendEquationMode;

    public class OpenGLOutputMergerTests
    {
        private BlendStateDescription blendState;

        private DepthStateDescription depthState;

        private Mock<IOpenGLInvoker> invoker;

        private Mock<IEnumMapper> mapper;

        private OpenGLOutputMerger outputMerger;

        private StencilStateDescription stencilState;

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLOutputMerger(null, this.mapper.Object));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLOutputMerger(this.invoker.Object, null));
        }

        [Test]
        public void SetBlendStateShouldInvokeBlendColorWhenInvoked()
        {
            // Arrange
            this.blendState.Color = Color.Coral;

            // Act
            this.outputMerger.SetBlendState(this.blendState);

            // Assert
            this.invoker.Verify(x => x.BlendColor(Color.Coral), Times.Once);
        }

        [Test]
        public void SetBlendStateShouldInvokeBlendEquationWhenInvoked()
        {
            // Arrange
            this.blendState.EquationMode = BlendEquationMode.Add;

            // Act
            this.outputMerger.SetBlendState(this.blendState);

            // Assert
            this.invoker.Verify(x => x.BlendEquation(this.mapper.Object.Forward<TKBlendEquationMode>(BlendEquationMode.Add)), Times.Once);
        }

        [Test]
        public void SetBlendStateShouldInvokeBlendFuncWhenInvoked()
        {
            // Arrange
            this.blendState.SourceMode = BlendMode.DestinationAlpha;
            this.blendState.DestinationMode = BlendMode.One;

            // Act
            this.outputMerger.SetBlendState(this.blendState);

            // Assert
            this.invoker.Verify(x => x.BlendFunc(this.mapper.Object.Forward<BlendingFactor>(BlendMode.DestinationAlpha), this.mapper.Object.Forward<BlendingFactor>(BlendMode.One)), Times.Once);
        }

        [Test]
        public void SetBlendStateShouldInvokeSwitchBlendFalseWhenEnabledIsFalse()
        {
            // Arrange
            this.blendState.Enabled = false;

            // Act
            this.outputMerger.SetBlendState(this.blendState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.Blend, false), Times.Once);
        }

        [Test]
        public void SetBlendStateShouldInvokeSwitchBlendTrueWhenEnabledIsTrue()
        {
            // Arrange
            this.blendState.Enabled = true;

            // Act
            this.outputMerger.SetBlendState(this.blendState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.Blend, true), Times.Once);
        }

        [Test]
        public void SetDepthStateShouldInvokeDepthFuncWhenInvoked()
        {
            // Arrange
            this.depthState.ComparisonMode = ComparisonMode.GreaterEqual;

            // Act
            this.outputMerger.SetDepthState(this.depthState);

            // Assert
            this.invoker.Verify(x => x.DepthFunc(this.mapper.Object.Forward<DepthFunction>(ComparisonMode.GreaterEqual)), Times.Once);
        }

        [Test]
        public void SetDepthStateShouldInvokeDepthMaskFalseWhenWriteEnabeldIsFalse()
        {
            // Arrange
            this.depthState.WriteEnabled = false;

            // ACt
            this.outputMerger.SetDepthState(this.depthState);

            // Assert
            this.invoker.Verify(x => x.DepthMask(false), Times.Once);
        }

        [Test]
        public void SetDepthStateShouldInvokeDepthMaskTrueWhenWriteEnabledIsTrue()
        {
            // Arrange
            this.depthState.WriteEnabled = true;

            // Act
            this.outputMerger.SetDepthState(this.depthState);

            // Assert
            this.invoker.Verify(x => x.DepthMask(true), Times.Once);
        }

        [Test]
        public void SetDepthStateShouldInvokeSwitchDepthTestFalseWhenReadEnabledIsFalse()
        {
            // Arrange
            this.depthState.ReadEnabled = false;

            // Act
            this.outputMerger.SetDepthState(this.depthState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.DepthTest, false), Times.Once);
        }

        [Test]
        public void SetDepthStateShouldInvokeSwitchDepthTestTrueWhenReadEnabledIsTrue()
        {
            // Arrange
            this.depthState.ReadEnabled = true;

            // Act
            this.outputMerger.SetDepthState(this.depthState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.DepthTest, true), Times.Once);
        }

        [Test]
        public void SetStencilStateShouldInvokeStencilFuncWhenInvoked()
        {
            // Arrange
            this.stencilState.ComparisonMode = ComparisonMode.Equal;
            this.stencilState.ReferenceValue = 120;
            this.stencilState.ReadMask = 567;

            // Act
            this.outputMerger.SetStencilState(this.stencilState);

            // Assert
            this.invoker.Verify(x => x.StencilFunc(this.mapper.Object.Forward<StencilFunction>(ComparisonMode.Equal), 120, 567), Times.Once);
        }

        [Test]
        public void SetStencilStateShouldInvokeStencilMaskWhenInvoked()
        {
            // Arrange
            this.stencilState.WriteMask = 45;

            // Act
            this.outputMerger.SetStencilState(this.stencilState);

            // Assert
            this.invoker.Verify(x => x.StencilMask(45), Times.Once);
        }

        [Test]
        public void SetStencilStateShouldInvokeStencilOpWhenInvoked()
        {
            // Arrange
            this.stencilState.StencilFail = StencilOperation.Decrement;
            this.stencilState.DepthFail = StencilOperation.Increment;
            this.stencilState.Pass = StencilOperation.Replace;

            // Act
            this.outputMerger.SetStencilState(this.stencilState);

            // Assert
            this.invoker.Verify(x => x.StencilOp(this.mapper.Object.Forward<StencilOp>(StencilOperation.Decrement), this.mapper.Object.Forward<StencilOp>(StencilOperation.Increment), this.mapper.Object.Forward<StencilOp>(StencilOperation.Replace)), Times.Once);
        }

        [Test]
        public void SetStencilStateShouldInvokeSwitchStencilTestFalseWhenEnabledIsFalse()
        {
            // Arrange
            this.stencilState.Enabled = false;

            // Act
            this.outputMerger.SetStencilState(this.stencilState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.StencilTest, false), Times.Once);
        }

        [Test]
        public void SetStencilStateShouldInvokeSwitchStencilTestTrueWhenEnabledIsTrue()
        {
            // Arrange
            this.stencilState.Enabled = true;

            // Act
            this.outputMerger.SetStencilState(this.stencilState);

            // Assert
            this.invoker.Verify(x => x.Cap(EnableCap.StencilTest, true), Times.Once);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.mapper = new Mock<IEnumMapper>();
            this.outputMerger = new OpenGLOutputMerger(this.invoker.Object, this.mapper.Object);

            this.blendState = default;
            this.depthState = default;
            this.stencilState = default;
        }
    }
}