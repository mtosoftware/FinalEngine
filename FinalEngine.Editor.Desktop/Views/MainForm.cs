// <copyright file="MainForm.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;
    using DarkUI.Docking;
    using DarkUI.Forms;
    using FinalEngine.Editor.Desktop;
    using FinalEngine.Editor.Desktop.Views.Documents;
    using FinalEngine.Editor.Desktop.Views.Tools;
    using FinalEngine.Editor.Presenters;
    using FinalEngine.Editor.Views;

    /// <summary>
    ///   Provides the main application form; this is currently the starting form of the application.
    /// </summary>
    /// <seealso cref="DarkForm"/>
    /// <seealso cref="IMainView"/>
    /// <seealso cref="IApplicationStarter"/>
    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "Tool Windows and Documents are disposed via the main dock panel.")]
    public partial class MainForm : DarkForm, IMainView, IApplicationStarter
    {
        /// <summary>
        ///   The presenter factory.
        /// </summary>
        private readonly IPresenterFactory presenterFactory;

        /// <summary>
        ///   The console tool window.
        /// </summary>
        private ConsoleToolWindow? consoleToolWindow;

        /// <summary>
        ///   The entity inspector tool window.
        /// </summary>
        private EntityInspectorToolWindow? entityInspectorToolWindow;

        /// <summary>
        ///   The entity systems tool window.
        /// </summary>
        private EntitySystemsToolWindow? entitySystemsToolWindow;

        /// <summary>
        ///   The scene hierachy tool window.
        /// </summary>
        private SceneHierarchyToolWindow? sceneHierachyToolWindow;

        /// <summary>
        ///   The scene view document.
        /// </summary>
        private SceneViewDocument? sceneViewDocument;

        /// <summary>
        ///   Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <param name="presenterFactory">
        ///   The presenter factory.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="presenterFactory"/> parameter cannot be null.
        /// </exception>
        public MainForm(IPresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory ?? throw new ArgumentNullException(nameof(presenterFactory));
            this.InitializeComponent();
        }

        /// <summary>
        ///   Gets or sets the action to perform when the application is exiting.
        /// </summary>
        /// <value>
        ///   The action to perform when the application is exiting.
        /// </value>
        public Action? OnExit { get; set; }

        /// <summary>
        ///   Gets or sets the action to perform when the application has loaded.
        /// </summary>
        /// <value>
        ///   The action to perform when the application has loaded.
        /// </value>
        public new Action? OnLoad { get; set; }

        /// <summary>
        ///   Starts the main application.
        /// </summary>
        public void StartApplication()
        {
            Application.Run(this);
        }

        private void AddDocuments()
        {
            this.sceneViewDocument = new SceneViewDocument(this.presenterFactory);

            this.dockPanel.AddContent(this.sceneViewDocument);
        }

        /// <summary>
        ///   Adds the tool windows to the main dock panel.
        /// </summary>
        private void AddToolWindows()
        {
            this.sceneHierachyToolWindow = new SceneHierarchyToolWindow(this.presenterFactory);
            this.entityInspectorToolWindow = new EntityInspectorToolWindow(this.presenterFactory);
            this.entitySystemsToolWindow = new EntitySystemsToolWindow(this.presenterFactory);
            this.consoleToolWindow = new ConsoleToolWindow(this.presenterFactory);

            this.dockPanel.AddContent(this.sceneHierachyToolWindow);
            this.dockPanel.AddContent(this.entityInspectorToolWindow);
            this.dockPanel.AddContent(this.entitySystemsToolWindow);
            this.dockPanel.AddContent(this.consoleToolWindow);
        }

        /// <summary>
        ///   Handles the Click event of the <see cref="fileToolStripMenuItem"/> control.
        /// </summary>
        /// <param name="sender">
        ///   The source of the event.
        /// </param>
        /// <param name="e">
        ///   The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void FileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnExit?.Invoke();
        }

        /// <summary>
        ///   Handles the FormClosing event of the <see cref="MainForm"/> control.
        /// </summary>
        /// <param name="sender">
        ///   The source of the event.
        /// </param>
        /// <param name="e">
        ///   The <see cref="FormClosingEventArgs"/> instance containing the event data.
        /// </param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        ///   Handles the Load event of the <see cref="MainForm"/> control.
        /// </summary>
        /// <param name="sender">
        ///   The source of the event.
        /// </param>
        /// <param name="e">
        ///   The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this.dockPanel.DockContentDragFilter);
            Application.AddMessageFilter(this.dockPanel.DockResizeFilter);

            this.Tag = this.presenterFactory.CreateMainPresenter(this);

            this.AddToolWindows();
            this.AddDocuments();

            this.OnLoad?.Invoke();
        }

        /// <summary>
        ///   Toggles the visiblity of the specified <paramref name="content"/>.
        /// </summary>
        /// <param name="content">
        ///   The content (tool window or otherwise).
        /// </param>
        private void ToggleContentVisibility(DarkDockContent? content)
        {
            if (content == null)
            {
                return;
            }

            if (this.dockPanel.ContainsContent(content))
            {
                this.dockPanel.RemoveContent(content);
                return;
            }

            this.dockPanel.AddContent(content);
        }

        /// <summary>
        ///   Handles the Click event of the <see cref="viewToolWindowsSceneHierarchyToolStripMenuItem"/> control.
        /// </summary>
        /// <param name="sender">
        ///   The source of the event.
        /// </param>
        /// <param name="e">
        ///   The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void ViewToolWindowsSceneHierarchyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ToggleContentVisibility(this.sceneHierachyToolWindow);
        }
    }
}
