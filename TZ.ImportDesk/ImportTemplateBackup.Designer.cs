namespace TZ.ImportDesk
{
    partial class ImportTemplateBackup
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
            this.opnFile = new System.Windows.Forms.OpenFileDialog();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnection = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckListClient = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPath.Enabled = false;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(569, 61);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(381, 26);
            this.txtPath.TabIndex = 13;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAs.Location = new System.Drawing.Point(956, 61);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(114, 28);
            this.btnSaveAs.TabIndex = 12;
            this.btnSaveAs.Text = "Open Export File";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.Location = new System.Drawing.Point(569, 252);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(501, 50);
            this.btnRestore.TabIndex = 14;
            this.btnRestore.Text = "Restore Backup";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // cmbClient
            // 
            this.cmbClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(786, 187);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(284, 28);
            this.cmbClient.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(565, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Choose Client to restore";
            // 
            // txtConnection
            // 
            this.txtConnection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnection.Location = new System.Drawing.Point(569, 141);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(501, 26);
            this.txtConnection.TabIndex = 17;
            this.txtConnection.TextChanged += new System.EventHandler(this.txtConnection_TextChanged);
            this.txtConnection.Leave += new System.EventHandler(this.txtConnection_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(565, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Enter Connection below";
            // 
            // ckListClient
            // 
            this.ckListClient.FormattingEnabled = true;
            this.ckListClient.Location = new System.Drawing.Point(65, 33);
            this.ckListClient.Name = "ckListClient";
            this.ckListClient.Size = new System.Drawing.Size(378, 582);
            this.ckListClient.TabIndex = 19;
            // 
            // ImportTemplateBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1413, 635);
            this.Controls.Add(this.ckListClient);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbClient);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSaveAs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportTemplateBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Template backup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog opnFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox ckListClient;
    }
}