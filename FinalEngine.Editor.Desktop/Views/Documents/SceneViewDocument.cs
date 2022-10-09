// <copyright file="SceneViewDocument.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Documents
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using FinalEngine.Editor.Desktop.Controls;
    using FinalEngine.Editor.Presenters;
    using FinalEngine.Editor.Views.Documents;
    using FinalEngine.Editor.Views.Events;
    using FinalEngine.Runtime;
    using FinalEngine.Runtime.Invocation;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.WinForms;

    public partial class SceneViewDocument : TogglableDocument, ISceneViewDocumentView, ISynchronizeInvoke
    {
        private readonly IPresenterFactory presenterFactory;

        [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "This occurs in SceneViewDocument_Disposed")]
        private GLControl? glControl;

        public SceneViewDocument(IPresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory ?? throw new ArgumentNullException(nameof(presenterFactory));
            this.InitializeComponent();
        }

        public event EventHandler<EventArgs>? OnLoaded;

        public event EventHandler<EventArgs>? OnRender;

        public event EventHandler<SizeChangedEventArgs>? OnResized;

        private void AddOpenGLControl()
        {
            //// TODO: Move the GL Control into the designer when possible.
            var settings = new GLControlSettings()
            {
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 5),
                AutoLoadBindings = true,
                Flags = ContextFlags.ForwardCompatible,
                IsEventDriven = true,
                Profile = ContextProfile.Core,
            };

            this.glControl = new GLControl(settings)
            {
                Dock = DockStyle.Fill,
            };

            int vao = -1;

            this.glControl.Load += (s, e) =>
            {
                this.glControl.MakeCurrent();

                vao = GL.GenVertexArray();
                GL.BindVertexArray(vao);
            };

            this.glControl.Disposed += (s, e) =>
            {
                GL.DeleteVertexArray(vao);
            };

            this.glControl.Paint += (s, e) =>
            {
                this.glControl.MakeCurrent();
                this.OnRender?.Invoke(this, EventArgs.Empty);
                this.glControl.SwapBuffers();
            };

            this.Controls.Add(this.glControl);
        }

        private void RunTimeLoop()
        {
            var watch = new Stopwatch();
            var invoker = new StopwatchInvoker(watch);
            var gameTime = new GameTime(invoker, 60.0d);

            bool isRunning = true;

            while (isRunning)
            {
                if (!gameTime.CanProcessNextFrame())
                {
                    continue;
                }

                try
                {
                    this.Invoke(new Action(() =>
                    {
                        this.glControl?.Invalidate();
                    }));
                }
                catch (ObjectDisposedException)
                {
                    isRunning = false;
                }
            }
        }

        private void SceneViewDocument_Disposed(object? sender, EventArgs e)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (this.Disposing)
            {
                if (this.glControl != null)
                {
                    this.glControl.Dispose();
                    this.glControl = null;
                }
            }
        }

        private void SceneViewDocument_Load(object sender, EventArgs e)
        {
            this.Tag = this.presenterFactory.CreateSceneViewDocumentPresenter(this);
            this.Disposed += this.SceneViewDocument_Disposed;

            this.AddOpenGLControl();

            Task.Factory.StartNew(
                this.RunTimeLoop,
                CancellationToken.None,
                TaskCreationOptions.RunContinuationsAsynchronously,
                TaskScheduler.Current);

            this.OnLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void SceneViewDocument_Resize(object sender, EventArgs e)
        {
            this.glControl?.MakeCurrent();
            this.OnResized?.Invoke(this, new SizeChangedEventArgs(this.glControl?.ClientRectangle ?? Rectangle.Empty));
        }
    }
}
