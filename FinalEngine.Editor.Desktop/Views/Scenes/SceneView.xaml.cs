// <copyright file="SceneView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System.Diagnostics;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for SceneView.xaml.
/// </summary>
public partial class SceneView : UserControl
{
    private static readonly Stopwatch stopwatch = Stopwatch.StartNew();

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneView"/> class.
    /// </summary>
    public SceneView()
    {
        this.InitializeComponent();
        this.glWpfControl.Start();
    }
}
