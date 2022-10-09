namespace FinalEngine.Editor.Desktop.Views
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip = new DarkUI.Controls.DarkStatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new DarkUI.Controls.DarkMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsSceneHierarchyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsEntityInspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsEntitySystemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel = new DarkUI.Docking.DarkDockPanel();
            this.documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDocumentsSceneViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.statusStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 651);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
            this.statusStrip.Size = new System.Drawing.Size(1264, 30);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 0);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.menuStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // fileExitToolStripMenuItem
            // 
            this.fileExitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.fileExitToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.fileExitToolStripMenuItem.Name = "fileExitToolStripMenuItem";
            this.fileExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.fileExitToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.fileExitToolStripMenuItem.Text = "&Exit";
            this.fileExitToolStripMenuItem.Click += new System.EventHandler(this.FileExitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolWindowsToolStripMenuItem,
            this.documentsToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // viewToolWindowsToolStripMenuItem
            // 
            this.viewToolWindowsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.viewToolWindowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolWindowsSceneHierarchyToolStripMenuItem,
            this.viewToolWindowsEntityInspectorToolStripMenuItem,
            this.viewToolWindowsEntitySystemsToolStripMenuItem,
            this.viewToolWindowsConsoleToolStripMenuItem});
            this.viewToolWindowsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.viewToolWindowsToolStripMenuItem.Name = "viewToolWindowsToolStripMenuItem";
            this.viewToolWindowsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewToolWindowsToolStripMenuItem.Text = "&Tool Windows";
            // 
            // viewToolWindowsSceneHierarchyToolStripMenuItem
            // 
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.Checked = true;
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.CheckOnClick = true;
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.Name = "viewToolWindowsSceneHierarchyToolStripMenuItem";
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.Text = "&Scene Hierarchy";
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ViewToolWindowsSceneHierarchyToolStripMenuItem_CheckedChanged);
            // 
            // viewToolWindowsEntityInspectorToolStripMenuItem
            // 
            this.viewToolWindowsEntityInspectorToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.viewToolWindowsEntityInspectorToolStripMenuItem.Checked = true;
            this.viewToolWindowsEntityInspectorToolStripMenuItem.CheckOnClick = true;
            this.viewToolWindowsEntityInspectorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewToolWindowsEntityInspectorToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.viewToolWindowsEntityInspectorToolStripMenuItem.Name = "viewToolWindowsEntityInspectorToolStripMenuItem";
            this.viewToolWindowsEntityInspectorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewToolWindowsEntityInspectorToolStripMenuItem.Text = "&Entity Inspector";
            this.viewToolWindowsEntityInspectorToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ViewToolWindowsEntityInspectorToolStripMenuItem_CheckedChanged);
            // 
            // viewToolWindowsEntitySystemsToolStripMenuItem
            // 
            this.viewToolWindowsEntitySystemsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.viewToolWindowsEntitySystemsToolStripMenuItem.Checked = true;
            this.viewToolWindowsEntitySystemsToolStripMenuItem.CheckOnClick = true;
            this.viewToolWindowsEntitySystemsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewToolWindowsEntitySystemsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.viewToolWindowsEntitySystemsToolStripMenuItem.Name = "viewToolWindowsEntitySystemsToolStripMenuItem";
            this.viewToolWindowsEntitySystemsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewToolWindowsEntitySystemsToolStripMenuItem.Text = "Entity S&ystems";
            this.viewToolWindowsEntitySystemsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.viewToolWindowsEntitySystemsToolStripMenuItem_CheckedChanged);
            // 
            // viewToolWindowsConsoleToolStripMenuItem
            // 
            this.viewToolWindowsConsoleToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.viewToolWindowsConsoleToolStripMenuItem.Checked = true;
            this.viewToolWindowsConsoleToolStripMenuItem.CheckOnClick = true;
            this.viewToolWindowsConsoleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewToolWindowsConsoleToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.viewToolWindowsConsoleToolStripMenuItem.Name = "viewToolWindowsConsoleToolStripMenuItem";
            this.viewToolWindowsConsoleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewToolWindowsConsoleToolStripMenuItem.Text = "&Console";
            this.viewToolWindowsConsoleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.viewToolWindowsConsoleToolStripMenuItem_CheckedChanged);
            // 
            // dockPanel
            // 
            this.dockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 24);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1264, 627);
            this.dockPanel.TabIndex = 2;
            // 
            // documentsToolStripMenuItem
            // 
            this.documentsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.documentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDocumentsSceneViewToolStripMenuItem});
            this.documentsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
            this.documentsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.documentsToolStripMenuItem.Text = "&Documents";
            // 
            // viewDocumentsSceneViewToolStripMenuItem
            // 
            this.viewDocumentsSceneViewToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.viewDocumentsSceneViewToolStripMenuItem.Checked = true;
            this.viewDocumentsSceneViewToolStripMenuItem.CheckOnClick = true;
            this.viewDocumentsSceneViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewDocumentsSceneViewToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.viewDocumentsSceneViewToolStripMenuItem.Name = "viewDocumentsSceneViewToolStripMenuItem";
            this.viewDocumentsSceneViewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewDocumentsSceneViewToolStripMenuItem.Text = "&Scene View";
            this.viewDocumentsSceneViewToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ViewDocumentsSceneViewToolStripMenuItem_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Final Engine Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkUI.Controls.DarkStatusStrip statusStrip;
        private DarkUI.Controls.DarkMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileExitToolStripMenuItem;
        private DarkUI.Docking.DarkDockPanel dockPanel;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolWindowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolWindowsSceneHierarchyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolWindowsEntityInspectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolWindowsEntitySystemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolWindowsConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDocumentsSceneViewToolStripMenuItem;
    }
}
