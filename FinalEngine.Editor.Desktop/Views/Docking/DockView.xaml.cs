// <copyright file="DockView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking;

using System;
using System.Windows;
using System.Windows.Controls;

public partial class DockView : UserControl
{
    public DockView()
    {
        this.InitializeComponent();
        this.Dispatcher.ShutdownStarted += this.Dispatcher_ShutdownStarted;
    }

    private void Dispatcher_ShutdownStarted(object? sender, EventArgs e)
    {
        this.DockManager.RaiseEvent(new RoutedEventArgs(UnloadedEvent));
    }
}
