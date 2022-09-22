// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking.Panes
{
    using OpenTK.Windowing.Common;
    using OpenTK.Wpf;

    /// <summary>
    ///   Interaction logic for SceneView.xaml.
    /// </summary>
    public partial class SceneView : GLWpfControl
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="SceneView"/> class.
        /// </summary>
        public SceneView()
        {
            this.InitializeComponent();

            this.Start(new GLWpfControlSettings()
            {
                MajorVersion = 4,
                MinorVersion = 6,
                GraphicsContextFlags = ContextFlags.ForwardCompatible,
                GraphicsProfile = ContextProfile.Core,
                RenderContinuously = true,
            });
        }
    }
}