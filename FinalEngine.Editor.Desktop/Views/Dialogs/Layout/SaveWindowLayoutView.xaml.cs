// <copyright file="SaveWindowLayoutView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Dialogs.Layout;

using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using MahApps.Metro.Controls;

public partial class SaveWindowLayoutView : MetroWindow, IViewable<ISaveWindowLayoutViewModel>, ICloseable
{
    public SaveWindowLayoutView()
    {
        this.InitializeComponent();
    }
}
