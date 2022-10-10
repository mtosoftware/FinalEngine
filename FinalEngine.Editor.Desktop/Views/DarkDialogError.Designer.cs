namespace FinalEngine.Editor.Desktop.Views
{
    partial class DarkDialogError
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
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 12);
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
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // DarkDialogError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(895, 464);
            this.Name = "DarkDialogError";
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.DarkDialogError_ControlAdded);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ErrorProvider ErrorProvider;
    }
}
