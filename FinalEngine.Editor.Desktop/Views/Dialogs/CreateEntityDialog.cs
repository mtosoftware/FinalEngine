// <copyright file="CreateEntityDialog.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs
{
    using System;
    using DarkUI.Forms;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Views.Dialogs;

    public partial class CreateEntityDialog : DarkDialog, ICreateEntityView
    {
        public CreateEntityDialog(ViewModelFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.InitializeComponent();
            this.bindingSource.DataSource = factory.Create(this);
        }

        public event EventHandler? OnAddComponent;

        public event EventHandler? OnOk;

        public event EventHandler? OnRemoveComponent;

        private void AddComponentToolStripButton_Click(object sender, EventArgs e)
        {
            this.OnAddComponent?.Invoke(this, e);
        }

        private void ButtonOk_OnClick(object sender, EventArgs e)
        {
            this.OnOk?.Invoke(this, e);
        }

        private void CreateEntityDialog_Load(object sender, EventArgs e)
        {
            this.InitializeDefaultState();
        }

        private void InitializeDefaultState()
        {
            this.entityTagTextbox.Select();
            this.entityTagTextbox.SelectAll();
        }

        private void RemoveComponentToolStripButton_Click(object sender, EventArgs e)
        {
            this.OnRemoveComponent?.Invoke(this, e);
        }
    }
}
