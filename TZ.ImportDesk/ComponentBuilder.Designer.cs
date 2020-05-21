namespace TZ.ImportDesk
{
    partial class ComponentBuilder
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
            this.lstTableList = new System.Windows.Forms.ListView();
            this.tblList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cntxCompType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showCoreTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLinkTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMetaTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAttributeTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNoComponentTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstDetail = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAttributeType = new System.Windows.Forms.ComboBox();
            this.btnSaveComponent = new System.Windows.Forms.Button();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckKey = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckCore = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ckReadOnly = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ckUnique = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ckNull = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ckSecuried = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ckRequied = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ckAuto = new System.Windows.Forms.CheckBox();
            this.cmbLookUp = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtExtension = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDefaultValue = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSaveAttribute = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtComponentName = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbCompDisplayName = new System.Windows.Forms.ComboBox();
            this.cmbCompLookup = new System.Windows.Forms.ComboBox();
            this.gbTableList = new System.Windows.Forms.GroupBox();
            this.gbField = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtEntityKey = new System.Windows.Forms.TextBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.cntxCompType.SuspendLayout();
            this.gbTableList.SuspendLayout();
            this.gbField.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstTableList
            // 
            this.lstTableList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tblList,
            this.columnHeader5,
            this.columnHeader6});
            this.lstTableList.ContextMenuStrip = this.cntxCompType;
            this.lstTableList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTableList.FullRowSelect = true;
            this.lstTableList.GridLines = true;
            this.lstTableList.HideSelection = false;
            this.lstTableList.Location = new System.Drawing.Point(26, 87);
            this.lstTableList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstTableList.Name = "lstTableList";
            this.lstTableList.Size = new System.Drawing.Size(420, 519);
            this.lstTableList.TabIndex = 3;
            this.lstTableList.UseCompatibleStateImageBehavior = false;
            this.lstTableList.View = System.Windows.Forms.View.Details;
            this.lstTableList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstTableList_ColumnClick);
            this.lstTableList.SelectedIndexChanged += new System.EventHandler(this.lstTableList_SelectedIndexChanged);
            // 
            // tblList
            // 
            this.tblList.Text = "Table Name";
            this.tblList.Width = 160;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Component Name";
            this.columnHeader5.Width = 160;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Type";
            // 
            // cntxCompType
            // 
            this.cntxCompType.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntxCompType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showCoreTableToolStripMenuItem,
            this.showLinkTableToolStripMenuItem,
            this.showMetaTableToolStripMenuItem,
            this.showAttributeTableToolStripMenuItem,
            this.showNoComponentTablesToolStripMenuItem});
            this.cntxCompType.Name = "cntxCompType";
            this.cntxCompType.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cntxCompType.Size = new System.Drawing.Size(266, 124);
            // 
            // showCoreTableToolStripMenuItem
            // 
            this.showCoreTableToolStripMenuItem.Name = "showCoreTableToolStripMenuItem";
            this.showCoreTableToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.showCoreTableToolStripMenuItem.Text = "Show Core Tables";
            this.showCoreTableToolStripMenuItem.Click += new System.EventHandler(this.showCoreTableToolStripMenuItem_Click);
            // 
            // showLinkTableToolStripMenuItem
            // 
            this.showLinkTableToolStripMenuItem.Name = "showLinkTableToolStripMenuItem";
            this.showLinkTableToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.showLinkTableToolStripMenuItem.Text = "Show Link Tables";
            this.showLinkTableToolStripMenuItem.Click += new System.EventHandler(this.showLinkTableToolStripMenuItem_Click);
            // 
            // showMetaTableToolStripMenuItem
            // 
            this.showMetaTableToolStripMenuItem.Name = "showMetaTableToolStripMenuItem";
            this.showMetaTableToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.showMetaTableToolStripMenuItem.Text = "Show Meta Tables";
            this.showMetaTableToolStripMenuItem.Click += new System.EventHandler(this.showMetaTableToolStripMenuItem_Click);
            // 
            // showAttributeTableToolStripMenuItem
            // 
            this.showAttributeTableToolStripMenuItem.Name = "showAttributeTableToolStripMenuItem";
            this.showAttributeTableToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.showAttributeTableToolStripMenuItem.Text = "Show Attribute Tables";
            this.showAttributeTableToolStripMenuItem.Click += new System.EventHandler(this.showAttributeTableToolStripMenuItem_Click);
            // 
            // showNoComponentTablesToolStripMenuItem
            // 
            this.showNoComponentTablesToolStripMenuItem.Name = "showNoComponentTablesToolStripMenuItem";
            this.showNoComponentTablesToolStripMenuItem.Size = new System.Drawing.Size(265, 24);
            this.showNoComponentTablesToolStripMenuItem.Text = "Show No Component Tables";
            this.showNoComponentTablesToolStripMenuItem.Click += new System.EventHandler(this.showNoComponentTablesToolStripMenuItem_Click);
            // 
            // lstDetail
            // 
            this.lstDetail.BackColor = System.Drawing.Color.White;
            this.lstDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDetail.FullRowSelect = true;
            this.lstDetail.GridLines = true;
            this.lstDetail.HideSelection = false;
            this.lstDetail.Location = new System.Drawing.Point(6, 166);
            this.lstDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstDetail.Name = "lstDetail";
            this.lstDetail.Size = new System.Drawing.Size(525, 438);
            this.lstDetail.TabIndex = 6;
            this.lstDetail.UseCompatibleStateImageBehavior = false;
            this.lstDetail.View = System.Windows.Forms.View.Details;
            this.lstDetail.SelectedIndexChanged += new System.EventHandler(this.lstDetail_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Column Name";
            this.columnHeader1.Width = 130;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Attribute Name";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Length";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Is Key";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(143, 42);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(302, 26);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(41, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Field Type";
            // 
            // cmbAttributeType
            // 
            this.cmbAttributeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAttributeType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbAttributeType.FormattingEnabled = true;
            this.cmbAttributeType.Location = new System.Drawing.Point(123, 45);
            this.cmbAttributeType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAttributeType.Name = "cmbAttributeType";
            this.cmbAttributeType.Size = new System.Drawing.Size(218, 28);
            this.cmbAttributeType.TabIndex = 7;
            this.cmbAttributeType.SelectedIndexChanged += new System.EventHandler(this.cmbAttributeType_SelectedIndexChanged);
            // 
            // btnSaveComponent
            // 
            this.btnSaveComponent.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnSaveComponent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveComponent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveComponent.Location = new System.Drawing.Point(1012, 92);
            this.btnSaveComponent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveComponent.Name = "btnSaveComponent";
            this.btnSaveComponent.Size = new System.Drawing.Size(174, 34);
            this.btnSaveComponent.TabIndex = 25;
            this.btnSaveComponent.Text = "&Create Component";
            this.btnSaveComponent.UseVisualStyleBackColor = false;
            this.btnSaveComponent.Click += new System.EventHandler(this.btnSaveComponent_Click);
            // 
            // txtDisplay
            // 
            this.txtDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDisplay.Location = new System.Drawing.Point(123, 79);
            this.txtDisplay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(218, 26);
            this.txtDisplay.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Display Name";
            // 
            // ckKey
            // 
            this.ckKey.AutoSize = true;
            this.ckKey.Location = new System.Drawing.Point(123, 138);
            this.ckKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckKey.Name = "ckKey";
            this.ckKey.Size = new System.Drawing.Size(18, 17);
            this.ckKey.TabIndex = 9;
            this.ckKey.UseVisualStyleBackColor = true;
            this.ckKey.CheckedChanged += new System.EventHandler(this.ckKey_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(6, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Is Key";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(6, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Is Core";
            // 
            // ckCore
            // 
            this.ckCore.AutoSize = true;
            this.ckCore.Location = new System.Drawing.Point(123, 207);
            this.ckCore.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckCore.Name = "ckCore";
            this.ckCore.Size = new System.Drawing.Size(18, 17);
            this.ckCore.TabIndex = 13;
            this.ckCore.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(192, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Is ReadOnly";
            // 
            // ckReadOnly
            // 
            this.ckReadOnly.AutoSize = true;
            this.ckReadOnly.Location = new System.Drawing.Point(308, 251);
            this.ckReadOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckReadOnly.Name = "ckReadOnly";
            this.ckReadOnly.Size = new System.Drawing.Size(18, 17);
            this.ckReadOnly.TabIndex = 16;
            this.ckReadOnly.UseVisualStyleBackColor = true;
            this.ckReadOnly.CheckedChanged += new System.EventHandler(this.ckReadOnly_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(6, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Is Unique";
            // 
            // ckUnique
            // 
            this.ckUnique.AutoSize = true;
            this.ckUnique.Location = new System.Drawing.Point(123, 250);
            this.ckUnique.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckUnique.Name = "ckUnique";
            this.ckUnique.Size = new System.Drawing.Size(18, 17);
            this.ckUnique.TabIndex = 15;
            this.ckUnique.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(192, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "Is Null";
            // 
            // ckNull
            // 
            this.ckNull.AutoSize = true;
            this.ckNull.Location = new System.Drawing.Point(308, 181);
            this.ckNull.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckNull.Name = "ckNull";
            this.ckNull.Size = new System.Drawing.Size(18, 17);
            this.ckNull.TabIndex = 12;
            this.ckNull.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(192, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 20;
            this.label9.Text = "Is Securied";
            // 
            // ckSecuried
            // 
            this.ckSecuried.AutoSize = true;
            this.ckSecuried.Location = new System.Drawing.Point(308, 144);
            this.ckSecuried.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckSecuried.Name = "ckSecuried";
            this.ckSecuried.Size = new System.Drawing.Size(18, 17);
            this.ckSecuried.TabIndex = 10;
            this.ckSecuried.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(6, 173);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "Is Required";
            // 
            // ckRequied
            // 
            this.ckRequied.AutoSize = true;
            this.ckRequied.Location = new System.Drawing.Point(123, 173);
            this.ckRequied.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckRequied.Name = "ckRequied";
            this.ckRequied.Size = new System.Drawing.Size(18, 17);
            this.ckRequied.TabIndex = 11;
            this.ckRequied.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(192, 213);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 20);
            this.label11.TabIndex = 24;
            this.label11.Text = "Is Auto";
            // 
            // ckAuto
            // 
            this.ckAuto.AutoSize = true;
            this.ckAuto.Location = new System.Drawing.Point(308, 217);
            this.ckAuto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckAuto.Name = "ckAuto";
            this.ckAuto.Size = new System.Drawing.Size(18, 17);
            this.ckAuto.TabIndex = 14;
            this.ckAuto.UseVisualStyleBackColor = true;
            // 
            // cmbLookUp
            // 
            this.cmbLookUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLookUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbLookUp.FormattingEnabled = true;
            this.cmbLookUp.Location = new System.Drawing.Point(123, 287);
            this.cmbLookUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbLookUp.Name = "cmbLookUp";
            this.cmbLookUp.Size = new System.Drawing.Size(218, 28);
            this.cmbLookUp.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.label12.Location = new System.Drawing.Point(6, 287);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 20);
            this.label12.TabIndex = 25;
            this.label12.Text = "Lookup";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DimGray;
            this.label13.Location = new System.Drawing.Point(6, 393);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 20);
            this.label13.TabIndex = 28;
            this.label13.Text = "Length";
            // 
            // txtLength
            // 
            this.txtLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLength.Location = new System.Drawing.Point(123, 389);
            this.txtLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(218, 26);
            this.txtLength.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.DimGray;
            this.label14.Location = new System.Drawing.Point(7, 425);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 20);
            this.label14.TabIndex = 30;
            this.label14.Text = "File Extension";
            // 
            // txtExtension
            // 
            this.txtExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExtension.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtExtension.Location = new System.Drawing.Point(124, 421);
            this.txtExtension.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new System.Drawing.Size(218, 26);
            this.txtExtension.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.DimGray;
            this.label15.Location = new System.Drawing.Point(9, 479);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(110, 20);
            this.label15.TabIndex = 32;
            this.label15.Text = "Default Value";
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefaultValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDefaultValue.Location = new System.Drawing.Point(126, 473);
            this.txtDefaultValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(218, 26);
            this.txtDefaultValue.TabIndex = 22;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DimGray;
            this.label16.Location = new System.Drawing.Point(6, 322);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 20);
            this.label16.TabIndex = 33;
            this.label16.Text = "Comp Lookup";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.DimGray;
            this.label17.Location = new System.Drawing.Point(6, 357);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(114, 20);
            this.label17.TabIndex = 35;
            this.label17.Text = "Comp Display";
            // 
            // btnSaveAttribute
            // 
            this.btnSaveAttribute.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnSaveAttribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAttribute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveAttribute.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveAttribute.Location = new System.Drawing.Point(179, 518);
            this.btnSaveAttribute.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveAttribute.Name = "btnSaveAttribute";
            this.btnSaveAttribute.Size = new System.Drawing.Size(162, 34);
            this.btnSaveAttribute.TabIndex = 23;
            this.btnSaveAttribute.Text = "&Update Field Attributes";
            this.btnSaveAttribute.UseVisualStyleBackColor = true;
            this.btnSaveAttribute.Click += new System.EventHandler(this.btnSaveAttribute_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DimGray;
            this.label19.Location = new System.Drawing.Point(37, 57);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(143, 20);
            this.label19.TabIndex = 41;
            this.label19.Text = "Component Name";
            // 
            // txtComponentName
            // 
            this.txtComponentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComponentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtComponentName.Location = new System.Drawing.Point(186, 50);
            this.txtComponentName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComponentName.Name = "txtComponentName";
            this.txtComponentName.Size = new System.Drawing.Size(330, 26);
            this.txtComponentName.TabIndex = 4;
            this.txtComponentName.TextChanged += new System.EventHandler(this.txtComponentName_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DimGray;
            this.label20.Location = new System.Drawing.Point(34, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(238, 29);
            this.label20.TabIndex = 42;
            this.label20.Text = "Component Builder";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DimGray;
            this.label21.Location = new System.Drawing.Point(54, 41);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(962, 18);
            this.label21.TabIndex = 43;
            this.label21.Text = "Component builder all user to change application table to component where this co" +
    "mponent allow build custom behaviour in business requirement";
            // 
            // cmbCompDisplayName
            // 
            this.cmbCompDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompDisplayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbCompDisplayName.FormattingEnabled = true;
            this.cmbCompDisplayName.Location = new System.Drawing.Point(123, 355);
            this.cmbCompDisplayName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCompDisplayName.Name = "cmbCompDisplayName";
            this.cmbCompDisplayName.Size = new System.Drawing.Size(218, 28);
            this.cmbCompDisplayName.TabIndex = 19;
            // 
            // cmbCompLookup
            // 
            this.cmbCompLookup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompLookup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbCompLookup.FormattingEnabled = true;
            this.cmbCompLookup.Location = new System.Drawing.Point(123, 321);
            this.cmbCompLookup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCompLookup.Name = "cmbCompLookup";
            this.cmbCompLookup.Size = new System.Drawing.Size(218, 28);
            this.cmbCompLookup.TabIndex = 18;
            this.cmbCompLookup.SelectedIndexChanged += new System.EventHandler(this.cmbCompLookup_SelectedIndexChanged_1);
            // 
            // gbTableList
            // 
            this.gbTableList.Controls.Add(this.lstTableList);
            this.gbTableList.Controls.Add(this.txtSearch);
            this.gbTableList.Controls.Add(this.label1);
            this.gbTableList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTableList.ForeColor = System.Drawing.Color.DimGray;
            this.gbTableList.Location = new System.Drawing.Point(12, 80);
            this.gbTableList.Name = "gbTableList";
            this.gbTableList.Size = new System.Drawing.Size(451, 617);
            this.gbTableList.TabIndex = 44;
            this.gbTableList.TabStop = false;
            this.gbTableList.Text = "Table List from Talentoz";
            // 
            // gbField
            // 
            this.gbField.Controls.Add(this.label23);
            this.gbField.Controls.Add(this.btnReset);
            this.gbField.Controls.Add(this.label10);
            this.gbField.Controls.Add(this.label2);
            this.gbField.Controls.Add(this.cmbAttributeType);
            this.gbField.Controls.Add(this.txtDisplay);
            this.gbField.Controls.Add(this.label3);
            this.gbField.Controls.Add(this.ckKey);
            this.gbField.Controls.Add(this.btnSaveAttribute);
            this.gbField.Controls.Add(this.label4);
            this.gbField.Controls.Add(this.cmbCompDisplayName);
            this.gbField.Controls.Add(this.ckCore);
            this.gbField.Controls.Add(this.label17);
            this.gbField.Controls.Add(this.label5);
            this.gbField.Controls.Add(this.cmbCompLookup);
            this.gbField.Controls.Add(this.ckReadOnly);
            this.gbField.Controls.Add(this.label16);
            this.gbField.Controls.Add(this.label6);
            this.gbField.Controls.Add(this.label15);
            this.gbField.Controls.Add(this.ckUnique);
            this.gbField.Controls.Add(this.txtDefaultValue);
            this.gbField.Controls.Add(this.label7);
            this.gbField.Controls.Add(this.label14);
            this.gbField.Controls.Add(this.ckNull);
            this.gbField.Controls.Add(this.txtExtension);
            this.gbField.Controls.Add(this.label8);
            this.gbField.Controls.Add(this.label13);
            this.gbField.Controls.Add(this.ckSecuried);
            this.gbField.Controls.Add(this.txtLength);
            this.gbField.Controls.Add(this.label9);
            this.gbField.Controls.Add(this.cmbLookUp);
            this.gbField.Controls.Add(this.ckRequied);
            this.gbField.Controls.Add(this.label12);
            this.gbField.Controls.Add(this.ckAuto);
            this.gbField.Controls.Add(this.label11);
            this.gbField.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbField.ForeColor = System.Drawing.Color.DimGray;
            this.gbField.Location = new System.Drawing.Point(1012, 133);
            this.gbField.Name = "gbField";
            this.gbField.Size = new System.Drawing.Size(350, 570);
            this.gbField.TabIndex = 45;
            this.gbField.TabStop = false;
            this.gbField.Text = "Field Attribute";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(127, 451);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(158, 17);
            this.label23.TabIndex = 76;
            this.label23.Text = "Separated by Comma(,)";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Gray;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReset.Location = new System.Drawing.Point(9, 518);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(164, 34);
            this.btnReset.TabIndex = 24;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // gbAttributes
            // 
            this.gbAttributes.Controls.Add(this.label24);
            this.gbAttributes.Controls.Add(this.txtEntityKey);
            this.gbAttributes.Controls.Add(this.cmbType);
            this.gbAttributes.Controls.Add(this.label22);
            this.gbAttributes.Controls.Add(this.lstDetail);
            this.gbAttributes.Controls.Add(this.label19);
            this.gbAttributes.Controls.Add(this.txtComponentName);
            this.gbAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAttributes.ForeColor = System.Drawing.Color.DimGray;
            this.gbAttributes.Location = new System.Drawing.Point(469, 90);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Size = new System.Drawing.Size(537, 613);
            this.gbAttributes.TabIndex = 47;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Component & Attribute List";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DimGray;
            this.label24.Location = new System.Drawing.Point(83, 122);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(89, 20);
            this.label24.TabIndex = 45;
            this.label24.Text = "Entity Key ";
            // 
            // txtEntityKey
            // 
            this.txtEntityKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntityKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEntityKey.Location = new System.Drawing.Point(186, 119);
            this.txtEntityKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEntityKey.Name = "txtEntityKey";
            this.txtEntityKey.Size = new System.Drawing.Size(330, 26);
            this.txtEntityKey.TabIndex = 44;
            this.txtEntityKey.TextChanged += new System.EventHandler(this.txtEntityKey_TextChanged);
            // 
            // cmbType
            // 
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(186, 82);
            this.cmbType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(330, 28);
            this.cmbType.TabIndex = 5;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.DimGray;
            this.label22.Location = new System.Drawing.Point(37, 90);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(135, 20);
            this.label22.TabIndex = 43;
            this.label22.Text = "Component Type";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancel.Location = new System.Drawing.Point(1192, 92);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 34);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(679, 90);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 18);
            this.lblMsg.TabIndex = 42;
            // 
            // ComponentBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1452, 747);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbAttributes);
            this.Controls.Add(this.gbField);
            this.Controls.Add(this.gbTableList);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.btnSaveComponent);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ComponentBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Component Builder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ComponentBuilder_FormClosed);
            this.Load += new System.EventHandler(this.ComponentBuilder_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ComponentBuilder_PreviewKeyDown);
            this.cntxCompType.ResumeLayout(false);
            this.gbTableList.ResumeLayout(false);
            this.gbTableList.PerformLayout();
            this.gbField.ResumeLayout(false);
            this.gbField.PerformLayout();
            this.gbAttributes.ResumeLayout(false);
            this.gbAttributes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstTableList;
        private System.Windows.Forms.ColumnHeader tblList;
        private System.Windows.Forms.ListView lstDetail;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAttributeType;
        private System.Windows.Forms.Button btnSaveComponent;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckCore;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckReadOnly;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ckUnique;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckNull;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckSecuried;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ckRequied;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox ckAuto;
        private System.Windows.Forms.ComboBox cmbLookUp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtExtension;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDefaultValue;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnSaveAttribute;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtComponentName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox cmbCompDisplayName;
        private System.Windows.Forms.ComboBox cmbCompLookup;
        private System.Windows.Forms.GroupBox gbTableList;
        private System.Windows.Forms.GroupBox gbField;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ContextMenuStrip cntxCompType;
        private System.Windows.Forms.ToolStripMenuItem showCoreTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLinkTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMetaTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAttributeTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNoComponentTablesToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtEntityKey;
    }
}