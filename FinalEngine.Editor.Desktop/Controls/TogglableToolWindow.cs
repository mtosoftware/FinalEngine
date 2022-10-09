// <copyright file="TogglableDarkDockContent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Controls
{
    using System.Windows.Forms;
    using DarkUI.Docking;
    using FinalEngine.Editor.Views.Interactions;

    public partial class TogglableToolWindow : DarkToolWindow, ITogglable
    {
        private DarkDockPanel panel;

        public TogglableToolWindow()
        {
            this.InitializeComponent();
        }

        public void Toggle()
        {
            if (this.DockPanel != null)
            {
                if (this.DockPanel.ContainsContent(this))
                {
                    this.panel = this.DockPanel;
                    this.DockPanel.RemoveContent(this);
                }
            }
            else
            {
                if (this.panel == null)
                {
                    return;
                }

                this.panel.AddContent(this);
            }

            if (this.Tag is not ToolStripMenuItem item)
            {
                return;
            }

            item.Checked = this.DockPanel != null;
        }
    }
}
