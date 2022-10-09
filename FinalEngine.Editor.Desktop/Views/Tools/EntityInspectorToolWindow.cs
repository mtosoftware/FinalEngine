// <copyright file="EntityInspectorToolWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Tools
{
    using System;
    using FinalEngine.Editor.Desktop.Controls;
    using FinalEngine.Editor.Presenters;

    public partial class EntityInspectorToolWindow : TogglableToolWindow
    {
        public EntityInspectorToolWindow(IPresenterFactory presenterFactory)
        {
            if (presenterFactory == null)
            {
                throw new ArgumentNullException(nameof(presenterFactory));
            }

            this.InitializeComponent();
        }
    }
}
