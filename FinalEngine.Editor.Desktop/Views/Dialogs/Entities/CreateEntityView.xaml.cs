// <copyright file="CreateEntityView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs.Entities;

using FinalEngine.Editor.ViewModels.Dialogs.Entities;
using FinalEngine.Editor.ViewModels.Interactions;
using MahApps.Metro.Controls;

/// <summary>
/// Interaction logic for CreateEntityView.xaml.
/// </summary>
public partial class CreateEntityView : MetroWindow, IViewable<ICreateEntityViewModel>, ICloseable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEntityView"/> class.
    /// </summary>
    public CreateEntityView()
    {
        this.InitializeComponent();
    }
}
