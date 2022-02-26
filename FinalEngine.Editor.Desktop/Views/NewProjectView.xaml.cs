// <copyright file="NewProjectView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views
{
    using FinalEngine.Editor.ViewModels.Interaction;
    using MahApps.Metro.Controls;

    /// <summary>
    ///   Interaction logic for NewProjectView.xaml.
    /// </summary>
    public partial class NewProjectView : MetroWindow, ICloseable
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="NewProjectView"/> class.
        /// </summary>
        public NewProjectView()
        {
            this.InitializeComponent();
        }
    }
}