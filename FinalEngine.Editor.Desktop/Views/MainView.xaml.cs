// <copyright file="MainView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views;

using FinalEngine.Editor.ViewModels.Interaction;
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
}
