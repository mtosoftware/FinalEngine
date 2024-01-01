// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System.Windows.Controls;

public partial class SceneView : UserControl
{
    public SceneView()
    {
        this.InitializeComponent();
        this.glWpfControl.Start();
    }
}
