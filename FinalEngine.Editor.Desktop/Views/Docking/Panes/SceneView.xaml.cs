namespace FinalEngine.Editor.Desktop.Views.Docking.Panes
{
    using OpenTK.Windowing.Common;
    using OpenTK.Wpf;

    /// <summary>
    ///   Interaction logic for SceneView.xaml.
    /// </summary>
    public partial class SceneView : GLWpfControl
    {
        public SceneView()
        {
            this.InitializeComponent();

            this.Start(new GLWpfControlSettings()
            {
                GraphicsContextFlags = ContextFlags.ForwardCompatible,
                GraphicsProfile = ContextProfile.Core,
                MajorVersion = 4,
                MinorVersion = 5,
                RenderContinuously = true,
            });
        }
    }
}