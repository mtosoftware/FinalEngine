// <copyright file="CreateEntityDialog.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs
{
    using System;
    using System.ComponentModel;
    using FinalEngine.ECS;

    public partial class CreateEntityDialog : DarkDialogError
    {
        public CreateEntityDialog()
        {
            this.InitializeComponent();
            this.Result = new Entity();
        }

        public string? EntityName { get; private set; }

        [TypeConverter(typeof(GuidConverter))]
        public Guid Identifier { get; private set; }

        public Entity Result { get; private set; }

        private void ButtonOk_OnClick(object sender, EventArgs e)
        {
            this.Result.Tag = this.EntityName;
        }

        private void CreateEntityDialog_Load(object sender, EventArgs e)
        {
            this.InitializeBindings();
            this.InitializeDefaultState();
        }

        private void InitializeBindings()
        {
            this.bindingSource.DataSource = this;
        }

        private void InitializeDefaultState()
        {
            this.EntityName = "New Entity";
            this.Identifier = Guid.NewGuid();

            this.entityTagTextbox.Select();
            this.entityTagTextbox.SelectAll();
        }
    }
}
