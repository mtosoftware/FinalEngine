namespace FinalEngine.Editor.Desktop.Views.Tools
{
    partial class SceneHierarchyToolWindow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox = new System.Windows.Forms.ListBox();
            this.contextMenu = new DarkUI.Controls.DarkContextMenu();
            this.deleteToolStripMenuItem = new FinalEngine.Editor.Desktop.Controls.ToolStripDataMenuItem();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createEntityToolStripMenuItem = new FinalEngine.Editor.Desktop.Controls.ToolStripDataMenuItem();
            this.entitiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entitiesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.listBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox.ContextMenuStrip = this.contextMenu;
            this.listBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedIndex", this.bindingSource, "SelectedIndex", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.listBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSource, "SelectedEntity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.listBox.DataSource = this.entitiesBindingSource;
            this.listBox.DisplayMember = "Tag";
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(0, 25);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(200, 375);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.contextMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.createEntityToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(185, 55);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.deleteToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingSource, "CanPerformEditCommands", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.deleteToolStripMenuItem.Text = "De&lete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(FinalEngine.Editor.ViewModels.Tools.SceneHierarchyViewModel);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // createEntityToolStripMenuItem
            // 
            this.createEntityToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.createEntityToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.createEntityToolStripMenuItem.Name = "createEntityToolStripMenuItem";
            this.createEntityToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.createEntityToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.createEntityToolStripMenuItem.Text = "Create &Entity";
            this.createEntityToolStripMenuItem.Click += new System.EventHandler(this.CreateEntityToolStripMenuItem_Click_1);
            // 
            // entitiesBindingSource
            // 
            this.entitiesBindingSource.DataMember = "Entities";
            this.entitiesBindingSource.DataSource = this.bindingSource;
            // 
            // SceneHierarchyToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox);
            this.DefaultDockArea = DarkUI.Docking.DarkDockArea.Left;
            this.DockText = "Scene Hierarchy";
            this.Name = "SceneHierarchyToolWindow";
            this.Size = new System.Drawing.Size(200, 400);
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entitiesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private DarkUI.Controls.DarkContextMenu contextMenu;
        private System.Windows.Forms.BindingSource bindingSource;
        private Controls.ToolStripDataMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.ToolStripDataMenuItem createEntityToolStripMenuItem;
        private System.Windows.Forms.BindingSource entitiesBindingSource;
    }
}
