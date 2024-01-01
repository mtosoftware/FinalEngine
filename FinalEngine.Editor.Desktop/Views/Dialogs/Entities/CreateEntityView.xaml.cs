// <copyright file="CreateEntityView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs.Entities;

using FinalEngine.Editor.ViewModels.Dialogs.Entities;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using MahApps.Metro.Controls;

public partial class CreateEntityView : MetroWindow, IViewable<ICreateEntityViewModel>, ICloseable
{
    public CreateEntityView()
    {
        this.InitializeComponent();
    }
}
