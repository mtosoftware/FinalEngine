// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System.Windows.Controls;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;

public partial class SceneView : UserControl
{
    public SceneView()
    {
        this.InitializeComponent();

        this.glWpfControl.Start(new GLWpfControlSettings()
        {
            MajorVersion = 4,
            MinorVersion = 6,
            GraphicsProfile = ContextProfile.Compatability,
            GraphicsContextFlags = ContextFlags.ForwardCompatible,
            RenderContinuously = true,
        });
    }
}
