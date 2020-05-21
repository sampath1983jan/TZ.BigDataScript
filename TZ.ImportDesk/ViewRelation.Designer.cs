namespace TZ.ImportDesk
{
    partial class ViewRelation
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lnkAddRelation = new System.Windows.Forms.LinkLabel();
            this.gbRelation = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(159, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(154, 23);
            this.textBox2.TabIndex = 1;
            this.textBox2.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox2_DragDrop);
            this.textBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox2_DragEnter);
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // lnkAddRelation
            // 
            this.lnkAddRelation.AutoSize = true;
            this.lnkAddRelation.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkAddRelation.LinkColor = System.Drawing.Color.DarkOrchid;
            this.lnkAddRelation.Location = new System.Drawing.Point(319, 8);
            this.lnkAddRelation.Name = "lnkAddRelation";
            this.lnkAddRelation.Size = new System.Drawing.Size(71, 13);
            this.lnkAddRelation.TabIndex = 2;
            this.lnkAddRelation.TabStop = true;
            this.lnkAddRelation.Text = " Add Relation";
            this.lnkAddRelation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAddRelation_LinkClicked);
            // 
            // gbRelation
            // 
            this.gbRelation.AutoSize = true;
            this.gbRelation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbRelation.Location = new System.Drawing.Point(122, 32);
            this.gbRelation.Name = "gbRelation";
            this.gbRelation.Size = new System.Drawing.Size(6, 19);
            this.gbRelation.TabIndex = 3;
            this.gbRelation.TabStop = false;
            this.gbRelation.Text = "Relationship";
           
            // 
            // ViewRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.gbRelation);
            this.Controls.Add(this.lnkAddRelation);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "ViewRelation";
            this.Size = new System.Drawing.Size(393, 54);
            this.Load += new System.EventHandler(this.ViewRelation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.LinkLabel lnkAddRelation;
        private System.Windows.Forms.GroupBox gbRelation;
    }
}
