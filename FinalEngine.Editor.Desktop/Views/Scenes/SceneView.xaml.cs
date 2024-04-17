// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.ViewModels.Scenes;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using Size = System.Drawing.Size;

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

    private void SceneView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (this.DataContext is not ISceneViewPaneViewModel viewModel)
        {
            return;
        }

        viewModel.AdjustRenderSizeCommand.Execute(new Size((int)this.glWpfControl.ActualWidth, (int)this.glWpfControl.ActualHeight));
    }
}
