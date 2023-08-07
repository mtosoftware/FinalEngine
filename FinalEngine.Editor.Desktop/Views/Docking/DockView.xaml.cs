// <copyright file="DockView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking;

using System.Windows;
using System.Windows.Controls;
using AvalonDock.Layout.Serialization;

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

        this.Loaded += this.DockView_Loaded;
    }

    private void DockView_Loaded(object sender, RoutedEventArgs e)
    {
        var serializer = new XmlLayoutSerializer(this.dockManager);
        serializer.Deserialize("Resources\\Layouts\\default.config");
    }
}
