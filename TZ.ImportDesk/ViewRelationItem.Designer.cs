namespace TZ.ImportDesk
{
    partial class ViewRelationItem
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
            this.cmbParentField = new System.Windows.Forms.ComboBox();
            this.lnkRemove = new System.Windows.Forms.LinkLabel();
            this.cmbChildField = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbParentField
            // 
            this.cmbParentField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParentField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbParentField.FormattingEnabled = true;
            this.cmbParentField.Location = new System.Drawing.Point(4, 0);
            this.cmbParentField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbParentField.Name = "cmbParentField";
            this.cmbParentField.Size = new System.Drawing.Size(239, 28);
            this.cmbParentField.TabIndex = 1;
            this.cmbParentField.SelectedIndexChanged += new System.EventHandler(this.cmbParentField_SelectedIndexChanged);
            // 
            // lnkRemove
            // 
            this.lnkRemove.AutoSize = true;
            this.lnkRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkRemove.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkRemove.LinkColor = System.Drawing.Color.Red;
            this.lnkRemove.Location = new System.Drawing.Point(519, 4);
            this.lnkRemove.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkRemove.Name = "lnkRemove";
            this.lnkRemove.Size = new System.Drawing.Size(19, 18);
            this.lnkRemove.TabIndex = 3;
            this.lnkRemove.TabStop = true;
            this.lnkRemove.Text = "X";
            this.lnkRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRemove_LinkClicked);
            // 
            // cmbChildField
            // 
            this.cmbChildField.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChildField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbChildField.FormattingEnabled = true;
            this.cmbChildField.Location = new System.Drawing.Point(251, 0);
            this.cmbChildField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbChildField.Name = "cmbChildField";
            this.cmbChildField.Size = new System.Drawing.Size(239, 28);
            this.cmbChildField.TabIndex = 2;
            this.cmbChildField.SelectedIndexChanged += new System.EventHandler(this.cmbChildField_SelectedIndexChanged);
            // 
            // ViewRelationItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.cmbChildField);
            this.Controls.Add(this.lnkRemove);
            this.Controls.Add(this.cmbParentField);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewRelationItem";
            this.Size = new System.Drawing.Size(557, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbParentField;
        private System.Windows.Forms.LinkLabel lnkRemove;
        private System.Windows.Forms.ComboBox cmbChildField;
    }
}
