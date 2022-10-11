// <copyright file="MainForm.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views
{
    using System;
    using System.Windows.Forms;
    using DarkUI.Forms;
    using FinalEngine.Editor.Desktop.Views.Dialogs;
    using FinalEngine.Editor.Desktop.Views.Documents;
    using FinalEngine.Editor.Desktop.Views.Tools;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Documents;
    using FinalEngine.Editor.ViewModels.Tools;
    using FinalEngine.Editor.ViewModels.Views;

    public partial class MainForm : DarkForm, IMainView
    {
        private readonly ViewModelFactory factory;

        private ConsoleToolWindow consoleToolWindow;

        private EntityInspectorToolWindow entityInspectorToolWindow;

        private EntitySystemsToolWindow entitySystemsToolWindow;

        private SceneHierarchyToolWindow sceneHierarchyToolWindow;

        private SceneViewDocument sceneViewDocument;

        public MainForm(ViewModelFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

            this.InitializeComponent();
            this.InitializeDefaultState();
        }

        public event EventHandler? OnEditMenuOpening;

        public event EventHandler? OnEditRedo;

        public event EventHandler? OnEditUndo;

        public event EventHandler? OnLoaded;

        public event EventHandler? OnMenuEditDelete;

        public ConsoleViewModel Console
        {
            get { return this.consoleToolWindow.ViewModel; }
        }

        public EntityInspectorViewModel EntityInspector
        {
            get { return this.entityInspectorToolWindow.ViewModel; }
        }

        public EntitySystemsViewModel EntitySystems
        {
            get { return this.entitySystemsToolWindow.ViewModel; }
        }

        public SceneViewModel Scene
        {
            get { return this.sceneViewDocument.ViewModel; }
        }

        public SceneHierarchyViewModel SceneHierarchy
        {
            get { return this.sceneHierarchyToolWindow.ViewModel; }
        }

        public void StartApplication()
        {
            Application.Run(this);
        }

        private void EditDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnMenuEditDelete?.Invoke(sender, e);
        }

        private void EditRedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnEditRedo?.Invoke(sender, e);
        }

        private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            this.OnEditMenuOpening?.Invoke(sender, e);
        }

        private void EditUndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnEditUndo?.Invoke(sender, e);
        }

        private void InitializeDefaultState()
        {
            this.bindingSource.DataSource = this.factory.Create(this);

            this.sceneHierarchyToolWindow = new SceneHierarchyToolWindow(this.factory);
            this.consoleToolWindow = new ConsoleToolWindow(this.factory);
            this.entityInspectorToolWindow = new EntityInspectorToolWindow(this.factory);
            this.entitySystemsToolWindow = new EntitySystemsToolWindow(this.factory);
            this.sceneViewDocument = new SceneViewDocument(this.factory);

            this.dockPanel.AddContent(this.sceneHierarchyToolWindow);
            this.dockPanel.AddContent(this.consoleToolWindow);
            this.dockPanel.AddContent(this.entityInspectorToolWindow);
            this.dockPanel.AddContent(this.entitySystemsToolWindow);
            this.dockPanel.AddContent(this.sceneViewDocument);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //// Winforms doesn't support shortcuts with a singular key.
            if (e.KeyCode == Keys.Delete)
            {
                this.OnMenuEditDelete?.Invoke(sender, e);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this.dockPanel.DockContentDragFilter);
            Application.AddMessageFilter(this.dockPanel.DockResizeFilter);

            this.OnLoaded?.Invoke(sender, e);
        }

        private void WorldCreateEntityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new CreateEntityDialog(this.factory))
            {
                if (dialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
            }
        }
    }
}
