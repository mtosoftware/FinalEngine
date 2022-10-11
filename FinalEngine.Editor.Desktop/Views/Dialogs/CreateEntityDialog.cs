// <copyright file="CreateEntityDialog.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using DarkUI.Forms;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.Editor.Services.Components;
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
        }

        private void AddContextMenuItem(string name)
        {
            var item = this.addComponentButton.ContextMenuStrip?.Items.Add(name);

            if (item == null)
            {
                return;
            }

            item.Click += (s, e) =>
            {
                this.OnAddComponent?.Invoke(s, e);
            };
        }

        private void ButtonOk_OnClick(object sender, EventArgs e)
        {
            this.OnOk?.Invoke(this, e);
        }

        private void ComponentsContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.OnAddComponent?.Invoke(sender, e);
        }

        private void CreateEntityDialog_Load(object sender, EventArgs e)
        {
            this.InitializeContextMenu();
            this.InitializeDefaultState();
        }

        private void InitializeContextMenu()
        {
            //// TODO: Move this code into the view model and just make the AddContextMenuItem function public for the view model.
            var types = ComponentTypesFetcher.Instance.FetchComponentTypes(Assembly.GetAssembly(typeof(TagComponent)));

            foreach (var type in types)
            {
                string name = Regex.Replace(type.Name, "([A-Z])", " $1").Trim();
                this.AddContextMenuItem(name);
            }
        }

        private void InitializeDefaultState()
        {
            this.entityTagTextbox.SelectAll();
            this.entityTagTextbox.Select();
        }

        private void removeComponentButton_Click(object sender, EventArgs e)
        {
            this.OnRemoveComponent?.Invoke(sender, e);
        }
    }
}
