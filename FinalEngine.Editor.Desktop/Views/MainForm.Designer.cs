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
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsSceneHierarchyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsEntityInspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsEntitySystemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolWindowsConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDocumentsSceneViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createEmptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel = new DarkUI.Docking.DarkDockPanel();
            this.darkSeparator2 = new DarkUI.Controls.DarkSeparator();
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
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.worldToolStripMenuItem});
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
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.duplicateToolStripMenuItem,
            this.toolStripMenuItem2,
            this.renameToolStripMenuItem});
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.cutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.cutToolStripMenuItem.Text = "C&ut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.copyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pasteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripMenuItem1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.duplicateToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            this.duplicateToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.duplicateToolStripMenuItem.Text = "Du&plicate";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripMenuItem2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(121, 6);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.renameToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.renameToolStripMenuItem.Text = "&Rename";
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
            this.viewToolWindowsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
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
            this.viewToolWindowsSceneHierarchyToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
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
            this.viewToolWindowsEntityInspectorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
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
            this.viewToolWindowsEntitySystemsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
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
            this.viewToolWindowsConsoleToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.viewToolWindowsConsoleToolStripMenuItem.Text = "&Console";
            this.viewToolWindowsConsoleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.viewToolWindowsConsoleToolStripMenuItem_CheckedChanged);
            // 
            // documentsToolStripMenuItem
            // 
            this.documentsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.documentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDocumentsSceneViewToolStripMenuItem});
            this.documentsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
            this.documentsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
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
            this.viewDocumentsSceneViewToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.viewDocumentsSceneViewToolStripMenuItem.Text = "&Scene View";
            this.viewDocumentsSceneViewToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ViewDocumentsSceneViewToolStripMenuItem_CheckedChanged);
            // 
            // worldToolStripMenuItem
            // 
            this.worldToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.worldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createEmptyToolStripMenuItem});
            this.worldToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.worldToolStripMenuItem.Name = "worldToolStripMenuItem";
            this.worldToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.worldToolStripMenuItem.Text = "&World";
            // 
            // createEmptyToolStripMenuItem
            // 
            this.createEmptyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.createEmptyToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.createEmptyToolStripMenuItem.Name = "createEmptyToolStripMenuItem";
            this.createEmptyToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.createEmptyToolStripMenuItem.Text = "Create &Empty";
            // 
            // dockPanel
            // 
            this.dockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 24);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(1264, 625);
            this.dockPanel.TabIndex = 2;
            // 
            // darkSeparator2
            // 
            this.darkSeparator2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.darkSeparator2.Location = new System.Drawing.Point(0, 649);
            this.darkSeparator2.Name = "darkSeparator2";
            this.darkSeparator2.Size = new System.Drawing.Size(1264, 2);
            this.darkSeparator2.TabIndex = 5;
            this.darkSeparator2.Text = "darkSeparator2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.darkSeparator2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Final Engine Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem worldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createEmptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private DarkUI.Controls.DarkSeparator darkSeparator2;
    }
}
