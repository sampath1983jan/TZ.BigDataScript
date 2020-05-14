namespace TZ.ImportDesk
{
    partial class ViewList
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
            this.lstView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtView = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.grContainer = new System.Windows.Forms.GroupBox();
            this.btnSaveComponent = new System.Windows.Forms.Button();
            this.btnSaveName = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.grContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstView
            // 
            this.lstView.AllowDrop = true;
            this.lstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstView.FullRowSelect = true;
            this.lstView.HideSelection = false;
            this.lstView.Location = new System.Drawing.Point(28, 118);
            this.lstView.Margin = new System.Windows.Forms.Padding(4);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(374, 571);
            this.lstView.TabIndex = 2;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            this.lstView.SelectedIndexChanged += new System.EventHandler(this.lstView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "View List";
            this.columnHeader1.Width = 278;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DimGray;
            this.label21.Location = new System.Drawing.Point(43, 52);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(524, 18);
            this.label21.TabIndex = 47;
            this.label21.Text = "List of view shown below. Here allow user to update and remove existing views";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DimGray;
            this.label20.Location = new System.Drawing.Point(23, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(118, 29);
            this.label20.TabIndex = 46;
            this.label20.Text = "View List";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(1243, 118);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(169, 42);
            this.btnDelete.TabIndex = 51;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtView
            // 
            this.txtView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtView.Location = new System.Drawing.Point(635, 115);
            this.txtView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtView.Name = "txtView";
            this.txtView.Size = new System.Drawing.Size(271, 26);
            this.txtView.TabIndex = 50;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DimGray;
            this.label19.Location = new System.Drawing.Point(471, 118);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(141, 20);
            this.label19.TabIndex = 49;
            this.label19.Text = "Name of the View";
            // 
            // grContainer
            // 
            this.grContainer.AutoSize = true;
            this.grContainer.Controls.Add(this.btnSaveComponent);
            this.grContainer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grContainer.ForeColor = System.Drawing.Color.DimGray;
            this.grContainer.Location = new System.Drawing.Point(441, 152);
            this.grContainer.Margin = new System.Windows.Forms.Padding(4);
            this.grContainer.Name = "grContainer";
            this.grContainer.Padding = new System.Windows.Forms.Padding(4);
            this.grContainer.Size = new System.Drawing.Size(971, 537);
            this.grContainer.TabIndex = 48;
            this.grContainer.TabStop = false;
            this.grContainer.Text = "View Component and relationship";
            // 
            // btnSaveComponent
            // 
            this.btnSaveComponent.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnSaveComponent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveComponent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveComponent.Location = new System.Drawing.Point(619, 20);
            this.btnSaveComponent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveComponent.Name = "btnSaveComponent";
            this.btnSaveComponent.Size = new System.Drawing.Size(148, 42);
            this.btnSaveComponent.TabIndex = 26;
            this.btnSaveComponent.Text = "Add Relation";
            this.btnSaveComponent.UseVisualStyleBackColor = false;
            // 
            // btnSaveName
            // 
            this.btnSaveName.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnSaveName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveName.Location = new System.Drawing.Point(914, 107);
            this.btnSaveName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveName.Name = "btnSaveName";
            this.btnSaveName.Size = new System.Drawing.Size(169, 42);
            this.btnSaveName.TabIndex = 52;
            this.btnSaveName.Text = "Save Name";
            this.btnSaveName.UseVisualStyleBackColor = false;
            // 
            // ViewList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1473, 718);
            this.Controls.Add(this.btnSaveName);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtView);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.grContainer);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lstView);
            this.Name = "ViewList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ViewList_Load);
            this.grContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtView;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox grContainer;
        private System.Windows.Forms.Button btnSaveComponent;
        private System.Windows.Forms.Button btnSaveName;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}