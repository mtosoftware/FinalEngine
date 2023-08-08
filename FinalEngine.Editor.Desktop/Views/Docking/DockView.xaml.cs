// <copyright file="DockView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for DockView.xaml.
/// </summary>
public partial class DockView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DockView"/> class.
    /// </summary>
    public DockView()
    {
        this.InitializeComponent();
        this.Dispatcher.ShutdownStarted += this.Dispatcher_ShutdownStarted;
    }

    private void Dispatcher_ShutdownStarted(object? sender, System.EventArgs e)
    {
        this.DockManager.RaiseEvent(new RoutedEventArgs(UnloadedEvent));
    }
}
