namespace TZ.ImportDesk
{
    partial class View
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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grContainer = new System.Windows.Forms.GroupBox();
            this.btnSaveComponent = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtView = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnSaveView = new System.Windows.Forms.Button();
            this.txtCoreComponent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstComponentList
            // 
            this.lstComponentList.AllowDrop = true;
            this.lstComponentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstComponentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstComponentList.FullRowSelect = true;
            this.lstComponentList.HideSelection = false;
            this.lstComponentList.Location = new System.Drawing.Point(12, 110);
            this.lstComponentList.Margin = new System.Windows.Forms.Padding(4);
            this.lstComponentList.MultiSelect = false;
            this.lstComponentList.Name = "lstComponentList";
            this.lstComponentList.Size = new System.Drawing.Size(374, 571);
            this.lstComponentList.TabIndex = 1;
            this.lstComponentList.UseCompatibleStateImageBehavior = false;
            this.lstComponentList.View = System.Windows.Forms.View.Details;
            this.lstComponentList.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.lstComponentList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.lstComponentList.DragOver += new System.Windows.Forms.DragEventHandler(this.listView1_DragOver);
            this.lstComponentList.DragLeave += new System.EventHandler(this.listView1_DragLeave);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Component List";
            this.columnHeader1.Width = 260;
            // 
            // grContainer
            // 
            this.grContainer.AutoSize = true;
            this.grContainer.Controls.Add(this.btnSaveComponent);
            this.grContainer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grContainer.ForeColor = System.Drawing.Color.DimGray;
            this.grContainer.Location = new System.Drawing.Point(394, 211);
            this.grContainer.Margin = new System.Windows.Forms.Padding(4);
            this.grContainer.Name = "grContainer";
            this.grContainer.Padding = new System.Windows.Forms.Padding(4);
            this.grContainer.Size = new System.Drawing.Size(971, 470);
            this.grContainer.TabIndex = 2;
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
            this.btnSaveComponent.Click += new System.EventHandler(this.btnSaveComponent_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DimGray;
            this.label19.Location = new System.Drawing.Point(424, 120);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(141, 20);
            this.label19.TabIndex = 42;
            this.label19.Text = "Name of the View";
            // 
            // txtView
            // 
            this.txtView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtView.Location = new System.Drawing.Point(588, 117);
            this.txtView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtView.Name = "txtView";
            this.txtView.Size = new System.Drawing.Size(271, 26);
            this.txtView.TabIndex = 43;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DimGray;
            this.label21.Location = new System.Drawing.Point(54, 41);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(456, 18);
            this.label21.TabIndex = 45;
            this.label21.Text = "View Builder build views based on the component exist in the system";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DimGray;
            this.label20.Location = new System.Drawing.Point(34, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(161, 29);
            this.label20.TabIndex = 44;
            this.label20.Text = "View Builder";
            // 
            // btnSaveView
            // 
            this.btnSaveView.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnSaveView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveView.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveView.Location = new System.Drawing.Point(1184, 138);
            this.btnSaveView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveView.Name = "btnSaveView";
            this.btnSaveView.Size = new System.Drawing.Size(169, 42);
            this.btnSaveView.TabIndex = 46;
            this.btnSaveView.Text = "Save View";
            this.btnSaveView.UseVisualStyleBackColor = false;
            this.btnSaveView.Click += new System.EventHandler(this.btnSaveView_Click);
            // 
            // txtCoreComponent
            // 
            this.txtCoreComponent.AllowDrop = true;
            this.txtCoreComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoreComponent.Location = new System.Drawing.Point(588, 157);
            this.txtCoreComponent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCoreComponent.Name = "txtCoreComponent";
            this.txtCoreComponent.Size = new System.Drawing.Size(271, 26);
            this.txtCoreComponent.TabIndex = 48;
            this.txtCoreComponent.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtCoreComponent_DragDrop);
            this.txtCoreComponent.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtCoreComponent_DragEnter);
            this.txtCoreComponent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCoreComponent_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(424, 160);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Core Component";
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1473, 718);
            this.Controls.Add(this.txtCoreComponent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveView);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtView);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.grContainer);
            this.Controls.Add(this.lstComponentList);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.View_Load);
            this.grContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView lstComponentList;
        private System.Windows.Forms.GroupBox grContainer;
        private System.Windows.Forms.Button btnSaveComponent;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnSaveView;
        private System.Windows.Forms.TextBox txtCoreComponent;
        private System.Windows.Forms.Label label1;
    }
}