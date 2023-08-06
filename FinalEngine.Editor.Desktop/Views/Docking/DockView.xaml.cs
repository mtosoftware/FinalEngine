// <copyright file="DockView.xaml.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Docking;

using System.IO;
using System.Runtime.Versioning;
using System.Windows.Controls;
using AvalonDock.Layout.Serialization;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Interactions;

/// <summary>
/// Interaction logic for DockView.xaml.
/// </summary>
[SupportedOSPlatform("windows")]
public partial class DockView : UserControl, ILayoutSerializable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DockView"/> class.
    /// </summary>
    public DockView()
    {
        this.InitializeComponent();
        this.Dispatcher.ShutdownStarted += this.Dispatcher_ShutdownStarted;
    }

    public void Deserialize(string content)
    {
        using (var reader = new StringReader(content))
        {
            var serializer = new XmlLayoutSerializer(this.dockManager);
            serializer.Deserialize(reader);
        }
    }

    public string Serialize()
    {
        using (var fs = new StringWriter())
        {
            var serializer = new XmlLayoutSerializer(this.dockManager);

            serializer.Serialize(fs);
            return fs.ToString();
        }
    }

    private void Dispatcher_ShutdownStarted(object? sender, System.EventArgs e)
    {
        ((IDockViewModel)this.DataContext).SaveLayoutCommand.Execute(this);
    }
}
