// <copyright file="SceneHierarchyToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using System.ComponentModel;
    using DarkUI.Docking;
    using FinalEngine.Editor.Desktop.Views.Dialogs;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Tools;
    using FinalEngine.Editor.ViewModels.Views.Tools;

    public partial class SceneHierarchyToolWindow : DarkToolWindow, ISceneHierarchyView
    {
        private readonly ViewModelFactory factory;

        public SceneHierarchyToolWindow(ViewModelFactory factory)
        {
            this.InitializeComponent();

            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

            this.ViewModel = factory.Create(this);
            this.bindingSource.DataSource = this.ViewModel;

            this.ViewModel.Entities.ListChanged += (s, e) =>
            {
                this.Validate();
            };
        }

        public event EventHandler OnContextDelete;

        public event EventHandler OnContextOpening;

        public SceneHierarchyViewModel ViewModel { get; }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.OnContextOpening?.Invoke(this, e);
        }

        private void CreateEntityToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var dialog = new CreateEntityDialog(this.factory))
            {
                dialog.ShowDialog(this);
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnContextDelete?.Invoke(this, e);
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //// This is a workaround for a bug with winforms.
            this.Validate();
        }
    }
}
