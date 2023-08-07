namespace FinalEngine.Editor.Desktop.Views.Dialogs.Layout;

using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Interactions;
using MahApps.Metro.Controls;

/// <summary>
/// Interaction logic for SaveWindowLayoutView.xaml.
/// </summary>
public partial class SaveWindowLayoutView : MetroWindow, IViewable<ISaveWindowLayoutViewModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveWindowLayoutView"/> class.
    /// </summary>
    public SaveWindowLayoutView()
    {
        this.InitializeComponent();
    }
}
