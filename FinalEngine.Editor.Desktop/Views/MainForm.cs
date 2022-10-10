// <copyright file="MainForm.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views
{
    using System;
    using System.Windows.Forms;
    using DarkUI.Docking;
    using DarkUI.Forms;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Desktop.Views.Documents;
    using FinalEngine.Editor.Desktop.Views.Tools;

    public partial class MainForm : DarkForm
    {
        private readonly IEntityWorld world;

        private ConsoleToolWindow consoleToolWindow;

        private EntityInspectorToolWindow entityInspectorToolWindow;

        private EntitySystemsToolWindow entitySystemsToolWindow;

        private SceneHierarchyToolWindow sceneHierarchyToolWindow;

        private SceneViewDocument sceneViewDocument;

        public MainForm(IEntityWorld world)
        {
            this.world = world ?? throw new ArgumentNullException(nameof(world));
            this.InitializeComponent();
        }

        private string Status
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
            this.sceneViewDocument = new SceneViewDocument();
            this.dockPanel.AddContent(this.sceneViewDocument);
        }

        private void AddToolWindows()
        {
            this.sceneHierarchyToolWindow = new SceneHierarchyToolWindow();
            this.consoleToolWindow = new ConsoleToolWindow();
            this.entityInspectorToolWindow = new EntityInspectorToolWindow();
            this.entitySystemsToolWindow = new EntitySystemsToolWindow();

            this.dockPanel.AddContent(this.sceneHierarchyToolWindow);
            this.dockPanel.AddContent(this.consoleToolWindow);
            this.dockPanel.AddContent(this.entityInspectorToolWindow);
            this.dockPanel.AddContent(this.entitySystemsToolWindow);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this.dockPanel.DockContentDragFilter);
            Application.AddMessageFilter(this.dockPanel.DockResizeFilter);

            this.Text = $"{Application.ProductName} - {Application.ProductVersion}";

            this.AddToolWindows();
            this.AddDocuments();

            this.Status = "Ready!";
        }

        private void ToggleContent(DarkDockContent content)
        {
            if (this.dockPanel.ContainsContent(content))
            {
                this.dockPanel.RemoveContent(content);
                return;
            }

            this.dockPanel.AddContent(content);
        }
    }
}
