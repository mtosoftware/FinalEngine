// <copyright file="EntityInspectorToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using DarkUI.Docking;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Tools;
    using FinalEngine.Editor.ViewModels.Views.Tools;

    public partial class EntityInspectorToolWindow : DarkToolWindow, IEntityInspectorView
    {
        public EntityInspectorToolWindow(ViewModelFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.InitializeComponent();

            this.ViewModel = factory.Create(this);
            this.bindingSource.DataSource = this.ViewModel;
        }

        public EntityInspectorViewModel ViewModel { get; }
    }
}
