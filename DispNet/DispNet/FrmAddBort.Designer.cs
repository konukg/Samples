namespace DispNet
{
    partial class FrmAddBort
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
            C1.Win.C1List.Style style1 = new C1.Win.C1List.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddBort));
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style9 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style10 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style11 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style12 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style13 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style14 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style15 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style16 = new C1.Win.C1List.Style();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxBort = new System.Windows.Forms.ComboBox();
            this.btnRecBort = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdBtnLest = new System.Windows.Forms.RadioButton();
            this.checkBoxRasch = new System.Windows.Forms.CheckBox();
            this.rdBtnPch = new System.Windows.Forms.RadioButton();
            this.rdBtnType = new System.Windows.Forms.RadioButton();
            this.rdBtnBort = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemov = new System.Windows.Forms.Button();
            this.c1ListTechPch = new C1.Win.C1List.C1List();
            this.c1ListBortPmt = new C1.Win.C1List.C1List();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListTechPch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListBortPmt)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbBoxBort);
            this.groupBox1.Controls.Add(this.btnRecBort);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnRemov);
            this.groupBox1.Controls.Add(this.c1ListTechPch);
            this.groupBox1.Controls.Add(this.c1ListBortPmt);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 236);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Техника";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(444, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Поиск";
            // 
            // cmbBoxBort
            // 
            this.cmbBoxBort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBoxBort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBoxBort.FormattingEnabled = true;
            this.cmbBoxBort.Location = new System.Drawing.Point(417, 175);
            this.cmbBoxBort.Name = "cmbBoxBort";
            this.cmbBoxBort.Size = new System.Drawing.Size(99, 21);
            this.cmbBoxBort.TabIndex = 7;
            this.cmbBoxBort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBoxBort_KeyDown);
            // 
            // btnRecBort
            // 
            this.btnRecBort.Location = new System.Drawing.Point(437, 204);
            this.btnRecBort.Name = "btnRecBort";
            this.btnRecBort.Size = new System.Drawing.Size(56, 22);
            this.btnRecBort.TabIndex = 5;
            this.btnRecBort.Text = "Запись";
            this.btnRecBort.UseVisualStyleBackColor = true;
            this.btnRecBort.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdBtnLest);
            this.groupBox2.Controls.Add(this.checkBoxRasch);
            this.groupBox2.Controls.Add(this.rdBtnPch);
            this.groupBox2.Controls.Add(this.rdBtnType);
            this.groupBox2.Controls.Add(this.rdBtnBort);
            this.groupBox2.Location = new System.Drawing.Point(417, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(99, 143);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Борт";
            // 
            // rdBtnLest
            // 
            this.rdBtnLest.AutoSize = true;
            this.rdBtnLest.Location = new System.Drawing.Point(17, 88);
            this.rdBtnLest.Name = "rdBtnLest";
            this.rdBtnLest.Size = new System.Drawing.Size(40, 17);
            this.rdBtnLest.TabIndex = 4;
            this.rdBtnLest.TabStop = true;
            this.rdBtnLest.Text = "АЛ";
            this.rdBtnLest.UseVisualStyleBackColor = true;
            this.rdBtnLest.CheckedChanged += new System.EventHandler(this.rdBtnLest_CheckedChanged);
            // 
            // checkBoxRasch
            // 
            this.checkBoxRasch.AutoSize = true;
            this.checkBoxRasch.Checked = true;
            this.checkBoxRasch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRasch.Location = new System.Drawing.Point(17, 114);
            this.checkBoxRasch.Name = "checkBoxRasch";
            this.checkBoxRasch.Size = new System.Drawing.Size(76, 17);
            this.checkBoxRasch.TabIndex = 3;
            this.checkBoxRasch.Text = "В расчете";
            this.checkBoxRasch.UseVisualStyleBackColor = true;
            this.checkBoxRasch.CheckedChanged += new System.EventHandler(this.checkBoxRasch_CheckedChanged);
            // 
            // rdBtnPch
            // 
            this.rdBtnPch.AutoSize = true;
            this.rdBtnPch.Location = new System.Drawing.Point(17, 65);
            this.rdBtnPch.Name = "rdBtnPch";
            this.rdBtnPch.Size = new System.Drawing.Size(66, 17);
            this.rdBtnPch.TabIndex = 2;
            this.rdBtnPch.TabStop = true;
            this.rdBtnPch.Text = "П/часть";
            this.rdBtnPch.UseVisualStyleBackColor = true;
            this.rdBtnPch.CheckedChanged += new System.EventHandler(this.rdBtnPch_CheckedChanged);
            // 
            // rdBtnType
            // 
            this.rdBtnType.AutoSize = true;
            this.rdBtnType.Location = new System.Drawing.Point(17, 42);
            this.rdBtnType.Name = "rdBtnType";
            this.rdBtnType.Size = new System.Drawing.Size(44, 17);
            this.rdBtnType.TabIndex = 1;
            this.rdBtnType.TabStop = true;
            this.rdBtnType.Text = "Тип";
            this.rdBtnType.UseVisualStyleBackColor = true;
            this.rdBtnType.CheckedChanged += new System.EventHandler(this.rdBtnType_CheckedChanged);
            // 
            // rdBtnBort
            // 
            this.rdBtnBort.AutoSize = true;
            this.rdBtnBort.Location = new System.Drawing.Point(17, 19);
            this.rdBtnBort.Name = "rdBtnBort";
            this.rdBtnBort.Size = new System.Drawing.Size(59, 17);
            this.rdBtnBort.TabIndex = 0;
            this.rdBtnBort.TabStop = true;
            this.rdBtnBort.Text = "Номер";
            this.rdBtnBort.UseVisualStyleBackColor = true;
            this.rdBtnBort.CheckedChanged += new System.EventHandler(this.rdBtnBort_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(91, 70);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 22);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "<<";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemov
            // 
            this.btnRemov.Location = new System.Drawing.Point(91, 131);
            this.btnRemov.Name = "btnRemov";
            this.btnRemov.Size = new System.Drawing.Size(45, 22);
            this.btnRemov.TabIndex = 2;
            this.btnRemov.Text = ">>";
            this.btnRemov.UseVisualStyleBackColor = true;
            this.btnRemov.Click += new System.EventHandler(this.btnRemov_Click);
            // 
            // c1ListTechPch
            // 
            this.c1ListTechPch.AddItemSeparator = ';';
            this.c1ListTechPch.AllowColMove = false;
            this.c1ListTechPch.AllowColSelect = false;
            this.c1ListTechPch.Caption = "Техника гарнизона";
            this.c1ListTechPch.CaptionHeight = 17;
            this.c1ListTechPch.CaptionStyle = style1;
            this.c1ListTechPch.ColumnCaptionHeight = 17;
            this.c1ListTechPch.ColumnFooterHeight = 17;
            this.c1ListTechPch.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.c1ListTechPch.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListTechPch.DefColWidth = 50;
            this.c1ListTechPch.EvenRowStyle = style2;
            this.c1ListTechPch.ExtendRightColumn = true;
            this.c1ListTechPch.FooterStyle = style3;
            this.c1ListTechPch.HeadingStyle = style4;
            this.c1ListTechPch.HighLightRowStyle = style5;
            this.c1ListTechPch.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListTechPch.Images"))));
            this.c1ListTechPch.ItemHeight = 15;
            this.c1ListTechPch.Location = new System.Drawing.Point(147, 19);
            this.c1ListTechPch.MatchEntryTimeout = ((long)(2000));
            this.c1ListTechPch.Name = "c1ListTechPch";
            this.c1ListTechPch.OddRowStyle = style6;
            this.c1ListTechPch.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1ListTechPch.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListTechPch.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListTechPch.SelectedStyle = style7;
            this.c1ListTechPch.SelectionMode = C1.Win.C1List.SelectionModeEnum.MultiSimple;
            this.c1ListTechPch.Size = new System.Drawing.Size(254, 193);
            this.c1ListTechPch.Style = style8;
            this.c1ListTechPch.TabIndex = 1;
            this.c1ListTechPch.PropBag = resources.GetString("c1ListTechPch.PropBag");
            // 
            // c1ListBortPmt
            // 
            this.c1ListBortPmt.AddItemSeparator = ';';
            this.c1ListBortPmt.AllowColMove = false;
            this.c1ListBortPmt.AllowColSelect = false;
            this.c1ListBortPmt.Caption = "Путевка";
            this.c1ListBortPmt.CaptionHeight = 17;
            this.c1ListBortPmt.CaptionStyle = style9;
            this.c1ListBortPmt.ColumnCaptionHeight = 17;
            this.c1ListBortPmt.ColumnFooterHeight = 17;
            this.c1ListBortPmt.ColumnWidth = 50;
            this.c1ListBortPmt.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.c1ListBortPmt.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1ListBortPmt.DefColWidth = 50;
            this.c1ListBortPmt.EvenRowStyle = style10;
            this.c1ListBortPmt.ExtendRightColumn = true;
            this.c1ListBortPmt.FooterStyle = style11;
            this.c1ListBortPmt.HeadingStyle = style12;
            this.c1ListBortPmt.HighLightRowStyle = style13;
            this.c1ListBortPmt.Images.Add(((System.Drawing.Image)(resources.GetObject("c1ListBortPmt.Images"))));
            this.c1ListBortPmt.ItemHeight = 15;
            this.c1ListBortPmt.Location = new System.Drawing.Point(11, 19);
            this.c1ListBortPmt.MatchEntryTimeout = ((long)(2000));
            this.c1ListBortPmt.Name = "c1ListBortPmt";
            this.c1ListBortPmt.OddRowStyle = style14;
            this.c1ListBortPmt.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.c1ListBortPmt.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.c1ListBortPmt.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1ListBortPmt.SelectedStyle = style15;
            this.c1ListBortPmt.SelectionMode = C1.Win.C1List.SelectionModeEnum.MultiSimple;
            this.c1ListBortPmt.Size = new System.Drawing.Size(74, 193);
            this.c1ListBortPmt.Style = style16;
            this.c1ListBortPmt.TabIndex = 0;
            this.c1ListBortPmt.Text = "c1List1";
            this.c1ListBortPmt.PropBag = resources.GetString("c1ListBortPmt.PropBag");
            // 
            // FrmAddBort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 244);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddBort";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Выбор техники";
            this.Load += new System.EventHandler(this.FrmAddBort_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListTechPch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ListBortPmt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1List.C1List c1ListBortPmt;
        private C1.Win.C1List.C1List c1ListTechPch;
        private System.Windows.Forms.Button btnRemov;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdBtnPch;
        private System.Windows.Forms.RadioButton rdBtnType;
        private System.Windows.Forms.RadioButton rdBtnBort;
        private System.Windows.Forms.CheckBox checkBoxRasch;
        private System.Windows.Forms.Button btnRecBort;
        private System.Windows.Forms.ComboBox cmbBoxBort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdBtnLest;
    }
}