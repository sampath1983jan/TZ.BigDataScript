namespace TZ.ImportDesk
{
    partial class ComponentList
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
            this.label18 = new System.Windows.Forms.Label();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.lstDetail = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmbCompDisplayName = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbCompLookup = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDefaultValue = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.cmbLookUp = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ckAuto = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ckRequied = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ckSecuried = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ckNull = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ckUnique = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ckReadOnly = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ckCore = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckKey = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbAttributeType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstComponentList
            // 
            this.lstComponentList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tblList});
            this.lstComponentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstComponentList.FullRowSelect = true;
            this.lstComponentList.GridLines = true;
            this.lstComponentList.HideSelection = false;
            this.lstComponentList.Location = new System.Drawing.Point(12, 75);
            this.lstComponentList.Name = "lstComponentList";
            this.lstComponentList.Size = new System.Drawing.Size(335, 582);
            this.lstComponentList.TabIndex = 1;
            this.lstComponentList.UseCompatibleStateImageBehavior = false;
            this.lstComponentList.View = System.Windows.Forms.View.Details;
            this.lstComponentList.SelectedIndexChanged += new System.EventHandler(this.lstComponentList_SelectedIndexChanged);
            // 
            // tblList
            // 
            this.tblList.Text = "Component List";
            this.tblList.Width = 220;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(350, 33);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(108, 17);
            this.label18.TabIndex = 41;
            this.label18.Text = "Choose Client";
            // 
            // cmbClients
            // 
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(531, 25);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(433, 24);
            this.cmbClients.TabIndex = 40;
            this.cmbClients.SelectedIndexChanged += new System.EventHandler(this.cmbClients_SelectedIndexChanged);
            // 
            // lstDetail
            // 
            this.lstDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader14,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.lstDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDetail.FullRowSelect = true;
            this.lstDetail.GridLines = true;
            this.lstDetail.HideSelection = false;
            this.lstDetail.Location = new System.Drawing.Point(353, 75);
            this.lstDetail.Name = "lstDetail";
            this.lstDetail.Size = new System.Drawing.Size(766, 582);
            this.lstDetail.TabIndex = 39;
            this.lstDetail.UseCompatibleStateImageBehavior = false;
            this.lstDetail.View = System.Windows.Forms.View.Details;
            this.lstDetail.SelectedIndexChanged += new System.EventHandler(this.lstDetail_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Column Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Name";
            this.columnHeader14.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Is Key";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "DefaultValue";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Is Core";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Is Unique";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Length";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Is Required";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Is Null";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Lookup";
            this.columnHeader10.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Component LookUp";
            this.columnHeader11.Width = 120;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Lookup Display";
            this.columnHeader12.Width = 120;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "File Extension";
            this.columnHeader13.Width = 120;
            // 
            // cmbCompDisplayName
            // 
            this.cmbCompDisplayName.FormattingEnabled = true;
            this.cmbCompDisplayName.Location = new System.Drawing.Point(1262, 389);
            this.cmbCompDisplayName.Name = "cmbCompDisplayName";
            this.cmbCompDisplayName.Size = new System.Drawing.Size(218, 24);
            this.cmbCompDisplayName.TabIndex = 74;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1145, 392);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(101, 18);
            this.label17.TabIndex = 73;
            this.label17.Text = "Comp Display";
            // 
            // cmbCompLookup
            // 
            this.cmbCompLookup.FormattingEnabled = true;
            this.cmbCompLookup.Location = new System.Drawing.Point(1262, 350);
            this.cmbCompLookup.Name = "cmbCompLookup";
            this.cmbCompLookup.Size = new System.Drawing.Size(218, 24);
            this.cmbCompLookup.TabIndex = 72;
            this.cmbCompLookup.SelectedIndexChanged += new System.EventHandler(this.cmbCompLookup_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1145, 353);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 18);
            this.label16.TabIndex = 71;
            this.label16.Text = "Comp Lookup";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1145, 541);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(94, 18);
            this.label15.TabIndex = 70;
            this.label15.Text = "Default Value";
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultValue.Location = new System.Drawing.Point(1262, 538);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(218, 24);
            this.txtDefaultValue.TabIndex = 69;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1145, 478);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 18);
            this.label14.TabIndex = 68;
            this.label14.Text = "File Extension";
            // 
            // txtExtension
            // 
            this.txtExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExtension.Location = new System.Drawing.Point(1262, 475);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(218, 24);
            this.txtExtension.TabIndex = 67;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1145, 438);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 18);
            this.label13.TabIndex = 66;
            this.label13.Text = "Length";
            // 
            // txtLength
            // 
            this.txtLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLength.Location = new System.Drawing.Point(1262, 435);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(218, 24);
            this.txtLength.TabIndex = 65;
            // 
            // cmbLookUp
            // 
            this.cmbLookUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLookUp.FormattingEnabled = true;
            this.cmbLookUp.Location = new System.Drawing.Point(1262, 307);
            this.cmbLookUp.Name = "cmbLookUp";
            this.cmbLookUp.Size = new System.Drawing.Size(218, 26);
            this.cmbLookUp.TabIndex = 64;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1145, 307);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 18);
            this.label12.TabIndex = 63;
            this.label12.Text = "Lookup";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1331, 251);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 18);
            this.label11.TabIndex = 62;
            this.label11.Text = "Is Auto";
            // 
            // ckAuto
            // 
            this.ckAuto.AutoSize = true;
            this.ckAuto.Location = new System.Drawing.Point(1448, 251);
            this.ckAuto.Name = "ckAuto";
            this.ckAuto.Size = new System.Drawing.Size(18, 17);
            this.ckAuto.TabIndex = 61;
            this.ckAuto.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1145, 176);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 18);
            this.label10.TabIndex = 60;
            this.label10.Text = "Is Required";
            // 
            // ckRequied
            // 
            this.ckRequied.AutoSize = true;
            this.ckRequied.Location = new System.Drawing.Point(1262, 177);
            this.ckRequied.Name = "ckRequied";
            this.ckRequied.Size = new System.Drawing.Size(18, 17);
            this.ckRequied.TabIndex = 59;
            this.ckRequied.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1331, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 18);
            this.label9.TabIndex = 58;
            this.label9.Text = "Is Securied";
            // 
            // ckSecuried
            // 
            this.ckSecuried.AutoSize = true;
            this.ckSecuried.Location = new System.Drawing.Point(1448, 178);
            this.ckSecuried.Name = "ckSecuried";
            this.ckSecuried.Size = new System.Drawing.Size(18, 17);
            this.ckSecuried.TabIndex = 57;
            this.ckSecuried.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1331, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 18);
            this.label8.TabIndex = 56;
            this.label8.Text = "Is Null";
            // 
            // ckNull
            // 
            this.ckNull.AutoSize = true;
            this.ckNull.Location = new System.Drawing.Point(1448, 216);
            this.ckNull.Name = "ckNull";
            this.ckNull.Size = new System.Drawing.Size(18, 17);
            this.ckNull.TabIndex = 55;
            this.ckNull.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1145, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 18);
            this.label7.TabIndex = 54;
            this.label7.Text = "Is Unique";
            // 
            // ckUnique
            // 
            this.ckUnique.AutoSize = true;
            this.ckUnique.Location = new System.Drawing.Point(1262, 252);
            this.ckUnique.Name = "ckUnique";
            this.ckUnique.Size = new System.Drawing.Size(18, 17);
            this.ckUnique.TabIndex = 53;
            this.ckUnique.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1331, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 18);
            this.label6.TabIndex = 52;
            this.label6.Text = "Is ReadOnly";
            // 
            // ckReadOnly
            // 
            this.ckReadOnly.AutoSize = true;
            this.ckReadOnly.Location = new System.Drawing.Point(1448, 135);
            this.ckReadOnly.Name = "ckReadOnly";
            this.ckReadOnly.Size = new System.Drawing.Size(18, 17);
            this.ckReadOnly.TabIndex = 51;
            this.ckReadOnly.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1145, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 18);
            this.label5.TabIndex = 50;
            this.label5.Text = "Is Core";
            // 
            // ckCore
            // 
            this.ckCore.AutoSize = true;
            this.ckCore.Location = new System.Drawing.Point(1262, 211);
            this.ckCore.Name = "ckCore";
            this.ckCore.Size = new System.Drawing.Size(18, 17);
            this.ckCore.TabIndex = 49;
            this.ckCore.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1145, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 18);
            this.label4.TabIndex = 48;
            this.label4.Text = "Is Key";
            // 
            // ckKey
            // 
            this.ckKey.AutoSize = true;
            this.ckKey.Location = new System.Drawing.Point(1262, 141);
            this.ckKey.Name = "ckKey";
            this.ckKey.Size = new System.Drawing.Size(18, 17);
            this.ckKey.TabIndex = 47;
            this.ckKey.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1145, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.TabIndex = 46;
            this.label3.Text = "Display Name";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplay.Location = new System.Drawing.Point(1262, 91);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(218, 24);
            this.txtDisplay.TabIndex = 45;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1183, 627);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 33);
            this.button1.TabIndex = 44;
            this.button1.Text = "Create/Update Component";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmbAttributeType
            // 
            this.cmbAttributeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAttributeType.FormattingEnabled = true;
            this.cmbAttributeType.Location = new System.Drawing.Point(1262, 48);
            this.cmbAttributeType.Name = "cmbAttributeType";
            this.cmbAttributeType.Size = new System.Drawing.Size(218, 26);
            this.cmbAttributeType.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1145, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 18);
            this.label2.TabIndex = 42;
            this.label2.Text = "Field Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1259, 502);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 18);
            this.label1.TabIndex = 75;
            this.label1.Text = "Separated by Comma(,)";
            // 
            // ComponentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1535, 681);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCompDisplayName);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cmbCompLookup);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtDefaultValue);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtExtension);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.cmbLookUp);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ckAuto);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ckRequied);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ckSecuried);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ckNull);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ckUnique);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ckReadOnly);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ckCore);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbAttributeType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cmbClients);
            this.Controls.Add(this.lstDetail);
            this.Controls.Add(this.lstComponentList);
            this.Name = "ComponentList";
            this.Text = "ComponentList";
            this.Load += new System.EventHandler(this.ComponentList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstComponentList;
        private System.Windows.Forms.ColumnHeader tblList;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.ListView lstDetail;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ComboBox cmbCompDisplayName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbCompLookup;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDefaultValue;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.ComboBox cmbLookUp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox ckAuto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ckRequied;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckSecuried;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckNull;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ckUnique;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckReadOnly;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckCore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbAttributeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}