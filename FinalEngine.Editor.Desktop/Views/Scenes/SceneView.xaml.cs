// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FinalEngine.Editor.Desktop.Framework.Input;
using FinalEngine.Editor.ViewModels.Scenes;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using Size = System.Drawing.Size;

public partial class SceneView : UserControl
{
    private static bool isEventsRegistered;

    public SceneView()
    {
        this.InitializeComponent();

        if (!isEventsRegistered)
        {
            this.glWpfControl.RegisterToEventsDirectly = false;
            this.glWpfControl.CanInvokeOnHandledEvents = false;
            isEventsRegistered = true;
        }

        this.glWpfControl.Focusable = true;

        this.glWpfControl.MouseDown += this.GlWpfControl_MouseDown;
        this.glWpfControl.MouseEnter += this.GlWpfControl_MouseEnter;
        this.glWpfControl.MouseLeave += this.GlWpfControl_MouseLeave;

        this.glWpfControl.Start(new GLWpfControlSettings()
        {
            MajorVersion = 4,
            MinorVersion = 6,
            GraphicsProfile = ContextProfile.Compatability,
            GraphicsContextFlags = ContextFlags.ForwardCompatible,
            RenderContinuously = true,
        });

        KeyboardDevice.Initialize(this);
        MouseDevice.Initialize(this);
    }

    internal static WPFKeyboardDevice KeyboardDevice { get; } = new WPFKeyboardDevice();

    internal static WPFMouseDevice MouseDevice { get; } = new WPFMouseDevice();

    private void GlWpfControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.glWpfControl.Focus();
    }

    private void GlWpfControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        this.glWpfControl.Focus();
    }

    private void GlWpfControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        var scope = FocusManager.GetFocusScope(this.glWpfControl);
        FocusManager.SetFocusedElement(scope, null);
        Keyboard.ClearFocus();
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
