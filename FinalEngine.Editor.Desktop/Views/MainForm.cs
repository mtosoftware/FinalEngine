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
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.Views;

    public partial class MainForm : DarkForm, IMainView, IApplicationStarter
    {
        private readonly ViewModelFactory factory;

        private ConsoleToolWindow? consoleToolWindow;

        private EntityInspectorToolWindow? entityInspectorToolWindow;

        private EntitySystemsToolWindow? entitySystemsToolWindow;

        private SceneHierarchyToolWindow? sceneHierachyToolWindow;

        private SceneViewDocument? sceneViewDocument;

        public MainForm(ViewModelFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

            this.InitializeComponent();
            this.bindingSource.DataSource = factory.Create(this);
        }

        public event EventHandler<EventArgs>? OnExiting;

        public event EventHandler<EventArgs>? OnLoaded;

        public void StartApplication()
        {
            Application.Run(this);
        }

        private void AddDocuments()
        {
            this.sceneViewDocument = new SceneViewDocument(this.factory);

            this.dockPanel.AddContent(this.sceneViewDocument);
        }

        private void AddToolWindows()
        {
            this.sceneHierachyToolWindow = new SceneHierarchyToolWindow(this.factory);
            this.entityInspectorToolWindow = new EntityInspectorToolWindow();
            this.entitySystemsToolWindow = new EntitySystemsToolWindow();
            this.consoleToolWindow = new ConsoleToolWindow();

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

            this.Disposed += this.MainForm_Disposed;

            this.AddToolWindows();
            this.AddDocuments();

            this.Text = $"{Application.ProductName} - {Application.ProductVersion}";
            this.OnLoaded?.Invoke(this, EventArgs.Empty);
        }
    }
}
