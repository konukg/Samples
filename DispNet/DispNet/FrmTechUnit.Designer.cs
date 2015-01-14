namespace DispNet
{
    partial class FrmTechUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTechUnit));
            C1.Win.C1List.Style style2 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style3 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style4 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style5 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style6 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style7 = new C1.Win.C1List.Style();
            C1.Win.C1List.Style style8 = new C1.Win.C1List.Style();
            this.c1FlxGrdTechUnit = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnTest = new System.Windows.Forms.Button();
            this.ListTechState = new C1.Win.C1List.C1List();
            this.grpBoxFindBort = new System.Windows.Forms.GroupBox();
            this.cmbBoxFind = new System.Windows.Forms.ComboBox();
            this.rdBtnState = new System.Windows.Forms.RadioButton();
            this.rdBtnPch = new System.Windows.Forms.RadioButton();
            this.rdBtnType = new System.Windows.Forms.RadioButton();
            this.rdBtnBort = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdTechUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListTechState)).BeginInit();
            this.grpBoxFindBort.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1FlxGrdTechUnit
            // 
            this.c1FlxGrdTechUnit.ColumnInfo = "10,1,0,0,0,85,Columns:0{Width:20;}\t1{Width:40;}\t2{Width:54;}\t3{Width:51;}\t4{Width" +
                ":50;}\t5{Width:49;}\t6{Width:40;}\t7{Width:37;}\t";
            this.c1FlxGrdTechUnit.ExtendLastCol = true;
            this.c1FlxGrdTechUnit.Location = new System.Drawing.Point(12, 12);
            this.c1FlxGrdTechUnit.Name = "c1FlxGrdTechUnit";
            this.c1FlxGrdTechUnit.Rows.DefaultSize = 17;
            this.c1FlxGrdTechUnit.ShowCellLabels = true;
            this.c1FlxGrdTechUnit.Size = new System.Drawing.Size(545, 192);
            this.c1FlxGrdTechUnit.TabIndex = 0;
            this.c1FlxGrdTechUnit.ComboDropDown += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdTechUnit_ComboDropDown);
            this.c1FlxGrdTechUnit.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1FlxGrdTechUnit_AfterEdit);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(355, 18);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(38, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // ListTechState
            // 
            this.ListTechState.AddItemSeparator = ';';
            this.ListTechState.CaptionHeight = 17;
            this.ListTechState.CaptionStyle = style1;
            this.ListTechState.ColumnCaptionHeight = 17;
            this.ListTechState.ColumnFooterHeight = 17;
            this.ListTechState.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
            this.ListTechState.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.ListTechState.EvenRowStyle = style2;
            this.ListTechState.FooterStyle = style3;
            this.ListTechState.HeadingStyle = style4;
            this.ListTechState.HighLightRowStyle = style5;
            this.ListTechState.Images.Add(((System.Drawing.Image)(resources.GetObject("ListTechState.Images"))));
            this.ListTechState.ItemHeight = 15;
            this.ListTechState.Location = new System.Drawing.Point(344, 112);
            this.ListTechState.MatchEntryTimeout = ((long)(2000));
            this.ListTechState.Name = "ListTechState";
            this.ListTechState.OddRowStyle = style6;
            this.ListTechState.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.ListTechState.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.ListTechState.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.ListTechState.SelectedStyle = style7;
            this.ListTechState.Size = new System.Drawing.Size(99, 52);
            this.ListTechState.Style = style8;
            this.ListTechState.TabIndex = 2;
            this.ListTechState.Text = "c1List1";
            this.ListTechState.Visible = false;
            this.ListTechState.PropBag = resources.GetString("ListTechState.PropBag");
            // 
            // grpBoxFindBort
            // 
            this.grpBoxFindBort.Controls.Add(this.btnTest);
            this.grpBoxFindBort.Controls.Add(this.cmbBoxFind);
            this.grpBoxFindBort.Controls.Add(this.rdBtnState);
            this.grpBoxFindBort.Controls.Add(this.rdBtnPch);
            this.grpBoxFindBort.Controls.Add(this.rdBtnType);
            this.grpBoxFindBort.Controls.Add(this.rdBtnBort);
            this.grpBoxFindBort.Location = new System.Drawing.Point(12, 210);
            this.grpBoxFindBort.Name = "grpBoxFindBort";
            this.grpBoxFindBort.Size = new System.Drawing.Size(545, 51);
            this.grpBoxFindBort.TabIndex = 3;
            this.grpBoxFindBort.TabStop = false;
            this.grpBoxFindBort.Text = "Поиск";
            // 
            // cmbBoxFind
            // 
            this.cmbBoxFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBoxFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBoxFind.FormattingEnabled = true;
            this.cmbBoxFind.Location = new System.Drawing.Point(399, 18);
            this.cmbBoxFind.Name = "cmbBoxFind";
            this.cmbBoxFind.Size = new System.Drawing.Size(127, 21);
            this.cmbBoxFind.TabIndex = 4;
            this.cmbBoxFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBoxFind_KeyDown);
            // 
            // rdBtnState
            // 
            this.rdBtnState.AutoSize = true;
            this.rdBtnState.Location = new System.Drawing.Point(290, 19);
            this.rdBtnState.Name = "rdBtnState";
            this.rdBtnState.Size = new System.Drawing.Size(79, 17);
            this.rdBtnState.TabIndex = 3;
            this.rdBtnState.Text = "Состояние";
            this.rdBtnState.UseVisualStyleBackColor = true;
            this.rdBtnState.CheckedChanged += new System.EventHandler(this.rdBtnState_CheckedChanged);
            // 
            // rdBtnPch
            // 
            this.rdBtnPch.AutoSize = true;
            this.rdBtnPch.Location = new System.Drawing.Point(199, 19);
            this.rdBtnPch.Name = "rdBtnPch";
            this.rdBtnPch.Size = new System.Drawing.Size(66, 17);
            this.rdBtnPch.TabIndex = 2;
            this.rdBtnPch.Text = "П/часть";
            this.rdBtnPch.UseVisualStyleBackColor = true;
            this.rdBtnPch.CheckedChanged += new System.EventHandler(this.rdBtnPch_CheckedChanged);
            // 
            // rdBtnType
            // 
            this.rdBtnType.AutoSize = true;
            this.rdBtnType.Location = new System.Drawing.Point(108, 19);
            this.rdBtnType.Name = "rdBtnType";
            this.rdBtnType.Size = new System.Drawing.Size(44, 17);
            this.rdBtnType.TabIndex = 1;
            this.rdBtnType.Text = "Тип";
            this.rdBtnType.UseVisualStyleBackColor = true;
            this.rdBtnType.CheckedChanged += new System.EventHandler(this.rdBtnType_CheckedChanged);
            // 
            // rdBtnBort
            // 
            this.rdBtnBort.AutoSize = true;
            this.rdBtnBort.Checked = true;
            this.rdBtnBort.Location = new System.Drawing.Point(17, 19);
            this.rdBtnBort.Name = "rdBtnBort";
            this.rdBtnBort.Size = new System.Drawing.Size(49, 17);
            this.rdBtnBort.TabIndex = 0;
            this.rdBtnBort.TabStop = true;
            this.rdBtnBort.Text = "Борт";
            this.rdBtnBort.UseVisualStyleBackColor = true;
            this.rdBtnBort.CheckedChanged += new System.EventHandler(this.rdBtnBort_CheckedChanged);
            // 
            // FrmTechUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 274);
            this.Controls.Add(this.grpBoxFindBort);
            this.Controls.Add(this.ListTechState);
            this.Controls.Add(this.c1FlxGrdTechUnit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmTechUnit";
            this.Text = "Техника горнизона";
            this.Load += new System.EventHandler(this.FrmTechUnit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlxGrdTechUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListTechState)).EndInit();
            this.grpBoxFindBort.ResumeLayout(false);
            this.grpBoxFindBort.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid c1FlxGrdTechUnit;
        private System.Windows.Forms.Button btnTest;
        private C1.Win.C1List.C1List ListTechState;
        private System.Windows.Forms.GroupBox grpBoxFindBort;
        private System.Windows.Forms.RadioButton rdBtnType;
        private System.Windows.Forms.RadioButton rdBtnBort;
        private System.Windows.Forms.RadioButton rdBtnState;
        private System.Windows.Forms.RadioButton rdBtnPch;
        private System.Windows.Forms.ComboBox cmbBoxFind;
    }
}