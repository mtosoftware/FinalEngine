namespace FinalEngine.Editor.Desktop.Views.Dialogs
{
    partial class CreateEntityDialog
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.darkSectionPanel2 = new DarkUI.Controls.DarkSectionPanel();
            this.darkTreeView1 = new DarkUI.Controls.DarkTreeView();
            this.darkSeparator1 = new DarkUI.Controls.DarkSeparator();
            this.darkToolStrip1 = new DarkUI.Controls.DarkToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.darkSectionPanel1 = new DarkUI.Controls.DarkSectionPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.entityTagTextbox = new DarkUI.Controls.DarkTextBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.darkTitle2 = new DarkUI.Controls.DarkTitle();
            this.darkTitle1 = new DarkUI.Controls.DarkTitle();
            this.identifierTextbox = new DarkUI.Controls.DarkTextBox();
            this.miniToolStrip = new DarkUI.Controls.DarkToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.darkSectionPanel2.SuspendLayout();
            this.darkToolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.darkSectionPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.DataSource = this.bindingSource;
            // 
            // btnOk
            // 
            this.btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnOk.Click += new System.EventHandler(this.ButtonOk_OnClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(110, 12);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 12);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(12, 12);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(12, 12);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(452, 12);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(452, 12);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorProvider.SetIconPadding(this.panel1, 10);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.panel1.Size = new System.Drawing.Size(384, 409);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.darkSectionPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorProvider.SetIconPadding(this.panel3, 10);
            this.panel3.Location = new System.Drawing.Point(10, 182);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel3.Size = new System.Drawing.Size(364, 227);
            this.panel3.TabIndex = 1;
            // 
            // darkSectionPanel2
            // 
            this.darkSectionPanel2.Controls.Add(this.darkTreeView1);
            this.darkSectionPanel2.Controls.Add(this.darkSeparator1);
            this.darkSectionPanel2.Controls.Add(this.darkToolStrip1);
            this.darkSectionPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkSectionPanel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ErrorProvider.SetIconPadding(this.darkSectionPanel2, 10);
            this.darkSectionPanel2.Location = new System.Drawing.Point(0, 0);
            this.darkSectionPanel2.Name = "darkSectionPanel2";
            this.darkSectionPanel2.SectionHeader = "Components";
            this.darkSectionPanel2.Size = new System.Drawing.Size(364, 217);
            this.darkSectionPanel2.TabIndex = 0;
            // 
            // darkTreeView1
            // 
            this.darkTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkTreeView1.HideScrollBars = false;
            this.ErrorProvider.SetIconPadding(this.darkTreeView1, 10);
            this.darkTreeView1.Location = new System.Drawing.Point(1, 25);
            this.darkTreeView1.Margin = new System.Windows.Forms.Padding(5);
            this.darkTreeView1.MaxDragChange = 20;
            this.darkTreeView1.Name = "darkTreeView1";
            this.darkTreeView1.Size = new System.Drawing.Size(362, 161);
            this.darkTreeView1.TabIndex = 3;
            this.darkTreeView1.Text = "darkTreeView1";
            // 
            // darkSeparator1
            // 
            this.darkSeparator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ErrorProvider.SetIconPadding(this.darkSeparator1, 10);
            this.darkSeparator1.Location = new System.Drawing.Point(1, 186);
            this.darkSeparator1.Name = "darkSeparator1";
            this.darkSeparator1.Size = new System.Drawing.Size(362, 2);
            this.darkSeparator1.TabIndex = 2;
            this.darkSeparator1.Text = "darkSeparator1";
            // 
            // darkToolStrip1
            // 
            this.darkToolStrip1.AutoSize = false;
            this.darkToolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.darkToolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ErrorProvider.SetIconPadding(this.darkToolStrip1, 10);
            this.darkToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.darkToolStrip1.Location = new System.Drawing.Point(1, 188);
            this.darkToolStrip1.Name = "darkToolStrip1";
            this.darkToolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.darkToolStrip1.Size = new System.Drawing.Size(362, 28);
            this.darkToolStrip1.TabIndex = 1;
            this.darkToolStrip1.Text = "darkToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripButton1.Image = global::FinalEngine.Editor.Desktop.Properties.Resources.Add_16x;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 25);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripButton2.Image = global::FinalEngine.Editor.Desktop.Properties.Resources.Remove_16x;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 25);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.darkSectionPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ErrorProvider.SetIconPadding(this.panel2, 10);
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel2.Size = new System.Drawing.Size(364, 172);
            this.panel2.TabIndex = 0;
            // 
            // darkSectionPanel1
            // 
            this.darkSectionPanel1.AutoSize = true;
            this.darkSectionPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.darkSectionPanel1.Controls.Add(this.tableLayoutPanel1);
            this.darkSectionPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkSectionPanel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ErrorProvider.SetIconPadding(this.darkSectionPanel1, 10);
            this.darkSectionPanel1.Location = new System.Drawing.Point(0, 0);
            this.darkSectionPanel1.Name = "darkSectionPanel1";
            this.darkSectionPanel1.SectionHeader = "Properties";
            this.darkSectionPanel1.Size = new System.Drawing.Size(364, 162);
            this.darkSectionPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.entityTagTextbox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.darkTitle2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.darkTitle1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.identifierTextbox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorProvider.SetIconPadding(this.tableLayoutPanel1, 10);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 32, 10);
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(362, 136);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // entityTagTextbox
            // 
            this.entityTagTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.entityTagTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.entityTagTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "EntityName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.entityTagTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityTagTextbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.entityTagTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ErrorProvider.SetIconPadding(this.entityTagTextbox, 10);
            this.entityTagTextbox.Location = new System.Drawing.Point(15, 98);
            this.entityTagTextbox.Margin = new System.Windows.Forms.Padding(5);
            this.entityTagTextbox.Name = "entityTagTextbox";
            this.entityTagTextbox.Size = new System.Drawing.Size(310, 23);
            this.entityTagTextbox.TabIndex = 3;
            this.entityTagTextbox.Text = "New Entity";
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(FinalEngine.Editor.Desktop.Views.Dialogs.CreateEntityDialog);
            // 
            // darkTitle2
            // 
            this.darkTitle2.AutoSize = true;
            this.ErrorProvider.SetIconPadding(this.darkTitle2, 10);
            this.darkTitle2.Location = new System.Drawing.Point(15, 73);
            this.darkTitle2.Margin = new System.Windows.Forms.Padding(5);
            this.darkTitle2.Name = "darkTitle2";
            this.darkTitle2.Size = new System.Drawing.Size(61, 15);
            this.darkTitle2.TabIndex = 2;
            this.darkTitle2.Text = "Entity Tag";
            // 
            // darkTitle1
            // 
            this.darkTitle1.AutoSize = true;
            this.ErrorProvider.SetIconPadding(this.darkTitle1, 10);
            this.darkTitle1.Location = new System.Drawing.Point(15, 15);
            this.darkTitle1.Margin = new System.Windows.Forms.Padding(5);
            this.darkTitle1.Name = "darkTitle1";
            this.darkTitle1.Size = new System.Drawing.Size(60, 15);
            this.darkTitle1.TabIndex = 0;
            this.darkTitle1.Text = "Identifier";
            // 
            // identifierTextbox
            // 
            this.identifierTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.identifierTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.identifierTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Identifier", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.identifierTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.identifierTextbox.Enabled = false;
            this.identifierTextbox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.identifierTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ErrorProvider.SetIconPadding(this.identifierTextbox, 10);
            this.identifierTextbox.Location = new System.Drawing.Point(15, 40);
            this.identifierTextbox.Margin = new System.Windows.Forms.Padding(5);
            this.identifierTextbox.Name = "identifierTextbox";
            this.identifierTextbox.Size = new System.Drawing.Size(310, 23);
            this.identifierTextbox.TabIndex = 1;
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleName = "New item selection";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Location = new System.Drawing.Point(60, 4);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.miniToolStrip.Size = new System.Drawing.Size(362, 28);
            this.miniToolStrip.TabIndex = 1;
            // 
            // CreateEntityDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.panel1);
            this.DialogButtons = DarkUI.Forms.DarkDialogButton.OkCancel;
            this.Name = "CreateEntityDialog";
            this.Text = "Create Entity";
            this.Load += new System.EventHandler(this.CreateEntityDialog_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.darkSectionPanel2.ResumeLayout(false);
            this.darkToolStrip1.ResumeLayout(false);
            this.darkToolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.darkSectionPanel1.ResumeLayout(false);
            this.darkSectionPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DarkUI.Controls.DarkTitle darkTitle1;
        private DarkUI.Controls.DarkTextBox identifierTextbox;
        private DarkUI.Controls.DarkTextBox entityTagTextbox;
        private DarkUI.Controls.DarkTitle darkTitle2;
        private DarkUI.Controls.DarkSectionPanel darkSectionPanel2;
        private DarkUI.Controls.DarkSeparator darkSeparator1;
        private DarkUI.Controls.DarkToolStrip darkToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private DarkUI.Controls.DarkToolStrip miniToolStrip;
        private DarkUI.Controls.DarkTreeView darkTreeView1;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}
