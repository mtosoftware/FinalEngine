// <copyright file="MainView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views;

using System.IO.Abstractions;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Desktop.Views.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Interactions;
using MahApps.Metro.Controls;

/// <summary>
/// Interaction logic for MainView.xaml.
/// </summary>
public partial class MainView : MetroWindow, ICloseable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainView"/> class.
    /// </summary>
    public MainView()
    {
        this.InitializeComponent();
    }

    private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var view = new ManageWindowLayoutsView()
        {
            DataContext = new ManageWindowLayoutsViewModel(
                new FileSystem(),
                new ApplicationDataContext(new FileSystem())),
        };

        view.ShowDialog();
    }
}
