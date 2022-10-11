// <copyright file="ToolStripDataButton.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Controls
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    [ToolboxItem(false)]
    public class ToolStripDataButton : ToolStripButton, IBindableComponent
    {
        private ControlBindingsCollection bindings;

        private BindingContext context;

        [Browsable(false)]
        public BindingContext BindingContext
        {
            get
            {
                if (this.context == null)
                {
                    this.context = new BindingContext();
                }

                return this.context;
            }

            set
            {
                this.context = value;
            }
        }

        public ContextMenuStrip? ContextMenuStrip { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (this.bindings == null)
                {
                    this.bindings = new ControlBindingsCollection(this);
                }

                return this.bindings;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ContextMenuStrip?.Show(Cursor.Position);
            }

            base.OnMouseUp(e);
        }
    }
}
