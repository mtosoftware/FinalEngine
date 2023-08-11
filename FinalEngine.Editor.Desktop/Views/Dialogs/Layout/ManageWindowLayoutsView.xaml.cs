// <copyright file="ManageWindowLayoutsView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs.Layout;

using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Interactions;
using MahApps.Metro.Controls;

/// <summary>
/// Interaction logic for ManageWindowLayoutsView.xaml.
/// </summary>
public partial class ManageWindowLayoutsView : MetroWindow, IViewable<IManageWindowLayoutsViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManageWindowLayoutsView"/> class.
    /// </summary>
    public ManageWindowLayoutsView()
    {
        this.InitializeComponent();
    }
}
