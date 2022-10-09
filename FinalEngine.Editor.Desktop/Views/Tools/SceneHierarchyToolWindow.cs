// <copyright file="SceneHierarchyToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using System.ComponentModel;
    using DarkUI.Docking;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.Views.Tools;

    public partial class SceneHierarchyToolWindow : DarkToolWindow, ISceneHierarchyToolWindowView
    {
        public SceneHierarchyToolWindow(ViewModelFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.InitializeComponent();
            this.bindingSource.DataSource = factory.Create(this);
        }

        public event EventHandler OnClick;

        public event EventHandler<CancelEventArgs> OnContextMenuOpening;

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.OnContextMenuOpening?.Invoke(this, e);
        }

        private void ListBox_Click(object sender, EventArgs e)
        {
            this.OnClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
