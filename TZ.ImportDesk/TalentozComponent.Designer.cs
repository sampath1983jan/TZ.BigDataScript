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
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.lstComponentList = new System.Windows.Forms.ListView();
            this.tblList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvComponentAttribute = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnImportComponent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbClients
            // 
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(619, 17);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(801, 24);
            this.cmbClients.TabIndex = 0;
            this.cmbClients.SelectedIndexChanged += new System.EventHandler(this.cmbClients_SelectedIndexChanged);
            // 
            // lstComponentList
            // 
            this.lstComponentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tblList});
            this.lstComponentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstComponentList.FullRowSelect = true;
            this.lstComponentList.GridLines = true;
            this.lstComponentList.HideSelection = false;
            this.lstComponentList.Location = new System.Drawing.Point(12, 57);
            this.lstComponentList.Name = "lstComponentList";
            this.lstComponentList.Size = new System.Drawing.Size(400, 638);
            this.lstComponentList.TabIndex = 1;
            this.lstComponentList.UseCompatibleStateImageBehavior = false;
            this.lstComponentList.View = System.Windows.Forms.View.Details;
            this.lstComponentList.SelectedIndexChanged += new System.EventHandler(this.lstComponentList_SelectedIndexChanged);
            // 
            // tblList
            // 
            this.tblList.Text = "Component List";
            this.tblList.Width = 295;
            // 
            // lvComponentAttribute
            // 
            this.lvComponentAttribute.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvComponentAttribute.FullRowSelect = true;
            this.lvComponentAttribute.GridLines = true;
            this.lvComponentAttribute.HideSelection = false;
            this.lvComponentAttribute.Location = new System.Drawing.Point(424, 57);
            this.lvComponentAttribute.Name = "lvComponentAttribute";
            this.lvComponentAttribute.Size = new System.Drawing.Size(1044, 571);
            this.lvComponentAttribute.TabIndex = 2;
            this.lvComponentAttribute.UseCompatibleStateImageBehavior = false;
            this.lvComponentAttribute.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Field Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Field Type";
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
            // btnImportComponent
            // 
            this.btnImportComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportComponent.Location = new System.Drawing.Point(1157, 644);
            this.btnImportComponent.Name = "btnImportComponent";
            this.btnImportComponent.Size = new System.Drawing.Size(263, 39);
            this.btnImportComponent.TabIndex = 3;
            this.btnImportComponent.Text = "Convert to Import Component";
            this.btnImportComponent.UseVisualStyleBackColor = true;
            this.btnImportComponent.Click += new System.EventHandler(this.btnImportComponent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(349, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose Client to view Component";
            // 
            // TalentozComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 707);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImportComponent);
            this.Controls.Add(this.lvComponentAttribute);
            this.Controls.Add(this.lstComponentList);
            this.Controls.Add(this.cmbClients);
            this.Name = "TalentozComponent";
            this.Text = "TalentozComponent";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TalentozComponent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.ListView lstComponentList;
        private System.Windows.Forms.ColumnHeader tblList;
        private System.Windows.Forms.ListView lvComponentAttribute;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnImportComponent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}