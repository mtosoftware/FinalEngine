// <copyright file="SceneHierarchyToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using DarkUI.Docking;
    using FinalEngine.ECS;
    using FinalEngine.Editor.Desktop.Views.Dialogs;

    public partial class SceneHierarchyToolWindow : DarkToolWindow
    {
        public SceneHierarchyToolWindow()
        {
            this.InitializeComponent();
            this.Entities = new BindingList<Entity>();
        }

        public BindingList<Entity> Entities { get; private set; }

        private void CreateEntityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new CreateEntityDialog())
            {
                if (dialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                this.Entities.Add(dialog.Result);
            }
        }

        private void InitializeBindings()
        {
            this.bindingSource.DataSource = this;
        }

        private void SceneHierarchyToolWindow_Load(object sender, EventArgs e)
        {
            this.InitializeBindings();
        }
    }
}
