// <copyright file="EntityComponentView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Inspectors;

using System.Windows.Controls;

/// <summary>
/// Interaction logic for EntityComponentView.xaml.
/// </summary>
public partial class EntityComponentView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityComponentView"/> class.
    /// </summary>
    public EntityComponentView()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Handles the Click event of the CollapseExpandButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void CollapseExpandButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (this.ItemsListBox.Visibility == System.Windows.Visibility.Collapsed)
        {
            this.ItemsListBox.Visibility = System.Windows.Visibility.Visible;
        }
        else
        {
            this.ItemsListBox.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
