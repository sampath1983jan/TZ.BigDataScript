namespace TZ.ImportDesk
{
    partial class Export
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
            this.savedig = new System.Windows.Forms.SaveFileDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.bntFullExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(155, 157);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(501, 50);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Export Template Only";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAs.Location = new System.Drawing.Point(542, 95);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(114, 28);
            this.btnSaveAs.TabIndex = 10;
            this.btnSaveAs.Text = "Export as";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPath.Enabled = false;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(155, 95);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(381, 26);
            this.txtPath.TabIndex = 11;
            // 
            // bntFullExport
            // 
            this.bntFullExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntFullExport.Location = new System.Drawing.Point(155, 227);
            this.bntFullExport.Name = "bntFullExport";
            this.bntFullExport.Size = new System.Drawing.Size(501, 50);
            this.bntFullExport.TabIndex = 12;
            this.bntFullExport.Text = "Export Component && Template";
            this.bntFullExport.UseVisualStyleBackColor = true;
            this.bntFullExport.Click += new System.EventHandler(this.bntFullExport_Click);
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(800, 289);
            this.Controls.Add(this.bntFullExport);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnExport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Export";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog savedig;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button bntFullExport;
    }
}