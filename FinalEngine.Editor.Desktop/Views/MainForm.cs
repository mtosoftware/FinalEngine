// <copyright file="MainForm.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views
{
    using System;
    using System.Windows.Forms;
    using DarkUI.Forms;
    using FinalEngine.Editor.Desktop;
    using FinalEngine.Editor.Desktop.Views.Documents;
    using FinalEngine.Editor.Desktop.Views.Tools;
    using FinalEngine.Editor.Presenters;
    using FinalEngine.Editor.Views;
    using FinalEngine.Editor.Views.Events;

    public partial class MainForm : DarkForm, IMainView, IApplicationStarter
    {
        private readonly IPresenterFactory presenterFactory;

        private ConsoleToolWindow? consoleToolWindow;

        private EntityInspectorToolWindow? entityInspectorToolWindow;

        private EntitySystemsToolWindow? entitySystemsToolWindow;

        private SceneHierarchyToolWindow? sceneHierachyToolWindow;

        private SceneViewDocument? sceneViewDocument;

        public MainForm(IPresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory ?? throw new ArgumentNullException(nameof(presenterFactory));
            this.InitializeComponent();
        }

        public event EventHandler<ContentToggledEventArgs>? OnContentToggled;

        public event EventHandler<EventArgs>? OnExiting;

        public event EventHandler<EventArgs>? OnLoaded;

        public string StatusText
        {
            get { return this.statusLabel.Text; }
            set { this.statusLabel.Text = value; }
        }

        public void StartApplication()
        {
            Application.Run(this);
        }

        private void AddDocuments()
        {
            this.sceneViewDocument = new SceneViewDocument(this.presenterFactory)
            {
                Tag = this.viewDocumentsSceneViewToolStripMenuItem,
            };

            this.dockPanel.AddContent(this.sceneViewDocument);
        }

        private void AddToolWindows()
        {
            this.sceneHierachyToolWindow = new SceneHierarchyToolWindow(this.presenterFactory)
            {
                Tag = this.viewToolWindowsSceneHierarchyToolStripMenuItem,
            };

            this.entityInspectorToolWindow = new EntityInspectorToolWindow(this.presenterFactory)
            {
                Tag = this.viewToolWindowsEntityInspectorToolStripMenuItem,
            };

            this.entitySystemsToolWindow = new EntitySystemsToolWindow(this.presenterFactory)
            {
                Tag = this.viewToolWindowsEntitySystemsToolStripMenuItem,
            };

            this.consoleToolWindow = new ConsoleToolWindow(this.presenterFactory)
            {
                Tag = this.viewToolWindowsConsoleToolStripMenuItem,
            };

            this.dockPanel.AddContent(this.sceneHierachyToolWindow);
            this.dockPanel.AddContent(this.entityInspectorToolWindow);
            this.dockPanel.AddContent(this.entitySystemsToolWindow);
            this.dockPanel.AddContent(this.consoleToolWindow);
        }

        private void FileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnExiting?.Invoke(this, EventArgs.Empty);
        }

        private void MainForm_Disposed(object? sender, EventArgs e)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (this.entitySystemsToolWindow != null)
            {
                this.entitySystemsToolWindow.Dispose();
                this.entitySystemsToolWindow = null;
            }

            if (this.entityInspectorToolWindow != null)
            {
                this.entityInspectorToolWindow.Dispose();
                this.entityInspectorToolWindow = null;
            }

            if (this.sceneHierachyToolWindow != null)
            {
                this.sceneHierachyToolWindow.Dispose();
                this.sceneHierachyToolWindow = null;
            }

            if (this.sceneViewDocument != null)
            {
                this.sceneViewDocument.Dispose();
                this.sceneViewDocument = null;
            }

            if (this.consoleToolWindow != null)
            {
                this.consoleToolWindow.Dispose();
                this.consoleToolWindow = null;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnExiting?.Invoke(this, EventArgs.Empty);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this.dockPanel.DockContentDragFilter);
            Application.AddMessageFilter(this.dockPanel.DockResizeFilter);

            this.Tag = this.presenterFactory.CreateMainPresenter(this);
            this.Disposed += this.MainForm_Disposed;

            this.AddToolWindows();
            this.AddDocuments();

            this.Text = $"{Application.ProductName} - {Application.ProductVersion}";

            this.OnLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void ViewDocumentsSceneViewToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.sceneViewDocument == null)
            {
                return;
            }

            this.OnContentToggled?.Invoke(this, new ContentToggledEventArgs(this.sceneViewDocument));
        }

        private void viewToolWindowsConsoleToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.consoleToolWindow == null)
            {
                return;
            }

            this.OnContentToggled?.Invoke(this, new ContentToggledEventArgs(this.consoleToolWindow));
        }

        private void ViewToolWindowsEntityInspectorToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.entityInspectorToolWindow == null)
            {
                return;
            }

            this.OnContentToggled?.Invoke(this, new ContentToggledEventArgs(this.entityInspectorToolWindow));
        }

        private void viewToolWindowsEntitySystemsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.entitySystemsToolWindow == null)
            {
                return;
            }

            this.OnContentToggled?.Invoke(this, new ContentToggledEventArgs(this.entitySystemsToolWindow));
        }

        private void ViewToolWindowsSceneHierarchyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.sceneHierachyToolWindow == null)
            {
                return;
            }

            this.OnContentToggled?.Invoke(this, new ContentToggledEventArgs(this.sceneHierachyToolWindow));
        }
    }
}
