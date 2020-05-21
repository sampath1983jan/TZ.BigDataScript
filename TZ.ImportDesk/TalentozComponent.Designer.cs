namespace TZ.ImportDesk
{
    partial class TalentozComponent
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
            this.lstComponentList = new System.Windows.Forms.ListView();
            this.tblList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvComponentAttribute = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnImportComponent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstComponentList
            // 
            this.lstComponentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tblList});
            this.lstComponentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstComponentList.FullRowSelect = true;
            this.lstComponentList.GridLines = true;
            this.lstComponentList.HideSelection = false;
            this.lstComponentList.Location = new System.Drawing.Point(12, 90);
            this.lstComponentList.Name = "lstComponentList";
            this.lstComponentList.Size = new System.Drawing.Size(400, 560);
            this.lstComponentList.TabIndex = 1;
            this.lstComponentList.UseCompatibleStateImageBehavior = false;
            this.lstComponentList.View = System.Windows.Forms.View.Details;
            this.lstComponentList.SelectedIndexChanged += new System.EventHandler(this.lstComponentList_SelectedIndexChanged);
            // 
            // tblList
            // 
            this.tblList.Text = "Component List";
            this.tblList.Width = 275;
            // 
            // lvComponentAttribute
            // 
            this.lvComponentAttribute.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvComponentAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvComponentAttribute.FullRowSelect = true;
            this.lvComponentAttribute.GridLines = true;
            this.lvComponentAttribute.HideSelection = false;
            this.lvComponentAttribute.Location = new System.Drawing.Point(418, 90);
            this.lvComponentAttribute.Name = "lvComponentAttribute";
            this.lvComponentAttribute.Size = new System.Drawing.Size(1044, 560);
            this.lvComponentAttribute.TabIndex = 2;
            this.lvComponentAttribute.UseCompatibleStateImageBehavior = false;
            this.lvComponentAttribute.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Field Name";
            this.columnHeader1.Width = 260;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Field Type";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Is Required";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Is Key Field";
            this.columnHeader4.Width = 80;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DimGray;
            this.label21.Location = new System.Drawing.Point(32, 38);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(561, 18);
            this.label21.TabIndex = 45;
            this.label21.Text = "Show talentoz component list and convert talentoz component to import component";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DimGray;
            this.label20.Location = new System.Drawing.Point(12, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(303, 29);
            this.label20.TabIndex = 44;
            this.label20.Text = "Talentoz Component List";
            // 
            // btnImportComponent
            // 
            this.btnImportComponent.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnImportComponent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportComponent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnImportComponent.Location = new System.Drawing.Point(1099, 660);
            this.btnImportComponent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImportComponent.Name = "btnImportComponent";
            this.btnImportComponent.Size = new System.Drawing.Size(298, 34);
            this.btnImportComponent.TabIndex = 46;
            this.btnImportComponent.Text = "Convert To Import Component";
            this.btnImportComponent.UseVisualStyleBackColor = true;
            this.btnImportComponent.Click += new System.EventHandler(this.btnImportComponent_Click);
            // 
            // TalentozComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 707);
            this.Controls.Add(this.btnImportComponent);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lvComponentAttribute);
            this.Controls.Add(this.lstComponentList);
            this.Name = "TalentozComponent";
            this.Text = "TalentozComponent";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TalentozComponent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lstComponentList;
        private System.Windows.Forms.ColumnHeader tblList;
        private System.Windows.Forms.ListView lvComponentAttribute;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnImportComponent;
    }
}