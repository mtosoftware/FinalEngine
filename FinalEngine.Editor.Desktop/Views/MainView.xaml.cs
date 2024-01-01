// <copyright file="MainView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views;

using FinalEngine.Editor.ViewModels.Services.Interactions;
using MahApps.Metro.Controls;

public partial class MainView : MetroWindow, ICloseable
{
    public MainView()
    {
        this.InitializeComponent();
    }
}
