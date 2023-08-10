// <copyright file="DockView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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

    /// <summary>
    /// Occurs when the <see cref="Dispatcher"/> for this <see cref="DockView"/> initiates the shutdown process.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The <see cref="EventArgs"/> instance containing the event data.
    /// </param>
    private void Dispatcher_ShutdownStarted(object? sender, EventArgs e)
    {
        this.DockManager.RaiseEvent(new RoutedEventArgs(UnloadedEvent));
    }
}
