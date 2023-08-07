// <copyright file="DockView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking;

using System;
using System.Windows;
using System.Windows.Controls;
using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.ViewModels.Messages.Layout;

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

        WeakReferenceMessenger.Default.Register<SaveWindowLayoutMessage>(this, this.SaveLayout);
        WeakReferenceMessenger.Default.Register<LoadWindowLayoutMessage>(this, this.LoadLayout);
        WeakReferenceMessenger.Default.Register<ResetWindowLayoutMessage>(this, this.ResetLayout);

        this.Dispatcher.ShutdownStarted += this.Dispatcher_ShutdownStarted;
    }

    private void Dispatcher_ShutdownStarted(object? sender, EventArgs e)
    {
        this.dockManager.RaiseEvent(new RoutedEventArgs(UnloadedEvent));
    }

    private void LoadLayout(object recipient, LoadWindowLayoutMessage message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        var serializer = new XmlLayoutSerializer(this.dockManager);
        serializer.Deserialize(message.FilePath);
    }

    private void ResetLayout(object recipient, ResetWindowLayoutMessage message)
    {
        WeakReferenceMessenger.Default.Send(new LoadWindowLayoutMessage("Resources\\Layouts\\default.config"));
    }

    private void SaveLayout(object recipient, SaveWindowLayoutMessage message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        var serializer = new XmlLayoutSerializer(this.dockManager);
        serializer.Serialize(message.FilePath);
    }
}
