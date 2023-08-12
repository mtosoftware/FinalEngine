// <copyright file="CreateNewEntityView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs.Entities;

using FinalEngine.Editor.ViewModels.Dialogs.Entities;
using FinalEngine.Editor.ViewModels.Interactions;
using MahApps.Metro.Controls;

/// <summary>
/// Interaction logic for CreateNewEntityView.xaml.
/// </summary>
public partial class CreateNewEntityView : MetroWindow, IViewable<ICreateNewEntityViewModel>, ICloseable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateNewEntityView"/> class.
    /// </summary>
    public CreateNewEntityView()
    {
        this.InitializeComponent();
    }
}
