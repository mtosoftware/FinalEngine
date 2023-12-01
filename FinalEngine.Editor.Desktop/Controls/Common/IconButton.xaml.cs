// <copyright file="IconButton.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Controls.Common;

using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// Interaction logic for IconButton.xaml.
/// </summary>
public partial class IconButton : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IconButton"/> class.
    /// </summary>
    public IconButton()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the URI source for the icon.
    /// </summary>
    /// <value>
    /// The URI source for the icon.
    /// </value>
    public ImageSource UriSource
    {
        get { return this.image.Source; }
        set { this.image.Source = value; }
    }
}
